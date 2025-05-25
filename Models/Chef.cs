using System.ComponentModel.DataAnnotations;

namespace RestaurantManagementSystem.Models
{
	public class Chef
	{
		public int Id { get; set; }

		[Required]
		public string FullName { get; set; }

		public string Specialty { get; set; }
		public string Bio { get; set; }
		public string PhotoUrl { get; set; }
	}
}
