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
    public class AuthorsPatentLicenseActivitiesController : Controller
    {
        private readonly ScientificReportDbContext _context;

        public AuthorsPatentLicenseActivitiesController(ScientificReportDbContext context)
        {
            _context = context;
        }

        // GET: AuthorsPatentLicenseActivities
        public async Task<IActionResult> Index()
        {
            return View(await _context.AuthorsPatentLicenseActivities.ToListAsync());
        }

        // GET: AuthorsPatentLicenseActivities/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorsPatentLicenseActivities = await _context.AuthorsPatentLicenseActivities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorsPatentLicenseActivities == null)
            {
                return NotFound();
            }

            return View(authorsPatentLicenseActivities);
        }

        // GET: AuthorsPatentLicenseActivities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AuthorsPatentLicenseActivities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AuthorId,PatentLicenseActivityId")] AuthorsPatentLicenseActivities authorsPatentLicenseActivities)
        {
            if (ModelState.IsValid)
            {
                authorsPatentLicenseActivities.Id = Guid.NewGuid();
                _context.Add(authorsPatentLicenseActivities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(authorsPatentLicenseActivities);
        }

        // GET: AuthorsPatentLicenseActivities/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorsPatentLicenseActivities = await _context.AuthorsPatentLicenseActivities.FindAsync(id);
            if (authorsPatentLicenseActivities == null)
            {
                return NotFound();
            }
            return View(authorsPatentLicenseActivities);
        }

        // POST: AuthorsPatentLicenseActivities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,AuthorId,PatentLicenseActivityId")] AuthorsPatentLicenseActivities authorsPatentLicenseActivities)
        {
            if (id != authorsPatentLicenseActivities.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorsPatentLicenseActivities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorsPatentLicenseActivitiesExists(authorsPatentLicenseActivities.Id))
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
            return View(authorsPatentLicenseActivities);
        }

        // GET: AuthorsPatentLicenseActivities/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorsPatentLicenseActivities = await _context.AuthorsPatentLicenseActivities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (authorsPatentLicenseActivities == null)
            {
                return NotFound();
            }

            return View(authorsPatentLicenseActivities);
        }

        // POST: AuthorsPatentLicenseActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var authorsPatentLicenseActivities = await _context.AuthorsPatentLicenseActivities.FindAsync(id);
            _context.AuthorsPatentLicenseActivities.Remove(authorsPatentLicenseActivities);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorsPatentLicenseActivitiesExists(Guid id)
        {
            return _context.AuthorsPatentLicenseActivities.Any(e => e.Id == id);
        }
    }
}
