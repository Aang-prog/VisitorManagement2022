using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using VisitorManagement2022.Data;
using VisitorManagement2022.DTO;
using VisitorManagement2022.Models;
using VisitorManagement2022.Service;
using VisitorManagement2022.ViewModels;
using static VisitorManagement2022.Enum.SweetAlertEnum;

namespace VisitorManagement2022.Controllers
{

    //READONLYS
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ITextFileOperations _textFileOperations;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISweetAlert _sweetalert;
        private readonly IDBCalls _dbCalls;
        private readonly IAPI _aPI;


        //HOMECONTROLLER
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, ITextFileOperations textFileOperations, ApplicationDbContext context, IMapper mapper, ISweetAlert sweetalert, IDBCalls dbCalls, IAPI aPI)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _textFileOperations = textFileOperations;
            _context = context;
            _mapper = mapper;
            _sweetalert = sweetalert;
            _dbCalls = dbCalls;
            _aPI = aPI;
        }

        //INDEX
        public async Task<IActionResult> Index()
        {


            //VARIABLES
            Root root = await _aPI.WeatherAPI();
            VisitorsVM visitorVM = new VisitorsVM();
            var date = DateTime.Now;
            visitorVM.DateIn = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
            var staffList = new SelectList(_context.StaffNames, "Id", "Name");
            visitorVM.DateOut = null;


            //TEMPDATA

            TempData["notification"] = _sweetalert.AlertPopupWithImage("The Visitor Management System", "Automate and record visitors to your organization", NotificationType.success);


            //VIEWDATA
            ViewData["Welcome"] = "Welcome to the VMS";
            ViewData["StaffNameId"] = staffList;
            ViewData["LoggedInVisitors"] = _dbCalls.VisitorsLoggedIn();
            ViewData["Conditions"] = _textFileOperations.LoadConditionsOfAcceptance();
            ViewData["TOE"] = "Terms of Entry";
            ViewData["StaffNameId"] = new SelectList(_context.StaffNames, "Id", "Name");
            ViewData["Temp"] = root.main.temp + "°C";
            ViewData["Wind"] = root.wind.speed + "KPH";

            return View(visitorVM);

        }

        //CREATE
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

                var staff = _context.StaffNames?.Find(visitorsVM.StaffNameId);

                staff.VisitorCount++;
                _context.Update(staff);
                _context.Add(visitors);
                await _context.SaveChangesAsync();
                TempData["create"] = _sweetalert.AlertPopup("Welcome to the College", visitors.FirstName + " visiting " + visitors.StaffName?.Name, NotificationType.success);
                return RedirectToAction(nameof(Index));
            }
            //reloads the select list
            ViewData["StaffNameId"] = new SelectList(_context.StaffNames, "Id", "Id", visitorsVM.StaffNameId);
            return View(visitorsVM);
        }





        //LOGOUT
        [Route("Home/Logout", Name = "LogoutRoute")]
        public async Task<IActionResult> Logout(Guid? id)
        {
            if (id == null || _context.Visitors == null)
            {
                return NotFound();
            }

            var visitors = await _context.Visitors.FindAsync(id);
            if (visitors == null)
            {
                return NotFound();
            }
            //Add in NOW to Date Out
            visitors.DateOut = DateTime.Now;

            //Update The visitor to context
            _context.Update(visitors);
            //Save the data to the database
            await _context.SaveChangesAsync();

            //Add in a sweet alert to confirm the logout also update the sweetalert partial page with a new alert name
            TempData["logout"] = _sweetalert.AlertPopup("Thank you for your visit", visitors.FirstName + " " + visitors.LastName, NotificationType.success);


            return RedirectToAction("Index", "Home");
        }


        //ADMIN
        public IActionResult Admin()
        {
            ViewData["WhereQuery"] = _dbCalls.WhereQuery();
            ViewData["WhereMethodSyntax"] = _dbCalls.WhereMethodSyntax();
            ViewData["OrderByQuery"] = _dbCalls.OrderBy();

            return View();



        }


        //PRIVACY
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
