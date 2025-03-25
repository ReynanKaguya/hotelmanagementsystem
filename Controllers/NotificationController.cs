using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Services;

[Authorize] // ✅ Ensures only logged-in users can access this controller
public class NotificationController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly EmailService _emailService;

    public NotificationController(ApplicationDbContext context, EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    // ✅ Send notifications & emails for upcoming check-ins
    public async Task<IActionResult> SendReminderNotifications()
    {
        try
        {
            var tomorrow = DateTime.Now.Date.AddDays(1);
            var upcomingBookings = await _context.Bookings
                .Include(b => b.Room)
                .ThenInclude(r => r.Hotel) // ✅ Ensure hotel details are included
                .Where(b => b.CheckinDate == tomorrow && b.Status == "Confirmed")
                .ToListAsync();

            if (!upcomingBookings.Any())
            {
                return Ok("No upcoming bookings for tomorrow.");
            }

            foreach (var booking in upcomingBookings)
            {
                string subject = "📅 Upcoming Check-in Reminder - Cloud9 Suites";
                string message = $"Hello {booking.GuestName},\n\nThis is a reminder that your check-in is tomorrow ({booking.CheckinDate:yyyy-MM-dd}) at {booking.Room.Hotel.Name}.\nWe look forward to your stay!\n\nBest Regards,\nCloud9 Suites Team";

                // ✅ Send email
                await _emailService.SendEmailAsync(booking.GuestEmail, subject, message);

                // ✅ Save notification in database
                var notification = new Notification
                {
                    UserId = booking.GuestEmail, // Using GuestEmail as UserId reference
                    Message = $"Reminder: Your check-in is tomorrow at {booking.Room.Hotel.Name}!",
                    CreatedAt = DateTime.Now,
                    IsRead = false
                };

                _context.Notifications.Add(notification);
            }

            await _context.SaveChangesAsync();
            return Ok("Reminders sent and notifications saved successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"❌ ERROR: {ex.Message}");
        }
    }

    // ✅ Get notifications for logged-in user
    public async Task<IActionResult> GetUserNotifications()
    {
        try
        {
            var userEmail = User.Identity?.Name;
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("User is not logged in.");
            }

            var notifications = await _context.Notifications
                .Where(n => n.UserId == userEmail)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return View(notifications);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"❌ ERROR: {ex.Message}");
        }
    }

    // ✅ Mark notification as read
    [HttpPost]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        try
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
            {
                return NotFound("Notification not found.");
            }

            notification.IsRead = true;
            await _context.SaveChangesAsync();

            return Ok("Notification marked as read.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"❌ ERROR: {ex.Message}");
        }
    }
}
