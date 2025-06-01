using Microsoft.EntityFrameworkCore;
using Restaurant.WebUi.Data;
using Restaurant.WebUi.Services.interfaces;
using Restaurant.WebUi.ViewModel;

namespace Restaurant.WebUi.Services.concrete
{
    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext _context;

        public MenuService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuCategoryViewModel>> GetMenuAsync()
        {
            var categories = await _context.MenuCategories
                .Include(c => c.Dishes)
                    .ThenInclude(d => d.DishIngredients)
                        .ThenInclude(di => di.Ingredient)
                .Select(c => new MenuCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Dishes = c.Dishes.Select(d => new DishViewModel
                    {
                        Id = d.Id,
                        Title = d.Title,
                        Price = d.Price,
                        ImagePath = d.ImagePath,
                        IsSignature = d.IsSignature,
                        Ingredients = d.DishIngredients.Select(di => new IngredientViewModel
                        {
                            Id = di.Ingredient.Id,
                            Name = di.Ingredient.Name
                        }).ToList()
                    }).ToList()
                }).ToListAsync();

            return categories;
        }
        public async Task<HomeIndexViewModel> GetHomeDataAsync()
        {
            var dishes = await _context.Dishes
                .Include(d => d.MenuCategory)
                .Include(d => d.DishIngredients)
                    .ThenInclude(di => di.Ingredient)
                .ToListAsync();

            return new HomeIndexViewModel
            {
                Starters = dishes.Where(d => d.MenuCategory.Name == "Starters").ToList(),
                MainCourses = dishes.Where(d => d.MenuCategory.Name == "Main").ToList(),
                Desserts = dishes.Where(d => d.MenuCategory.Name == "Desserts").ToList(),
                SignatureDish = dishes.FirstOrDefault(d => d.IsSignature)
            };
        }
    }
}
