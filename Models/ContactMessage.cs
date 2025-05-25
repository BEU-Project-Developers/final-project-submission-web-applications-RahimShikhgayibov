using System.ComponentModel.DataAnnotations;

namespace RestaurantManagementSystem.Models
{
	public class ContactMessage
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Email { get; set; }

		public string Message { get; set; }

		public DateTime SentAt { get; set; }
	}
}
