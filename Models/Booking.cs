using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Booking
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Room")]
    public int RoomId { get; set; }

    [Required]
    public string GuestName { get; set; } = string.Empty;

    [Required]
    public string GuestEmail { get; set; } = string.Empty;

    [Required]
    public DateTime CheckinDate { get; set; }

    [Required]
    public DateTime CheckoutDate { get; set; }

    [Required]
    public string Status { get; set; } = "Pending"; // Pending, Confirmed, Checked-In, Checked-Out

    public string InvoiceNumber { get; set; } = string.Empty; // Will be generated after payment

    [Required]
    public string PaymentStatus { get; set; } = "Pending"; // Default to "Pending"

    // ✅ Define relationship to Room
    public virtual Room Room { get; set; } = null!;
}
