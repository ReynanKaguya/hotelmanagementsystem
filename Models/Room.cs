using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Room
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string RoomNumber { get; set; }

    [Required]
    public required string RoomType { get; set; }

    [Required]
    public decimal PricePerNight { get; set; }

    [Required]
    public string Status { get; set; } = "Vacant"; // Vacant, Occupied, Under Maintenance

    [Required]
    public string ImageUrl { get; set; } = "default-room.jpg";

    // ✅ Define relationship to Hotel
    [ForeignKey("HotelId")]
    public int HotelId { get; set; }

    public virtual Hotel? Hotel { get; set; }
}
