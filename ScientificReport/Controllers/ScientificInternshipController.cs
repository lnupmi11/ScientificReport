using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;

namespace ScientificReport.Controllers
{
    public class ScientificInternshipController : Controller
    {
        private readonly ScientificReportDbContext _context;

        public ScientificInternshipController(ScientificReportDbContext context)
        {
            _context = context;
        }

        // GET: ScientificInternship
        public async Task<IActionResult> Index()
        {
            return View(await _context.ScientificInternships.ToListAsync());
        }

        // GET: ScientificInternship/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scientificInternship = await _context.ScientificInternships
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scientificInternship == null)
            {
                return NotFound();
            }

            return View(scientificInternship);
        }

        // GET: ScientificInternship/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ScientificInternship/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlaceOfInternship,Started,Ended,Contents")] ScientificInternship scientificInternship)
        {
            if (ModelState.IsValid)
            {
                scientificInternship.Id = Guid.NewGuid();
                _context.Add(scientificInternship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scientificInternship);
        }

        // GET: ScientificInternship/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scientificInternship = await _context.ScientificInternships.FindAsync(id);
            if (scientificInternship == null)
            {
                return NotFound();
            }
            return View(scientificInternship);
        }

        // POST: ScientificInternship/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PlaceOfInternship,Started,Ended,Contents")] ScientificInternship scientificInternship)
        {
            if (id != scientificInternship.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scientificInternship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScientificInternshipExists(scientificInternship.Id))
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
            return View(scientificInternship);
        }

        // GET: ScientificInternship/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scientificInternship = await _context.ScientificInternships
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scientificInternship == null)
            {
                return NotFound();
            }

            return View(scientificInternship);
        }

        // POST: ScientificInternship/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var scientificInternship = await _context.ScientificInternships.FindAsync(id);
            _context.ScientificInternships.Remove(scientificInternship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScientificInternshipExists(Guid id)
        {
            return _context.ScientificInternships.Any(e => e.Id == id);
        }
    }
}
