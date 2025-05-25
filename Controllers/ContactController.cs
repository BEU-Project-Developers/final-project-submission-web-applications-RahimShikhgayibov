using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagementSystem.Controllers
{
	public class ContactController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
