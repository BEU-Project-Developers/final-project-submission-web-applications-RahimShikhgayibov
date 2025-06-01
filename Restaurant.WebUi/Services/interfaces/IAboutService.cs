using Restaurant.WebUi.Models;
using Restaurant.WebUi.ViewModel;

namespace Restaurant.WebUi.Services.interfaces
{
    public interface IAboutService
    {
        Task<AboutInfo> GetAboutInfoAsync();
        Task<List<Chef>> GetChefsAsync();
        Task<List<Testimonial>> GetTestimonialsAsync();
        Task<AboutIndexViewModel> GetAboutPageDataAsync();
    }

}
