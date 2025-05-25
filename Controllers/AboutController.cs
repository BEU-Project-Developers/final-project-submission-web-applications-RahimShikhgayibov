using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagementSystem.Controllers
{
	public class AboutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
