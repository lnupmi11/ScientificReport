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
//	[Authorize(Roles = UserProfileRole.Teacher)]
	public class PatentLicenseActivityController : Controller
	{
		private readonly ScientificReportDbContext _context;

		public PatentLicenseActivityController(ScientificReportDbContext context)
		{
			_context = context;
		}

		// GET: PatentLicenseActivity
		public async Task<IActionResult> Index()
		{
			return View(await _context.PatentLicenseActivities.ToListAsync());
		}

		// GET: PatentLicenseActivity/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var patentLicenseActivity = await _context.PatentLicenseActivities
				.FirstOrDefaultAsync(m => m.Id == id);
			if (patentLicenseActivity == null)
			{
				return NotFound();
			}

			return View(patentLicenseActivity);
		}

		// GET: PatentLicenseActivity/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: PatentLicenseActivity/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,Number,DateTime,Type")] PatentLicenseActivity patentLicenseActivity)
		{
			if (ModelState.IsValid)
			{
				patentLicenseActivity.Id = Guid.NewGuid();
				_context.Add(patentLicenseActivity);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(patentLicenseActivity);
		}

		// GET: PatentLicenseActivity/Edit/5
		public async Task<IActionResult> Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var patentLicenseActivity = await _context.PatentLicenseActivities.FindAsync(id);
			if (patentLicenseActivity == null)
			{
				return NotFound();
			}
			return View(patentLicenseActivity);
		}

		// POST: PatentLicenseActivity/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Number,DateTime,Type")] PatentLicenseActivity patentLicenseActivity)
		{
			if (id != patentLicenseActivity.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(patentLicenseActivity);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PatentLicenseActivityExists(patentLicenseActivity.Id))
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
			return View(patentLicenseActivity);
		}

		// GET: PatentLicenseActivity/Delete/5
		public async Task<IActionResult> Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var patentLicenseActivity = await _context.PatentLicenseActivities
				.FirstOrDefaultAsync(m => m.Id == id);
			if (patentLicenseActivity == null)
			{
				return NotFound();
			}

			return View(patentLicenseActivity);
		}

		// POST: PatentLicenseActivity/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var patentLicenseActivity = await _context.PatentLicenseActivities.FindAsync(id);
			_context.PatentLicenseActivities.Remove(patentLicenseActivity);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool PatentLicenseActivityExists(Guid id)
		{
			return _context.PatentLicenseActivities.Any(e => e.Id == id);
		}
	}
}
