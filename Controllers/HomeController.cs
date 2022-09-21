using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using VisitorManagement2022.Data;
using VisitorManagement2022.Models;
using VisitorManagement2022.Service;
using VisitorManagement2022.ViewModels;
namespace VisitorManagement2022.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ITextFileOperations _textFileOperations;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, ITextFileOperations textFileOperations, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _textFileOperations = textFileOperations;
            _context = context;
            _mapper = mapper;

        }

        public IActionResult Index()
        {
            ViewData["Conditions"] = _textFileOperations.LoadConditionsOfAcceptance();
            ViewBag.Welcome = "Welcome to the VMS";



            ViewData["AnotherWelcome"] = "Please Sign In";


            var staffList = new SelectList(_context.StaffNames, "Id", "Name");


            ViewData["StaffNameId"] = staffList;

            return View();

        }



        //copied over from the VisitorsController
        // POST: Visitors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public IActionResult Create()
        {
            ViewData["StaffNameId"] = new SelectList(_context.StaffNames, "Id", "Name");
            //create an instance of the visitor
            VisitorsVM visitorVM = new VisitorsVM();
            //pass in the currentdate and time to the Datein property
            visitorVM.DateIn = DateTime.Now;
            //send that visitor to the Create View
            return View(visitorVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Buisness,DateIn,DateOut,StaffNameId")] VisitorsVM visitorsVM)
        {
            Visitors visitors = new Visitors();
            visitors = _mapper.Map(visitorsVM, visitors);
            if (ModelState.IsValid)
            {

                visitors.Id = Guid.NewGuid();
                //increase the counter 
                var staff = _context.StaffNames.Find(visitorsVM.StaffNameId);
                staff.VisitorCount++;
                _context.Update(staff);
                _context.Add(visitors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //reloads the select list
            ViewData["StaffNameId"] = new SelectList(_context.StaffNames, "Id", "Id", visitorsVM.StaffNameId);
            return View(visitorsVM);
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
