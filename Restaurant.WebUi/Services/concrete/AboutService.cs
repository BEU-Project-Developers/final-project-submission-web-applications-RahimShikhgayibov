using Microsoft.EntityFrameworkCore;
using Restaurant.WebUi.Data;
using Restaurant.WebUi.Models;
using Restaurant.WebUi.Services.interfaces;
using Restaurant.WebUi.ViewModel;

namespace Restaurant.WebUi.Services.concrete
{
    public class AboutService : IAboutService
    {
        private readonly ApplicationDbContext _context;

        public AboutService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AboutInfo> GetAboutInfoAsync()
        {
            return await _context.AboutInfos.FirstOrDefaultAsync();
        }

        public async Task<List<Chef>> GetChefsAsync()
        {
            return await _context.Chefs.ToListAsync();
        }

        public async Task<List<Testimonial>> GetTestimonialsAsync()
        {
            return await _context.Testimonials.ToListAsync();
        }

        public async Task<AboutIndexViewModel> GetAboutPageDataAsync()
        {
            var aboutInfo = await GetAboutInfoAsync();
            var chefs = await GetChefsAsync();
            var testimonials = await GetTestimonialsAsync();

            return new AboutIndexViewModel
            {
                AboutInfo = aboutInfo,
                Chefs = chefs,
                Testimonials = testimonials
            };
        }
    }

}
