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
    public class ScientificConsultationController : Controller
    {
        private readonly ScientificReportDbContext _context;

        public ScientificConsultationController(ScientificReportDbContext context)
        {
            _context = context;
        }

        // GET: ScientificConsultation
        public async Task<IActionResult> Index()
        {
            return View(await _context.ScientificConsultations.ToListAsync());
        }

        // GET: ScientificConsultation/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scientificConsultation = await _context.ScientificConsultations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scientificConsultation == null)
            {
                return NotFound();
            }

            return View(scientificConsultation);
        }

        // GET: ScientificConsultation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ScientificConsultation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CandidateName,DissertationTitle")] ScientificConsultation scientificConsultation)
        {
            if (ModelState.IsValid)
            {
                scientificConsultation.Id = Guid.NewGuid();
                _context.Add(scientificConsultation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(scientificConsultation);
        }

        // GET: ScientificConsultation/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scientificConsultation = await _context.ScientificConsultations.FindAsync(id);
            if (scientificConsultation == null)
            {
                return NotFound();
            }
            return View(scientificConsultation);
        }

        // POST: ScientificConsultation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CandidateName,DissertationTitle")] ScientificConsultation scientificConsultation)
        {
            if (id != scientificConsultation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scientificConsultation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScientificConsultationExists(scientificConsultation.Id))
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
            return View(scientificConsultation);
        }

        // GET: ScientificConsultation/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scientificConsultation = await _context.ScientificConsultations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scientificConsultation == null)
            {
                return NotFound();
            }

            return View(scientificConsultation);
        }

        // POST: ScientificConsultation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var scientificConsultation = await _context.ScientificConsultations.FindAsync(id);
            _context.ScientificConsultations.Remove(scientificConsultation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScientificConsultationExists(Guid id)
        {
            return _context.ScientificConsultations.Any(e => e.Id == id);
        }
    }
}
