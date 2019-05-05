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
    public class ApplicantsPatentLicenseActivitiesController : Controller
    {
        private readonly ScientificReportDbContext _context;

        public ApplicantsPatentLicenseActivitiesController(ScientificReportDbContext context)
        {
            _context = context;
        }

        // GET: ApplicantsPatentLicenseActivities
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicantsPatentLicenseActivities.ToListAsync());
        }

        // GET: ApplicantsPatentLicenseActivities/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantsPatentLicenseActivities = await _context.ApplicantsPatentLicenseActivities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantsPatentLicenseActivities == null)
            {
                return NotFound();
            }

            return View(applicantsPatentLicenseActivities);
        }

        // GET: ApplicantsPatentLicenseActivities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicantsPatentLicenseActivities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicantId,PatentLicenseActivityId")] ApplicantsPatentLicenseActivities applicantsPatentLicenseActivities)
        {
            if (ModelState.IsValid)
            {
                applicantsPatentLicenseActivities.Id = Guid.NewGuid();
                _context.Add(applicantsPatentLicenseActivities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicantsPatentLicenseActivities);
        }

        // GET: ApplicantsPatentLicenseActivities/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantsPatentLicenseActivities = await _context.ApplicantsPatentLicenseActivities.FindAsync(id);
            if (applicantsPatentLicenseActivities == null)
            {
                return NotFound();
            }
            return View(applicantsPatentLicenseActivities);
        }

        // POST: ApplicantsPatentLicenseActivities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ApplicantId,PatentLicenseActivityId")] ApplicantsPatentLicenseActivities applicantsPatentLicenseActivities)
        {
            if (id != applicantsPatentLicenseActivities.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicantsPatentLicenseActivities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantsPatentLicenseActivitiesExists(applicantsPatentLicenseActivities.Id))
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
            return View(applicantsPatentLicenseActivities);
        }

        // GET: ApplicantsPatentLicenseActivities/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantsPatentLicenseActivities = await _context.ApplicantsPatentLicenseActivities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantsPatentLicenseActivities == null)
            {
                return NotFound();
            }

            return View(applicantsPatentLicenseActivities);
        }

        // POST: ApplicantsPatentLicenseActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicantsPatentLicenseActivities = await _context.ApplicantsPatentLicenseActivities.FindAsync(id);
            _context.ApplicantsPatentLicenseActivities.Remove(applicantsPatentLicenseActivities);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantsPatentLicenseActivitiesExists(Guid id)
        {
            return _context.ApplicantsPatentLicenseActivities.Any(e => e.Id == id);
        }
    }
}
