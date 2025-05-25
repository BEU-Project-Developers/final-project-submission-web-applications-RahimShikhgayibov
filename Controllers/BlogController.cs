using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagementSystem.Controllers
{
	public class BlogController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
