using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ✅ Database Connection (MariaDB/MySQL)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

// ✅ Identity Configuration
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<EmailService>();

var app = builder.Build();

// ✅ Auto-Apply Migrations & Seed Data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); // Apply any pending migrations
    SeedDatabase(dbContext); // ✅ Now actually seed dummy rooms
}

// ✅ Seed Dummy Data (Hotels & Rooms)
void SeedDatabase(ApplicationDbContext context)
{
    if (!context.Hotels.Any()) // ✅ If no hotels exist, add one
    {
        var hotel = new Hotel
        {
            Name = "Cloud9 Suites",
            Location = "Manila, Philippines",
            Description = "A luxury stay for relaxation.",
            ImageUrl = "hotel-cloud9.jpg"
        };

        context.Hotels.Add(hotel);
        context.SaveChanges(); // ✅ Save the new hotel first!
    }

    var existingHotel = context.Hotels.FirstOrDefault(); // ✅ Get any existing hotel

    if (existingHotel != null && !context.Rooms.Any()) // ✅ If no rooms exist, add them
    {
        context.Rooms.AddRange(new List<Room>
        {
            new Room { HotelId = existingHotel.Id, RoomNumber = "101", RoomType = "Deluxe", PricePerNight = 5000, ImageUrl = "deluxe-room.jpg", Status = "Vacant" },
            new Room { HotelId = existingHotel.Id, RoomNumber = "102", RoomType = "Standard", PricePerNight = 3000, ImageUrl = "standard-room.jpg", Status = "Vacant" },
            new Room { HotelId = existingHotel.Id, RoomNumber = "103", RoomType = "Suite", PricePerNight = 8000, ImageUrl = "suite-room.jpg", Status = "Vacant" }
        });

        context.SaveChanges(); // ✅ Now save the rooms!
        Console.WriteLine("✅ Dummy rooms added!");
    }
}

// ✅ Middleware Pipeline
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
