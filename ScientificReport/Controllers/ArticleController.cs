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
	public class ArticleController : Controller
	{
		private readonly ScientificReportDbContext _context;

		public ArticleController(ScientificReportDbContext context)
		{
			_context = context;
		}

		// GET: Article
		public async Task<IActionResult> Index()
		{
			return View(await _context.Articles.ToListAsync());
		}

		// GET: Article/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var article = await _context.Articles
				.FirstOrDefaultAsync(m => m.Id == id);
			if (article == null)
			{
				return NotFound();
			}

			return View(article);
		}

		// GET: Article/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Article/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Type,Title,LiabilityInfo,DocumentInfo,PublishingPlace,PublishingHouseName,PublishingYear,IsPeriodical,Number,PagesAmount,IsPrintCanceled,IsRecommendedToPrint")] Article article)
		{
			if (ModelState.IsValid)
			{
				article.Id = Guid.NewGuid();
				_context.Add(article);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(article);
		}

		// GET: Article/Edit/5
		public async Task<IActionResult> Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var article = await _context.Articles.FindAsync(id);
			if (article == null)
			{
				return NotFound();
			}
			return View(article);
		}

		// POST: Article/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,Type,Title,LiabilityInfo,DocumentInfo,PublishingPlace,PublishingHouseName,PublishingYear,IsPeriodical,Number,PagesAmount,IsPrintCanceled,IsRecommendedToPrint")] Article article)
		{
			if (id != article.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(article);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ArticleExists(article.Id))
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
			return View(article);
		}

		// GET: Article/Delete/5
		public async Task<IActionResult> Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var article = await _context.Articles
				.FirstOrDefaultAsync(m => m.Id == id);
			if (article == null)
			{
				return NotFound();
			}

			return View(article);
		}

		// POST: Article/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var article = await _context.Articles.FindAsync(id);
			_context.Articles.Remove(article);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ArticleExists(Guid id)
		{
			return _context.Articles.Any(e => e.Id == id);
		}
	}
}
