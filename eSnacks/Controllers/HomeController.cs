using System.Diagnostics;
using eSnacks.Data;
using Microsoft.AspNetCore.Mvc;
using eSnacks.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eSnacks.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        ViewData["Id"] = new SelectList(_context.Cities, "Id", "CityName");
        return View();
    }
    
    [HttpGet]
    public async Task<JsonResult> GetCities(string name)
    {
        // Case-insensitive for ASCII characters A-Z (Sqlite)
        var cities = await _context.Cities.Where(c => EF.Functions.Collate(c.CityName, "NOCASE")
            .StartsWith(name.Trim().ToLower())).OrderBy(x => x).Take(10).ToArrayAsync();
        return Json(cities);
    }

    public IActionResult Privacy()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}