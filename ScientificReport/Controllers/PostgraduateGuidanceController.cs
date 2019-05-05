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
    public class PostgraduateGuidanceController : Controller
    {
        private readonly ScientificReportDbContext _context;

        public PostgraduateGuidanceController(ScientificReportDbContext context)
        {
            _context = context;
        }

        // GET: PostgraduateGuidance
        public async Task<IActionResult> Index()
        {
            return View(await _context.PostgraduateGuidances.ToListAsync());
        }

        // GET: PostgraduateGuidance/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postgraduateGuidance = await _context.PostgraduateGuidances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postgraduateGuidance == null)
            {
                return NotFound();
            }

            return View(postgraduateGuidance);
        }

        // GET: PostgraduateGuidance/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostgraduateGuidance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PostgraduateName,PostgraduateInfo")] PostgraduateGuidance postgraduateGuidance)
        {
            if (ModelState.IsValid)
            {
                postgraduateGuidance.Id = Guid.NewGuid();
                _context.Add(postgraduateGuidance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postgraduateGuidance);
        }

        // GET: PostgraduateGuidance/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postgraduateGuidance = await _context.PostgraduateGuidances.FindAsync(id);
            if (postgraduateGuidance == null)
            {
                return NotFound();
            }
            return View(postgraduateGuidance);
        }

        // POST: PostgraduateGuidance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PostgraduateName,PostgraduateInfo")] PostgraduateGuidance postgraduateGuidance)
        {
            if (id != postgraduateGuidance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postgraduateGuidance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostgraduateGuidanceExists(postgraduateGuidance.Id))
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
            return View(postgraduateGuidance);
        }

        // GET: PostgraduateGuidance/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postgraduateGuidance = await _context.PostgraduateGuidances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postgraduateGuidance == null)
            {
                return NotFound();
            }

            return View(postgraduateGuidance);
        }

        // POST: PostgraduateGuidance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var postgraduateGuidance = await _context.PostgraduateGuidances.FindAsync(id);
            _context.PostgraduateGuidances.Remove(postgraduateGuidance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostgraduateGuidanceExists(Guid id)
        {
            return _context.PostgraduateGuidances.Any(e => e.Id == id);
        }
    }
}
