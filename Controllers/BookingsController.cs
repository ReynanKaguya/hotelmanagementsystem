using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Services; // ✅ Ensure EmailService is recognized

public class BookingsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly EmailService _emailService;

    public BookingsController(ApplicationDbContext context, EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    // ✅ Show available rooms (GET)
    [HttpGet]
    public IActionResult Create(int hotelId)
    {
        var availableRooms = _context.Rooms
            .Include(r => r.Hotel)
            .Where(r => r.Status == "Vacant" && r.HotelId == hotelId)
            .ToList();

        if (!availableRooms.Any())
        {
            return View("NoRoomsAvailable");
        }

        var booking = new Booking
        {
            Room = availableRooms.First(),
            RoomId = availableRooms.First().Id
        };

        return View(booking);
    }

    // ✅ Show all available rooms (for "Book Now" page)
    [HttpGet]
    public async Task<IActionResult> AvailableRooms()
    {
        var availableRooms = await _context.Rooms
            .Include(r => r.Hotel)
            .Where(r => r.Status == "Vacant")
            .ToListAsync();

        if (!availableRooms.Any())
        {
            return View("NoRoomsAvailable");
        }

        return View(availableRooms);
    }

    // ✅ Handle Booking Submission (POST)
    [HttpPost]
[ValidateAntiForgeryToken] // ✅ Prevent CSRF errors
public async Task<IActionResult> Submit(int roomId, string guestName, string guestEmail, DateTime checkin, DateTime checkout, string paymentMethod)
{
    if (!ModelState.IsValid) // ✅ Check for missing values
    {
        Console.WriteLine("🚨 ModelState Errors:");
        foreach (var error in ModelState)
        {
            Console.WriteLine($"❌ {error.Key}: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
        }
        return View("Create"); // ❌ Prevent crash and return to form
    }

    var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId && r.Status == "Vacant");

    if (room == null)
    {
        return NotFound("❌ Room is not available.");
    }

    var booking = new Booking
    {
        RoomId = roomId,
        GuestName = guestName,
        GuestEmail = guestEmail,
        CheckinDate = checkin,
        CheckoutDate = checkout,
        Status = "Pending",
        PaymentStatus = paymentMethod == "Cash" ? "Pending" : "Paid",
        InvoiceNumber = $"INV-{DateTime.Now:yyyyMMddHHmmss}-{roomId}"
    };

    _context.Bookings.Add(booking);
    room.Status = "Occupied";
    await _context.SaveChangesAsync();

    return RedirectToAction("Confirmation", new { id = booking.Id });
}

    // ✅ Show Booking Confirmation
    [HttpGet]
    public async Task<IActionResult> Confirmation(int id)
    {
        var booking = await _context.Bookings
            .Include(b => b.Room)
            .ThenInclude(r => r.Hotel)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null)
        {
            return NotFound();
        }

        return View(booking);
    }

    // ✅ Show Booking Invoice
    [HttpGet]
    public async Task<IActionResult> Invoice(int id)
    {
        var booking = await _context.Bookings
            .Include(b => b.Room)
            .ThenInclude(r => r.Hotel)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null)
        {
            return NotFound();
        }

        return View(booking);
    }

    // ✅ Show Pending Bookings (For Admin/FrontDesk)
    [HttpGet]
    public async Task<IActionResult> Pending()
    {
        var pendingBookings = await _context.Bookings
            .Include(b => b.Room)
            .Where(b => b.Status == "Pending")
            .ToListAsync();

        return View(pendingBookings);
    }

    // ✅ Manage All Bookings (For Admin/FrontDesk)
    [HttpGet]
    public async Task<IActionResult> ManageBookings()
    {
        var bookings = await _context.Bookings
            .Include(b => b.Room)
            .ThenInclude(r => r.Hotel)
            .ToListAsync();

        return View(bookings);
    }

    // ✅ Approve Booking (Admin/FrontDesk Only)
    [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> ApproveBooking(int bookingId)
{
    var booking = await _context.Bookings.Include(b => b.Room).ThenInclude(r => r.Hotel)
        .FirstOrDefaultAsync(b => b.Id == bookingId);

    if (booking == null)
    {
        return NotFound();
    }

    booking.Status = "Confirmed"; // ✅ Change status to "Confirmed"
    await _context.SaveChangesAsync();

    // ✅ Send Approval Email
    string subject = "🎉 Booking Approved - Cloud9 Suites";
    string message = $"Hello {booking.GuestName},\n\nGreat news! Your booking for {booking.Room.RoomType} at {booking.Room.Hotel.Name} has been approved!\n\n📅 Check-in: {booking.CheckinDate:yyyy-MM-dd}\n📅 Check-out: {booking.CheckoutDate:yyyy-MM-dd}\n\nWe look forward to welcoming you!\n\nCloud9 Suites Team";

    await _emailService.SendEmailAsync(booking.GuestEmail, subject, message);

    return RedirectToAction("Pending"); // ✅ Go back to Pending Bookings
}


[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> RejectBooking(int bookingId)
{
    var booking = await _context.Bookings.Include(b => b.Room).ThenInclude(r => r.Hotel)
        .FirstOrDefaultAsync(b => b.Id == bookingId);

    if (booking == null)
    {
        return NotFound();
    }

    booking.Status = "Rejected"; // ✅ Change status to "Rejected"
    await _context.SaveChangesAsync();

    // ✅ Send Rejection Email
    string subject = "❌ Booking Rejected - Cloud9 Suites";
    string message = $"Hello {booking.GuestName},\n\nWe regret to inform you that your booking for {booking.Room.RoomType} at {booking.Room.Hotel.Name} has been rejected due to unavailability.\n\nWe apologize for any inconvenience.\n\nCloud9 Suites Team";

    await _emailService.SendEmailAsync(booking.GuestEmail, subject, message);

    return RedirectToAction("Pending"); // ✅ Go back to Pending Bookings
}


}
