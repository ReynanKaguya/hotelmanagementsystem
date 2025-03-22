using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class RoomsController : Controller
{
    private readonly ApplicationDbContext _context;

    public RoomsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ✅ Show Pending Bookings
    public IActionResult PendingBookings()
    {
        var pendingBookings = _context.Bookings
            .Include(b => b.Room)
            .Where(b => b.Status == "Pending")
            .ToList();

        return View(pendingBookings);
    }

    // ✅ Approve Booking
    public IActionResult ApproveBooking(int id)
    {
        var booking = _context.Bookings.Find(id);
        if (booking != null)
        {
            booking.Status = "Approved";
            _context.SaveChanges();
        }
        return RedirectToAction("PendingBookings");
    }

    // ✅ Reject Booking
    public IActionResult RejectBooking(int id)
    {
        var booking = _context.Bookings.Find(id);
        if (booking != null)
        {
            booking.Status = "Rejected";
            _context.SaveChanges();
        }
        return RedirectToAction("PendingBookings");
    }

    // ✅ Mark as Paid
    public IActionResult MarkPaid(int id)
    {
        var booking = _context.Bookings.Find(id);
        if (booking != null)
        {
            booking.PaymentStatus = "Paid";
            _context.SaveChanges();
        }
        return RedirectToAction("PendingBookings");
    }
}
