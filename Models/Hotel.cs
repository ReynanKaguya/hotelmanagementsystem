using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class Hotel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Description { get; set; }

    [Required]
    public required string Location { get; set; }

    [Required]
    public decimal PricePerNight { get; set; }

    [Required]
    public string ImageUrl { get; set; } = "default-hotel.jpg";

    // ✅ Ensure a hotel can have multiple rooms (Relationship)
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
