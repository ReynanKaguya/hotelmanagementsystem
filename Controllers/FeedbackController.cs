using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelManagementSystem.Services; // ✅ Ensure EmailService is recognized


public class FeedbackController : Controller
{
    private static List<FeedbackModel> feedbackList = new List<FeedbackModel>();

    // ✅ Show Feedback Page
    public IActionResult Index()
    {
        return View("Feedback");
    }

    // ✅ Handle Feedback Submission
    [HttpPost]
    public IActionResult SubmitFeedback(string guestName, string comment)
    {
        if (string.IsNullOrEmpty(guestName) || string.IsNullOrEmpty(comment))
        {
            return BadRequest("❌ Please provide your name and feedback.");
        }

        feedbackList.Add(new FeedbackModel { GuestName = guestName, Comment = comment });

        return RedirectToAction("ThankYou");
    }

    // ✅ Show Thank You Page
    public IActionResult ThankYou()
    {
        return View();
    }
}

// ✅ Feedback Model
public class FeedbackModel
{
    public string GuestName { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
}
