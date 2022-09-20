using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using VisitorManagement2022.Data;
using VisitorManagement2022.Models;
using VisitorManagement2022.Service;

namespace VisitorManagement2022.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ITextFileOperations _textFileOperations;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, ITextFileOperations textFileOperations, ApplicationDbContext context)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _textFileOperations = textFileOperations;
            _context = context;
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


            var staffList = new SelectList(_context.StaffNames, "Id", "Name");


            ViewData["StaffNameId"] = staffList;

            //create an instance of the visitor
            Visitors visitor = new Visitors();
            //pass in the currentdate and time to the Datein property
            visitor.DateIn = DateTime.Now;

            //send that visitor to the Create View
            return View(visitor);
        }

        // GET: Visitors/Create
        public IActionResult Create()
        {
            ViewData["StaffNameId"] = new SelectList(_context.StaffNames, "Id", "Name");
            Visitors visitors = new Visitors();
            visitors.DateIn = DateTime.Now;
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
