using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

public class NotificationController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly EmailService _emailService;

    public NotificationController(ApplicationDbContext context, EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    public IActionResult SendReminders()
    {
        var tomorrow = DateTime.Now.Date.AddDays(1);
        var upcomingBookings = _context.Bookings
            .Where(b => b.CheckinDate == tomorrow && b.Status == "Confirmed")
            .ToList();

        foreach (var booking in upcomingBookings)
        {
            string subject = "Upcoming Check-in Reminder - Cloud9 Suites";
            string message = $"Hello {booking.GuestName},\n\nThis is a reminder that your check-in is tomorrow ({booking.CheckinDate:yyyy-MM-dd}).\nWe look forward to your stay!\n\nCloud9 Suites Team";
            _emailService.SendEmail(booking.GuestEmail, subject, message);
        }

        return Ok("Reminders sent.");
    }
}
