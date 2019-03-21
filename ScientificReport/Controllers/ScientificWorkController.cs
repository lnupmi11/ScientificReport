using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScientificReport.Data;
using ScientificReport.Models;

namespace ScientificReport.Controllers
{
  public class ScientificWorkController : Controller
  {
    private readonly ApplicationDbContext _context;

    public ScientificWorkController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: ScientificWork
    public async Task<IActionResult> Index()
    {
      return View(await _context.ScientificWork.ToListAsync());
    }

    // GET: ScientificWork/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var scientificWork = await _context.ScientificWork
          .FirstOrDefaultAsync(m => m.Id == id);
      if (scientificWork == null)
      {
        return NotFound();
      }

      return View(scientificWork);
    }

    // GET: ScientificWork/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: ScientificWork/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Cypher,Category,Theme,Content")] ScientificWork scientificWork)
    {
      if (ModelState.IsValid)
      {
        _context.Add(scientificWork);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(scientificWork);
    }

    // GET: ScientificWork/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var scientificWork = await _context.ScientificWork.FindAsync(id);
      if (scientificWork == null)
      {
        return NotFound();
      }
      return View(scientificWork);
    }

    // POST: ScientificWork/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Cypher,Category,Theme,Content")] ScientificWork scientificWork)
    {
      if (id != scientificWork.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(scientificWork);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ScientificWorkExists(scientificWork.Id))
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
      return View(scientificWork);
    }

    // GET: ScientificWork/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var scientificWork = await _context.ScientificWork
          .FirstOrDefaultAsync(m => m.Id == id);
      if (scientificWork == null)
      {
        return NotFound();
      }

      return View(scientificWork);
    }

    // POST: ScientificWork/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var scientificWork = await _context.ScientificWork.FindAsync(id);
      _context.ScientificWork.Remove(scientificWork);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool ScientificWorkExists(int id)
    {
      return _context.ScientificWork.Any(e => e.Id == id);
    }
  }
}
