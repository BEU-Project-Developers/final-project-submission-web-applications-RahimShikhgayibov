using Restaurant.WebUi.Models;

namespace Restaurant.WebUi.ViewModel
{
    public class HomeIndexViewModel
    {
        public List<Dish> Starters { get; set; }
        public List<Dish> MainCourses { get; set; }
        public List<Dish> Desserts { get; set; }
        public Dish SignatureDish { get; set; }
    }

}
