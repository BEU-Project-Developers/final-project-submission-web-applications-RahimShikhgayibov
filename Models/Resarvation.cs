using System.ComponentModel.DataAnnotations;

namespace RestaurantManagementSystem.Models
{
	public class Resarvation
	{
		public int Id { get; set; }

		[Required]
		public string CustomerName { get; set; }

		[Required]
		public string Phone { get; set; }

		public DateTime ReservationDate { get; set; }

		public int NumberOfGuests { get; set; }
	}
}
