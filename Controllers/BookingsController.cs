using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
        var room = _context.Rooms
            .Include(r => r.Hotel) // ✅ Load Hotel details
            .FirstOrDefault(r => r.HotelId == hotelId && r.Status == "Vacant");

        if (room == null)
        {
            return View("NoRoomsAvailable"); // ✅ Show "No rooms available" view
        }

        var booking = new Booking 
        { 
            Room = room, 
            RoomId = room.Id 
        };

        return View(booking);
    }

    // ✅ Handle booking submission (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Submit(int roomId, string guestName, string guestEmail, DateTime checkin, DateTime checkout)
    {
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
            Status = "Confirmed",
            PaymentStatus = "Pending"
        };

        _context.Bookings.Add(booking);
        room.Status = "Occupied";
        await _context.SaveChangesAsync();

        // ✅ Send confirmation email
        string subject = "📩 Booking Confirmation - Cloud9 Suites";
        string message = $"Hello {guestName},\n\nYour booking for {room.RoomType} is confirmed!\nCheck-in: {checkin:yyyy-MM-dd}\nCheck-out: {checkout:yyyy-MM-dd}\n\nThank you!";
        _emailService.SendEmail(guestEmail, subject, message);

        return RedirectToAction("Confirmation", new { id = booking.Id });
    }

    // ✅ Handle payment processing (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProcessPayment(int bookingId, string paymentMethod)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == bookingId);

        if (booking == null || booking.PaymentStatus == "Paid")
        {
            return BadRequest("❌ Invalid booking or payment already completed.");
        }

        // Generate unique invoice number
        booking.InvoiceNumber = $"INV-{DateTime.Now:yyyyMMddHHmmss}-{booking.Id}";
        booking.PaymentStatus = "Paid";
        await _context.SaveChangesAsync();

        return RedirectToAction("Invoice", new { id = booking.Id });
    }

    // ✅ Handle check-in (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckIn(int bookingId)
    {
        var booking = await _context.Bookings.Include(b => b.Room).FirstOrDefaultAsync(b => b.Id == bookingId);

        if (booking == null || booking.Status != "Confirmed")
        {
            return BadRequest("❌ Invalid check-in request.");
        }

        booking.Status = "Checked-In";
        booking.Room.Status = "Occupied";
        await _context.SaveChangesAsync();

        return RedirectToAction("ManageBookings");
    }

    // ✅ Handle check-out (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckOut(int bookingId)
    {
        var booking = await _context.Bookings.Include(b => b.Room).FirstOrDefaultAsync(b => b.Id == bookingId);

        if (booking == null || booking.Status != "Checked-In")
        {
            return BadRequest("❌ Invalid check-out request.");
        }

        booking.Status = "Checked-Out";
        booking.Room.Status = "Vacant";
        await _context.SaveChangesAsync();

        // ✅ Send "Thank You" email
        string subject = "🎉 Thank You for Staying with Us!";
        string message = $"Hello {booking.GuestName},\n\nThank you for staying at Cloud9 Suites! We hope you had a great experience.\n\nWe look forward to welcoming you again soon!";
        _emailService.SendEmail(booking.GuestEmail, subject, message);

        return RedirectToAction("ManageBookings");
    }

    // ✅ Show booking confirmation (GET)
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

    // ✅ Show invoice (GET)
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

    // ✅ Show pending bookings (GET)
    [HttpGet]
    public async Task<IActionResult> Pending()
    {
        var pendingBookings = await _context.Bookings
            .Include(b => b.Room)
            .Where(b => b.Status == "Pending")
            .ToListAsync();

        return View(pendingBookings);
    }

    // ✅ Manage bookings (Approve/Reject)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Manage(int id, string status)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null)
        {
            return NotFound();
        }

        booking.Status = status;
        await _context.SaveChangesAsync();

        return RedirectToAction("Pending");
    }
}
