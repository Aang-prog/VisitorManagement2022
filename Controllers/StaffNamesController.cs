using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VisitorManagement2022.Data;
using VisitorManagement2022.Models;

namespace VisitorManagement2022.Controllers
{
    public class StaffNamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StaffNamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StaffNames
        public async Task<IActionResult> Index()
        {
              return _context.StaffNames != null ? 
                          View(await _context.StaffNames.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.StaffNames'  is null.");
        }

        // GET: StaffNames/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.StaffNames == null)
            {
                return NotFound();
            }

            var staffNames = await _context.StaffNames
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staffNames == null)
            {
                return NotFound();
            }

            return View(staffNames);
        }

        // GET: StaffNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StaffNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Department,VisitorCount")] StaffNames staffNames)
        {
            if (ModelState.IsValid)
            {
                staffNames.Id = Guid.NewGuid();
                _context.Add(staffNames);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staffNames);
        }

        // GET: StaffNames/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.StaffNames == null)
            {
                return NotFound();
            }

            var staffNames = await _context.StaffNames.FindAsync(id);
            if (staffNames == null)
            {
                return NotFound();
            }
            return View(staffNames);
        }

        // POST: StaffNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Department,VisitorCount")] StaffNames staffNames)
        {
            if (id != staffNames.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staffNames);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffNamesExists(staffNames.Id))
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
            return View(staffNames);
        }

        // GET: StaffNames/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.StaffNames == null)
            {
                return NotFound();
            }

            var staffNames = await _context.StaffNames
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staffNames == null)
            {
                return NotFound();
            }

            return View(staffNames);
        }

        // POST: StaffNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.StaffNames == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StaffNames'  is null.");
            }
            var staffNames = await _context.StaffNames.FindAsync(id);
            if (staffNames != null)
            {
                _context.StaffNames.Remove(staffNames);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffNamesExists(Guid id)
        {
          return (_context.StaffNames?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
