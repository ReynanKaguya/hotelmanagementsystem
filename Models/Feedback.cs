using System;
using System.ComponentModel.DataAnnotations;


namespace HotelManagementSystem.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;

        [Required]
        [StringLength(500, MinimumLength = 5)]
        public string Comment { get; set; } = string.Empty;

        public int Rating { get; set; } // 1-5 star rating

        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}
