using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagementSystem.Controllers
{
	public class MenuController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
