using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;

namespace ScientificReport.Controllers
{
	public class ReportController : Controller
	{
		private readonly ScientificReportDbContext _context;

		public ReportController(ScientificReportDbContext context)
		{
			_context = context;
		}

		// GET: Report
		public async Task<IActionResult> Index()
		{
			return View(); //await _context.TeacherReports.ToListAsync());
		}

		// GET: Report/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			/*
			var report = await _context.TeacherReports
				.FirstOrDefaultAsync(m => m.Id == id);
			if (report == null)
			{
				return NotFound();
			}
			*/

			return View();//report);
		}

		// GET: Report/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Report/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Title,Created")] Report report)
		{
			if (ModelState.IsValid)
			{
				_context.Add(report);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(report);
		}

		// GET: Report/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			/*
			var report = await _context.TeacherReports.FindAsync(id);
			if (report == null)
			{
				return NotFound();
			}
			*/

			return View();//report);
		}

		// POST: Report/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Created")] Report report)
		{
			if (id != report.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(report);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ReportExists(report.Id))
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
			return View(report);
		}

		// GET: Report/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			/*
			var report = await _context.TeacherReports
				.FirstOrDefaultAsync(m => m.Id == id);
			if (report == null)
			{
				return NotFound();
			}
			*/

			return View(); //report);
		}

		// POST: Report/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
		//	var report = await _context.TeacherReports.FindAsync(id);
		//	_context.TeacherReports.Remove(report);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ReportExists(int id)
		{
			return false;//_context.TeacherReports.Any(e => e.Id == id);
		}
	}
}
