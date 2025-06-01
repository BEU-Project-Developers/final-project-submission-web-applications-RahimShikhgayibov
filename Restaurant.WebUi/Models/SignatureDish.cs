namespace Restaurant.WebUi.Models
{
    public class SignatureDish
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }

        public ICollection<DishIngredient> DishIngredients { get; set; }
    }
}
