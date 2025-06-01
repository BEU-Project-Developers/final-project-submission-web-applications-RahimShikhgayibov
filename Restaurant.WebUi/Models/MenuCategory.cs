
namespace Restaurant.WebUi.Models
{
    public class MenuCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Dish> Dishes { get; set; }
    }
}
