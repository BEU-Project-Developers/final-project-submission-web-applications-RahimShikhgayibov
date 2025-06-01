using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.WebUi.Models;
using Restaurant.WebUi.Services.interfaces;

namespace Restaurant.WebUi.Controllers;

[Authorize]
public class HomeController : Controller
{

    private readonly IMenuService _menuService;

    public HomeController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _menuService.GetHomeDataAsync();
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
