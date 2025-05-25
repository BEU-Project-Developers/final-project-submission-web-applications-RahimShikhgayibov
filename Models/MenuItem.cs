﻿using System.ComponentModel.DataAnnotations;

namespace RestaurantManagementSystem.Models
{
	public class MenuItem
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public string Description { get; set; }

		[Range(0, double.MaxValue)]
		public decimal Price { get; set; }

		public string ImageUrl { get; set; }

		public int CategoryId { get; set; }
		public virtual Category Category { get; set; }
	}
}
