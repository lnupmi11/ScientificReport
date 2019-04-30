using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.Entities.Reports;
using ScientificReport.DTO.Models.TeacherReport;

namespace ScientificReport.Controllers
{
	public class TeacherReportController : Controller
	{
		private readonly ITeacherReportService _teacherReportService;
		private readonly IUserProfileService _userProfileService;

		public TeacherReportController(ITeacherReportService teacherReportService, IUserProfileService userProfileService)
		{
			_teacherReportService = teacherReportService;
			_userProfileService = userProfileService;
		}

		// GET: Report
		public IActionResult Index()
		{
			return View(_teacherReportService.GetAll());
		}

		// GET: Report/Details/{id}
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var report = _teacherReportService.GetById(id.Value);
			if (report == null)
			{
				return NotFound();
			}

			return View(report);
		}

		// GET: Report/Create
		public IActionResult Create()
		{
			var viewModel = new TeacherReportCreateViewModel
			{
				Users = _userProfileService.GetAll()
			};

			return View(viewModel);
		}

		// POST: Report/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Guid UserId)
		{
			if (!ModelState.IsValid)
			{
				var viewModel = new TeacherReportCreateViewModel
				{
					Users = _userProfileService.GetAll()
				};

				ModelState.AddModelError("key", "Failed to create");
				return View(viewModel);
			}

			var report = new TeacherReport
			{
				Teacher = _userProfileService.GetById(UserId)
			};

			_teacherReportService.CreateItem(report);
			return RedirectToAction(nameof(Index));
		}	

		// GET: Report/Edit/{id}
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var report = _teacherReportService.GetById(id.Value);
			if (report == null)
			{
				return NotFound();
			}

			return View(report);
		}

		// POST: Report/Edit/{id}
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, [Bind("Id,Created")] TeacherReport report)
		{
			if (id != report.Id)
			{
				return NotFound();
			}

			if (!ModelState.IsValid) return View(report);
			try
			{
				_teacherReportService.UpdateItem(report);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_teacherReportService.Exists(report.Id))
					return NotFound();

				throw;
			}

			return RedirectToAction(nameof(Index));
		}

		// GET: Report/Delete/{id}
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var report = _teacherReportService.GetById(id.Value);
			if (report == null)
			{
				return NotFound();
			}

			return View(report);
		}

		// POST: Report/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			_teacherReportService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
