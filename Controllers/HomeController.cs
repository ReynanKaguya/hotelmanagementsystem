using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagementSystem.Services; // ✅ Ensure EmailService is recognized

using System.Linq;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var hotels = _context.Hotels.ToList(); // ✅ Fetch hotels from DB
        return View(hotels);
    }
}
