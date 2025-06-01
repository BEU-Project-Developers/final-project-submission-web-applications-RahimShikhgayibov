using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.WebUi.Services.interfaces;

namespace Restaurant.WebUi.Controllers
{
     [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        // GET: /Blog/
        public async Task<IActionResult> Index()
        {
            var posts = await _blogService.GetAllAsync();
            return View(posts);
        }

        // GET: /Blog/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var post = await _blogService.GetByIdAsync(id);
            if (post == null) return NotFound();

            return View(post);
        }
    }
}
