using System.ComponentModel.DataAnnotations;

namespace Restaurant.WebUi.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [Phone]
        public string Phone { get; set; } = "";

        [Required]
        public DateTime ReservationDate { get; set; }

        [Required]
        public TimeSpan ReservationTime { get; set; }

        [Required]
        [Range(1, 20)]
        public int GuestCount { get; set; }

        [StringLength(500)]
        public string SpecialRequests { get; set; } = "";

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation property
        public virtual AppUser User { get; set; }
    }
}