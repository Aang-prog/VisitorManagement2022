using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VisitorManagement2022.Models;

namespace VisitorManagement2022.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _webHostEnvironment = _webHostEnvironment;

        }

        public IActionResult Index()
        {
            ViewBag.Welcome = "Welcome to the VMS";

            ViewBag.VisitorNew = new Visitors()
            {
                FirstName = "Howard",
                LastName = "The Barbarian"
            };

            ViewData["AnotherWelcome"] = "Please enter your name";
            string rootPath = _webHostEnvironment.WebRootPath;
            FileInfo filePath = new FileInfo(Path.Combine(rootPath, "ConditionsForAcceptance.txt"));
            string[] lines = System.IO.File.ReadAllLines(filePath.ToString());
            return View();
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
}