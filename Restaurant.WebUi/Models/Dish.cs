
namespace Restaurant.WebUi.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public bool IsSignature { get; set; } = false;

        // Foreign key
        public int MenuCategoryId { get; set; }

        // Navigation properties
        public MenuCategory MenuCategory { get; set; }
        public ICollection<DishIngredient> DishIngredients { get; set; }
    }

}
