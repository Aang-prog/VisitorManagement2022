using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VisitorManagement2022.Models;
using VisitorManagement2022.Service;

namespace VisitorManagement2022.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ITextFileOperations _textFileOperations;
        public HomeController(ILogger<HomeController> logger, ITextFileOperations textFileOperations)
        {
            _logger = logger;
            _webHostEnvironment = _webHostEnvironment;
            _textFileOperations = textFileOperations;
        }

        public IActionResult Index()
        {
            ViewData["Conditions"] = _textFileOperations.LoadConditionsOfAcceptance();
            ViewBag.Welcome = "Welcome to the VMS";

            ViewBag.VisitorNew = new Visitors()
            {
                FirstName = "Howard",
                LastName = "The Barbarian"
            };

            ViewData["AnotherWelcome"] = "Please enter your name";

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