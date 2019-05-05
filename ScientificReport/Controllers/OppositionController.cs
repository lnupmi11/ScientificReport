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
    public class OppositionController : Controller
    {
        private readonly ScientificReportDbContext _context;

        public OppositionController(ScientificReportDbContext context)
        {
            _context = context;
        }

        // GET: Opposition
        public async Task<IActionResult> Index()
        {
            return View(await _context.Oppositions.ToListAsync());
        }

        // GET: Opposition/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opposition = await _context.Oppositions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (opposition == null)
            {
                return NotFound();
            }

            return View(opposition);
        }

        // GET: Opposition/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Opposition/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,About,DateOfOpposition")] Opposition opposition)
        {
            if (ModelState.IsValid)
            {
                opposition.Id = Guid.NewGuid();
                _context.Add(opposition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(opposition);
        }

        // GET: Opposition/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opposition = await _context.Oppositions.FindAsync(id);
            if (opposition == null)
            {
                return NotFound();
            }
            return View(opposition);
        }

        // POST: Opposition/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,About,DateOfOpposition")] Opposition opposition)
        {
            if (id != opposition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opposition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OppositionExists(opposition.Id))
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
            return View(opposition);
        }

        // GET: Opposition/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opposition = await _context.Oppositions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (opposition == null)
            {
                return NotFound();
            }

            return View(opposition);
        }

        // POST: Opposition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var opposition = await _context.Oppositions.FindAsync(id);
            _context.Oppositions.Remove(opposition);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OppositionExists(Guid id)
        {
            return _context.Oppositions.Any(e => e.Id == id);
        }
    }
}
