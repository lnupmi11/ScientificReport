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
    public class ReportThesisController : Controller
    {
        private readonly ScientificReportDbContext _context;

        public ReportThesisController(ScientificReportDbContext context)
        {
            _context = context;
        }

        // GET: ReportThesis
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReportTheses.ToListAsync());
        }

        // GET: ReportThesis/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportThesis = await _context.ReportTheses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reportThesis == null)
            {
                return NotFound();
            }

            return View(reportThesis);
        }

        // GET: ReportThesis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReportThesis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Thesis")] ReportThesis reportThesis)
        {
            if (ModelState.IsValid)
            {
                reportThesis.Id = Guid.NewGuid();
                _context.Add(reportThesis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reportThesis);
        }

        // GET: ReportThesis/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportThesis = await _context.ReportTheses.FindAsync(id);
            if (reportThesis == null)
            {
                return NotFound();
            }
            return View(reportThesis);
        }

        // POST: ReportThesis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Thesis")] ReportThesis reportThesis)
        {
            if (id != reportThesis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportThesis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportThesisExists(reportThesis.Id))
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
            return View(reportThesis);
        }

        // GET: ReportThesis/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportThesis = await _context.ReportTheses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reportThesis == null)
            {
                return NotFound();
            }

            return View(reportThesis);
        }

        // POST: ReportThesis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reportThesis = await _context.ReportTheses.FindAsync(id);
            _context.ReportTheses.Remove(reportThesis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportThesisExists(Guid id)
        {
            return _context.ReportTheses.Any(e => e.Id == id);
        }
    }
}
