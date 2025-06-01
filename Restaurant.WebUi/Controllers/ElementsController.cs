using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.WebUi.Controllers
{
     [Authorize]
    public class ElementsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
