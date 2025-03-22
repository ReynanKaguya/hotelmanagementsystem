using System.ComponentModel.DataAnnotations;

public class Hotel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; } // ✅ Fixed warning with 'required' keyword

    [Required]
    public required string Description { get; set; } // ✅ Fixed

    [Required]
    public required string Location { get; set; } // ✅ Fixed

    [Required]
    public decimal PricePerNight { get; set; }

    // ✅ Fix: Ensure `ImageUrl` has a default value
    [Required]
    public string ImageUrl { get; set; } = "default-hotel.jpg";


}
