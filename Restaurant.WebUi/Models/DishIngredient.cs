

namespace Restaurant.WebUi.Models
{
    public class DishIngredient
    {
        public int DishId { get; set; }
        public int IngredientId { get; set; }

        // Navigation properties
        public Dish Dish { get; set; }
        public Ingredient Ingredient { get; set; }
    }


}
