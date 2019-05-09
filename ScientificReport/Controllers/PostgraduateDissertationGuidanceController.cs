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
	public class PostgraduateDissertationGuidanceController : Controller
	{
		private readonly ScientificReportDbContext _context;

		public PostgraduateDissertationGuidanceController(ScientificReportDbContext context)
		{
			_context = context;
		}

		// GET: PostgraduateDissertationGuidance
		public async Task<IActionResult> Index()
		{
			return View(await _context.PostgraduateDissertationGuidances.ToListAsync());
		}

		// GET: PostgraduateDissertationGuidance/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var postgraduateDissertationGuidance = await _context.PostgraduateDissertationGuidances
				.FirstOrDefaultAsync(m => m.Id == id);
			if (postgraduateDissertationGuidance == null)
			{
				return NotFound();
			}

			return View(postgraduateDissertationGuidance);
		}

		// GET: PostgraduateDissertationGuidance/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: PostgraduateDissertationGuidance/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,PostgraduateName,Dissertation,Speciality,DateDegreeGained,GraduationYear")] PostgraduateDissertationGuidance postgraduateDissertationGuidance)
		{
			if (ModelState.IsValid)
			{
				postgraduateDissertationGuidance.Id = Guid.NewGuid();
				_context.Add(postgraduateDissertationGuidance);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(postgraduateDissertationGuidance);
		}

		// GET: PostgraduateDissertationGuidance/Edit/5
		public async Task<IActionResult> Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var postgraduateDissertationGuidance = await _context.PostgraduateDissertationGuidances.FindAsync(id);
			if (postgraduateDissertationGuidance == null)
			{
				return NotFound();
			}
			return View(postgraduateDissertationGuidance);
		}

		// POST: PostgraduateDissertationGuidance/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, [Bind("Id,PostgraduateName,Dissertation,Speciality,DateDegreeGained,GraduationYear")] PostgraduateDissertationGuidance postgraduateDissertationGuidance)
		{
			if (id != postgraduateDissertationGuidance.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(postgraduateDissertationGuidance);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PostgraduateDissertationGuidanceExists(postgraduateDissertationGuidance.Id))
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
			return View(postgraduateDissertationGuidance);
		}

		// GET: PostgraduateDissertationGuidance/Delete/5
		public async Task<IActionResult> Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var postgraduateDissertationGuidance = await _context.PostgraduateDissertationGuidances
				.FirstOrDefaultAsync(m => m.Id == id);
			if (postgraduateDissertationGuidance == null)
			{
				return NotFound();
			}

			return View(postgraduateDissertationGuidance);
		}

		// POST: PostgraduateDissertationGuidance/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			var postgraduateDissertationGuidance = await _context.PostgraduateDissertationGuidances.FindAsync(id);
			_context.PostgraduateDissertationGuidances.Remove(postgraduateDissertationGuidance);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool PostgraduateDissertationGuidanceExists(Guid id)
		{
			return _context.PostgraduateDissertationGuidances.Any(e => e.Id == id);
		}
	}
}
