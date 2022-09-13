using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VisitorManagement2022.Data;
using VisitorManagement2022.Models;

namespace VisitorManagement2022.Controllers
{
    public class VisitorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Visitors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Visitors.Include(v => v.StaffName);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Visitors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Visitors == null)
            {
                return NotFound();
            }

            var visitors = await _context.Visitors
                .Include(v => v.StaffName)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitors == null)
            {
                return NotFound();
            }

            return View(visitors);
        }

        // GET: Visitors/Create
        public IActionResult Create()
        {
            ViewData["StaffNameId"] = new SelectList(_context.StaffNames, "Id", "Name");
            Visitors visitors = new Visitors();
            visitors.DateIn = DateTime.Now;
            return View();
        }

        // POST: Visitors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Buisness,DateIn,DateOut,StaffNameId")] Visitors visitors)
        {
            if (ModelState.IsValid)
            {
                visitors.Id = Guid.NewGuid();
                _context.Add(visitors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaffNameId"] = new SelectList(_context.StaffNames, "Id", "Id", visitors.StaffNameId);
            return View(visitors);
        }

        // GET: Visitors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
            ViewData["StaffNameId"] = new SelectList(_context.StaffNames, "Id", "Id", visitors.StaffNameId);
            return View(visitors);
        }

        // POST: Visitors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FirstName,LastName,Buisness,DateIn,DateOut,StaffNameId")] Visitors visitors)
        {
            if (id != visitors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visitors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitorsExists(visitors.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaffNameId"] = new SelectList(_context.StaffNames, "Id", "Name", visitors.StaffNameId);
            return View(visitors);
        }

        // GET: Visitors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Visitors == null)
            {
                return NotFound();
            }

            var visitors = await _context.Visitors
                .Include(v => v.StaffName)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitors == null)
            {
                return NotFound();
            }

            return View(visitors);
        }

        // POST: Visitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Visitors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Visitors'  is null.");
            }
            var visitors = await _context.Visitors.FindAsync(id);
            if (visitors != null)
            {
                _context.Visitors.Remove(visitors);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitorsExists(Guid id)
        {
            return (_context.Visitors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
