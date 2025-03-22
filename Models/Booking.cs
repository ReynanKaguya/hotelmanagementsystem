using System;
using System.ComponentModel.DataAnnotations;

public class Booking
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int RoomId { get; set; }

    [Required]
    public string GuestName { get; set; } = string.Empty; // ✅ Fix: Provide default value

    [Required]
    public string GuestEmail { get; set; } = string.Empty; // ✅ Fix: Provide default value

    [Required]
    public DateTime CheckinDate { get; set; }

    [Required]
    public DateTime CheckoutDate { get; set; }

    [Required]
    public string Status { get; set; } = "Pending"; // ✅ Already has a default value

    public string InvoiceNumber { get; set; } = string.Empty; // Will be generated after payment

        [Required]
    public string PaymentStatus { get; set; } = "Pending"; // Default to "Pending"


    public Room Room { get; set; } = null!; // ✅ Fix: "null!" tells .NET that we will assign it later
}
