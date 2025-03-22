using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Hotel> Hotels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ✅ Fix for MySQL: Set key length for Identity tables
        const int KeyLength = 191; // MySQL index limit

        modelBuilder.Entity<IdentityUser>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(KeyLength);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(KeyLength);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(KeyLength);
        });

        modelBuilder.Entity<IdentityRole>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(KeyLength);
            entity.Property(e => e.NormalizedName).HasMaxLength(KeyLength);
        });

        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.Property(e => e.LoginProvider).HasMaxLength(KeyLength);
            entity.Property(e => e.ProviderKey).HasMaxLength(KeyLength);
        });

        modelBuilder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.Property(e => e.LoginProvider).HasMaxLength(KeyLength);
            entity.Property(e => e.Name).HasMaxLength(KeyLength);
        });

        modelBuilder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(KeyLength);
            entity.Property(e => e.RoleId).HasMaxLength(KeyLength);
        });

        // ✅ Seed initial data for Hotels
        modelBuilder.Entity<Hotel>().HasData(
            new Hotel { Id = 1, Name = "Luxury Hotel", Description = "Experience luxury and comfort.", Location = "Manila", ImageUrl = "P1.jpg" },
            new Hotel { Id = 2, Name = "Beachfront Resort", Description = "Enjoy stunning ocean views.", Location = "Bohol", ImageUrl = "P2.jpg" },
            new Hotel { Id = 3, Name = "City Stay", Description = "Perfect for business travelers.", Location = "Cebu City", ImageUrl = "P3.jpg" }
        );
    }
}
