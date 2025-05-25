using System.ComponentModel.DataAnnotations;

namespace RestaurantManagementSystem.Models
{
	public class Staff
	{
		public int Id { get; set; }

		[Required]
		public string FullName { get; set; }

		public string Role { get; set; } 
		public string Contact { get; set; }
	}
}
