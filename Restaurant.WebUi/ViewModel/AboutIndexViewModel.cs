using Restaurant.WebUi.Models;

namespace Restaurant.WebUi.ViewModel
{
    public class AboutIndexViewModel
    {
        public AboutInfo AboutInfo { get; set; }
        public List<Chef> Chefs { get; set; }
        public List<Testimonial> Testimonials { get; set; }
    }

}
