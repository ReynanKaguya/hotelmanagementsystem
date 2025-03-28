using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Services; // ✅ Ensure EmailService is recognized


[Authorize(Roles = "Admin")] // ✅ Only Admin can assign roles
public class RoleController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // ✅ Show All Roles & Users
    public IActionResult ListRoles()
    {
        var roles = _roleManager.Roles.ToList();
        return View(roles);
    }

    // ✅ Show Role Assignment Page
    public IActionResult AssignRoles()
    {
        var users = _userManager.Users.ToList();
        var roles = _roleManager.Roles.ToList();
        ViewBag.Users = users;
        ViewBag.Roles = roles;
        return View();
    }

    // ✅ Handle Role Assignment
    [HttpPost]
    public async Task<IActionResult> AssignRoles(string email, string role)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return NotFound("User not found.");

        if (!await _roleManager.RoleExistsAsync(role))
        {
            await _roleManager.CreateAsync(new IdentityRole(role)); // Create role if it doesn't exist
        }

        await _userManager.AddToRoleAsync(user, role);
        return RedirectToAction("ListRoles");
    }
}
