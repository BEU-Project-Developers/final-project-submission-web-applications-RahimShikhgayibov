namespace RestaurantManagementSystem.Models
{
	public class Order
	{
		public int Id { get; set; }

		public DateTime OrderDate { get; set; }

		public string CustomerName { get; set; }

		public string Address { get; set; }

		public virtual ICollection<OrderItem> OrderItems { get; set; }
	}
}
