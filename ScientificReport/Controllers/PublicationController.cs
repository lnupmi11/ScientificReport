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
	public class PublicationController : Controller
	{
		private readonly ScientificReportDbContext _context;

		public PublicationController(ScientificReportDbContext context)
		{
			_context = context;
		}

		// GET: Publication
		public async Task<IActionResult> Index()
		{
			return View(await _context.Publications.ToListAsync());
		}

		// GET: Publication/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var publication = await _context.Publications
				.FirstOrDefaultAsync(m => m.Id == id);
			if (publication == null)
			{
				return NotFound();
			}

			return View(publication);
		}

		// GET: Publication/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Publication/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Type,Title,Specification,PublishingPlace,PublishingHouseName,PublishingYear,PagesAmount,IsPrintCanceled,IsRecommendedToPrint,CreatedAt,LastEditAt")] Publication publication)
		{
			if (ModelState.IsValid)
			{
				publication.Id = Guid.NewGuid();
				_context.Add(publication);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(publication);
		}

		// GET: Publication/Edit/5
		public async Task<IActionResult> Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var publication = await _context.Publications.FindAsync(id);
			if (publication == null)
			{
				return NotFound();
			}
			return View(publication);
		}

		// POST: Publication/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,Type,Title,Specification,PublishingPlace,PublishingHouseName,PublishingYear,PagesAmount,IsPrintCanceled,IsRecommendedToPrint,CreatedAt,LastEditAt")] Publication publication)
		{
			if (id != publication.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(publication);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PublicationExists(publication.Id))
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
			return View(publication);
		}

		// GET: Publication/Delete/5
		public async Task<IActionResult> Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var publication = await _context.Publications
				.FirstOrDefaultAsync(m => m.Id == id);
			if (publication == null)
			{
				return NotFound();
			}

			return View(publication);
		}

		// POST: Publication/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var publication = await _context.Publications.FindAsync(id);
			_context.Publications.Remove(publication);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool PublicationExists(Guid id)
		{
			return _context.Publications.Any(e => e.Id == id);
		}
	}
}
