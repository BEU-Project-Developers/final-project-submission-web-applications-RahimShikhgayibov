using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.WebUi.Services.interfaces;

namespace Restaurant.WebUi.Controllers
{
    [Authorize]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _aboutService.GetAboutPageDataAsync();
            return View(model);
        }
    }

}
