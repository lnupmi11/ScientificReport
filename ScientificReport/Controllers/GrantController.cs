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
	public class GrantController : Controller
	{
		private readonly ScientificReportDbContext _context;

		public GrantController(ScientificReportDbContext context)
		{
			_context = context;
		}

		// GET: Grant
		public async Task<IActionResult> Index()
		{
			return View(await _context.Grants.ToListAsync());
		}

		// GET: Grant/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var grant = await _context.Grants
				.FirstOrDefaultAsync(m => m.Id == id);
			if (grant == null)
			{
				return NotFound();
			}

			return View(grant);
		}

		// GET: Grant/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Grant/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id")] Grant grant)
		{
			if (ModelState.IsValid)
			{
				grant.Id = Guid.NewGuid();
				_context.Add(grant);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(grant);
		}

		// GET: Grant/Edit/5
		public async Task<IActionResult> Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var grant = await _context.Grants.FindAsync(id);
			if (grant == null)
			{
				return NotFound();
			}
			return View(grant);
		}

		// POST: Grant/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id")] Grant grant)
		{
			if (id != grant.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(grant);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!GrantExists(grant.Id))
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
			return View(grant);
		}

		// GET: Grant/Delete/5
		public async Task<IActionResult> Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var grant = await _context.Grants
				.FirstOrDefaultAsync(m => m.Id == id);
			if (grant == null)
			{
				return NotFound();
			}

			return View(grant);
		}

		// POST: Grant/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var grant = await _context.Grants.FindAsync(id);
			_context.Grants.Remove(grant);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool GrantExists(Guid id)
		{
			return _context.Grants.Any(e => e.Id == id);
		}
	}
}
