using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Room
{
    [Key]
    public int Id { get; set; }

    [Required]
    public required string RoomNumber { get; set; } // ✅ Fixed warning with 'required'

    [Required]
    public required string RoomType { get; set; } // ✅ Fixed

    public decimal PricePerNight { get; set; }

    public string Status { get; set; } = "Vacant"; // Vacant, Occupied, Under Maintenance

    // ✅ Fix: Keep only one `Hotel` relationship
    [ForeignKey("HotelId")]
    public int HotelId { get; set; }  

    public virtual Hotel? Hotel { get; set; } // ✅ Nullable to prevent errors

    // ✅ ImageUrl with default placeholder
    public string ImageUrl { get; set; } = "default-room.jpg";  
}
