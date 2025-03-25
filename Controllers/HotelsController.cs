using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelManagementSystem.Services; // ✅ Ensure EmailService is recognized

public class HotelsController : Controller
{
    private readonly ApplicationDbContext _context;

    public HotelsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ✅ Display All Hotels (Fixed to return a list of hotels)
    public async Task<IActionResult> Index()
    {
        var hotels = await _context.Hotels.ToListAsync();
        if (!hotels.Any())
        {
            return NotFound("❌ No hotels found. Please check database seeding.");
        }

        return View(hotels); // ✅ Show all hotels
    }

    // ✅ Display Hotel Details
    public async Task<IActionResult> Details(int id)
    {
        var hotel = await _context.Hotels
            .Include(h => h.Rooms) // ✅ Load related rooms
            .FirstOrDefaultAsync(h => h.Id == id);

        if (hotel == null)
        {
            return NotFound();
        }

        return View(hotel);
    }

    // ✅ Show Search Form
    public IActionResult Search()
    {
        return View();
    }

    // ✅ Handle Search Requests
    [HttpPost]
    public async Task<IActionResult> SearchResults(string location, string roomType)
    {
        var availableRooms = await _context.Rooms
            .Include(r => r.Hotel)
            .Where(r => r.Status == "Vacant" &&
                        (string.IsNullOrEmpty(roomType) || r.RoomType.Contains(roomType)) &&
                        (string.IsNullOrEmpty(location) || r.Hotel.Location.Contains(location))) // ✅ Now searches all hotels
            .ToListAsync();

        if (!availableRooms.Any())
        {
            return View("NoRoomsAvailable");
        }

        return View("SearchResults", availableRooms);
    }
}
