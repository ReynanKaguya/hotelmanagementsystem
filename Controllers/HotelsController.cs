using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

public class HotelsController : Controller
{
    private readonly ApplicationDbContext _context;

    public HotelsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ✅ Asynchronous Index method (Displays All Hotels)
    public async Task<IActionResult> Index()
    {
        var hotels = await _context.Hotels.ToListAsync();
        return View(hotels);
    }

    // ✅ Asynchronous Details method (View Hotel Details)
    public async Task<IActionResult> Details(int id)
    {
        var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id);
        
        if (hotel == null)
        {
            return NotFound();
        }

        // ✅ Ensure ImageUrl is not null
        hotel.ImageUrl ??= "default-hotel.jpg";

        return View(hotel);
    }

    // ✅ Show Search Form
    public IActionResult Search()
    {
        return View();
    }

    // ✅ Handle Search Requests (Merged duplicate methods)
    [HttpPost]
    public async Task<IActionResult> SearchResults(string location, string roomType, DateTime checkin, DateTime checkout)
    {
        var availableRooms = await _context.Rooms
            .Include(r => r.Hotel)
            .Where(r => r.Status == "Vacant"
                && r.RoomType == roomType
                && (string.IsNullOrEmpty(location) || r.Hotel.Location.Contains(location))) // ✅ Handle null location
            .ToListAsync();

        return View("SearchResults", availableRooms);
    }
}
