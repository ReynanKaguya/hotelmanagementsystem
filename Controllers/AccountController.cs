using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HotelManagementSystem.Services; // ✅ Ensure EmailService is recognized

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly EmailService _emailService;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, EmailService emailService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _emailService = emailService;
    }

    // ✅ Show login page
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // ✅ Handle login submission
    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user != null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            
            if (result.Succeeded)
            {
                // ✅ Redirect Based on Role
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("ManageBookings", "Bookings"); // Admin goes to Manage Bookings
                }
                else if (roles.Contains("FrontDesk"))
                {
                    return RedirectToAction("Pending", "Bookings"); // FrontDesk sees Pending Bookings
                }
                else
                {
                    return RedirectToAction("MyBookings", "Bookings"); // Normal User sees their bookings
                }
            }
        }

        ModelState.AddModelError("", "Invalid login attempt. Please check your credentials.");
        return View();
    }

    // ✅ Automatically log out users if they are still logged in
    public async Task<IActionResult> AutoLogout()
    {
        if (User.Identity.IsAuthenticated)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login"); // Redirect to login after logout
        }

        return RedirectToAction("Index", "Home"); // If already logged out, go to home
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }


    // ✅ Show Forgot Password Page
    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    // ✅ Handle Password Reset Request
    [HttpPost]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            ModelState.AddModelError("", "User not found.");
            return View();
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetLink = Url.Action("ResetPassword", "Account", new { token, email }, Request.Scheme);

        // ✅ Send Password Reset Email
        string subject = "🔑 Reset Your Password";
        string message = $"Click the link below to reset your password:\n\n{resetLink}";

        await _emailService.SendEmailAsync(email, subject, message);

        return View("ForgotPasswordConfirmation");
    }

    // ✅ Show Reset Password Page
    [HttpGet]
    public IActionResult ResetPassword(string token, string email)
    {
        if (token == null || email == null)
        {
            return BadRequest("Invalid password reset request.");
        }
        return View();
    }

    // ✅ Handle Reset Password Submission
    [HttpPost]
    public async Task<IActionResult> ResetPassword(string token, string email, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            ModelState.AddModelError("", "User not found.");
            return View();
        }

        var resetResult = await _userManager.ResetPasswordAsync(user, token, newPassword);
        if (resetResult.Succeeded)
        {
            return RedirectToAction("Login");
        }

        ModelState.AddModelError("", "Failed to reset password.");
        return View();
    }
}
