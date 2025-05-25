using System.ComponentModel.DataAnnotations;

namespace RestaurantManagementSystem.Models
{
	public class Category
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public virtual ICollection<MenuItem> MenuItems { get; set; }
	}
}
