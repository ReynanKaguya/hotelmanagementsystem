using HotelManagementSystem.Models;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

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

// ✅ Register EmailSettings and EmailService
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<EmailService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// ✅ Apply Migrations and Seed Data
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

    dbContext.Database.Migrate(); // Apply migrations
    SeedDatabase(dbContext); // Seed hotels & rooms

    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // ✅ Ensure roles & users exist
    await EnsureRolesExist(roleManager);
    await EnsureUsersExist(userManager);

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

/// ✅ **Ensure Roles Exist**
async Task EnsureRolesExist(RoleManager<IdentityRole> roleManager)
{
    string[] roles = { "Admin", "FrontDesk" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

/// ✅ **Ensure Users Exist**
async Task EnsureUsersExist(UserManager<IdentityUser> userManager)
{
    await CreateUser(userManager, "Admin123@gmail.com", "Admin@123", "Admin");
    await CreateUser(userManager, "Frontdesk123@gmail.com", "FrontDesk@123", "FrontDesk");
}

/// ✅ **Helper Method to Create Users**
async Task CreateUser(UserManager<IdentityUser> userManager, string email, string password, string role)
{
    var user = await userManager.FindByEmailAsync(email);
    
    if (user == null)
    {
        user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, role);
            Console.WriteLine($"✅ Created {role} user: {email}");
        }
        else
        {
            Console.WriteLine($"❌ Failed to create {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }
    else
    {
        // ✅ Ensure user is assigned to the role
        if (!await userManager.IsInRoleAsync(user, role))
        {
            await userManager.AddToRoleAsync(user, role);
        }
    }
}

/// ✅ **Seed Database Function (Fixed)**
void SeedDatabase(ApplicationDbContext context)
{
    if (!context.Hotels.Any()) 
    {
        var hotels = new List<Hotel>
        {
            new Hotel { Name = "Luxury Hotel", Location = "Manila", Description = "Experience luxury and comfort.", ImageUrl = "P1.jpg" },
            new Hotel { Name = "Beachfront Resort", Location = "Bohol", Description = "Enjoy stunning ocean views.", ImageUrl = "P2.jpg" },
            new Hotel { Name = "City Stay", Location = "Cebu City", Description = "Perfect for business travelers.", ImageUrl = "P3.jpg" }
        };

        context.Hotels.AddRange(hotels);
        context.SaveChanges();
        Console.WriteLine("✅ Hotels seeded!");
    }

    // ✅ Only add new rooms if none exist (DO NOT DELETE old rooms)
    if (!context.Rooms.Any())
    {
        var allHotels = context.Hotels.ToList();
        foreach (var hotel in allHotels)
        {
            var rooms = new List<Room>
            {
                new Room { HotelId = hotel.Id, RoomNumber = "101", RoomType = "Deluxe", PricePerNight = 5000, ImageUrl = "deluxe-room.jpg", Status = "Vacant" },
                new Room { HotelId = hotel.Id, RoomNumber = "102", RoomType = "Standard", PricePerNight = 3000, ImageUrl = "standard-room.jpg", Status = "Vacant" },
                new Room { HotelId = hotel.Id, RoomNumber = "103", RoomType = "Suite", PricePerNight = 8000, ImageUrl = "suite-room.jpg", Status = "Vacant" }
            };

            context.Rooms.AddRange(rooms);
        }

        context.SaveChanges();
        Console.WriteLine("✅ Rooms added across all hotels!");
    }
}
