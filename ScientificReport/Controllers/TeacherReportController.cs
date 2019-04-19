using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.Reports;

namespace ScientificReport.Controllers
{
	public class TeacherReportController : Controller
	{
		private readonly TeacherReportService _teacherReport;
		private readonly UserProfileService _users;

		public TeacherReportController(ScientificReportDbContext context)
		{
			_teacherReport = new TeacherReportService(context);
			_users = new UserProfileService(context);
		}

		// GET: Report
		public IActionResult Index()
		{
			return View(_teacherReport.GetAll());
		}

		// GET: Report/Details/5
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var report = _teacherReport.GetById(id.Value);
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
				Users = _users.GetAll()
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
					Users = _users.GetAll()
				};

				ModelState.AddModelError("key", "Failed to create");
				return View(viewModel);
			}

			var report = new TeacherReport
			{
				Teacher = _users.GetById(UserId)
			};

			_teacherReport.CreateItem(report);
			return RedirectToAction(nameof(Index));
		}	

		// GET: Report/Edit/5
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var report = _teacherReport.GetById(id.Value);
			if (report == null)
			{
				return NotFound();
			}

			return View(report);
		}

		// POST: Report/Edit/5
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
				_teacherReport.UpdateItem(report);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_teacherReport.Exists(report.Id))
					return NotFound();

				throw;
			}

			return RedirectToAction(nameof(Index));
		}

		// GET: Report/Delete/5
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var report = _teacherReport.GetById(id.Value);
			if (report == null)
			{
				return NotFound();
			}

			return View(report);
		}

		// POST: Report/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			_teacherReport.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
	}

	public class TeacherReportCreateViewModel
	{
		[Required]
		public string UserId;
		
		public IEnumerable<UserProfile> Users;
	}
}