using System.Diagnostics;
using Hancerlioglu.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hancerlioglu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Baklava()
        {
            return View();
        }
        public IActionResult Kadayif()
        {
            return View();
        }
        public IActionResult Kunefe()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
