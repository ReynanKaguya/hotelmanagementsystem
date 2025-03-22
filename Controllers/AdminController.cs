using Microsoft.AspNetCore.Mvc; // ✅ Fix for "Controller" & "IActionResult" errors
using Microsoft.EntityFrameworkCore; // ✅ Needed for database queries
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization; // ✅ Fix for [Authorize] attribute

[Authorize(Roles = "Admin,Staff")] // ✅ Only Admin & Staff can access
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult Reports()
    {
        var totalBookings = _context.Bookings.Count();
        var totalRevenue = _context.Bookings
            .Where(b => b.PaymentStatus == "Paid")
            .Sum(b => b.Room.PricePerNight * (b.CheckoutDate - b.CheckinDate).Days);

        var roomOccupancy = _context.Rooms
            .GroupBy(r => r.Status)
            .Select(g => new { Status = g.Key, Count = g.Count() })
            .ToList();

        ViewData["TotalBookings"] = totalBookings;
        ViewData["TotalRevenue"] = totalRevenue;
        ViewData["RoomOccupancy"] = roomOccupancy;

        return View();
    }

    public IActionResult Billing()
    {
        var invoices = _context.Bookings.Where(b => b.PaymentStatus == "Paid").ToList();
        return View(invoices);
    }
}
