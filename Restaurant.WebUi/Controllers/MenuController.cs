using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.WebUi.Services.interfaces;

namespace Restaurant.WebUi.Controllers
{
     [Authorize]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        public async Task<IActionResult> Index()
        {
            var menu = await _menuService.GetMenuAsync();
            return View(menu); 
        }
    }

}
