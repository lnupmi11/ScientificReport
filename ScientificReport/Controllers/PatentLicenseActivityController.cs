using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.PatentLicenseActivity;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class PatentLicenseActivityController : Controller
	{
		private readonly IPatentLicenseActivityService _patentLicenseActivityService;

		public PatentLicenseActivityController(IPatentLicenseActivityService patentLicenseActivityService)
		{
			_patentLicenseActivityService = patentLicenseActivityService;
		}

		// GET: PatentLicenseActivity
		public IActionResult Index(PatentLicenseActivityIndexModel model)
		{
			model.PatentLicenseActivities = _patentLicenseActivityService.GetPage(model.CurrentPage, model.PageSize);
			model.Count = _patentLicenseActivityService.GetCount();
			return View(model);
		}

		// GET: PatentLicenseActivity/Details/{id}
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var patentLicenseActivity = _patentLicenseActivityService.GetById(id.Value);
			if (patentLicenseActivity == null)
			{
				return NotFound();
			}

			return View(patentLicenseActivity);
		}

		// GET: PatentLicenseActivity/Create
		public IActionResult Create() => View();

		// POST: PatentLicenseActivity/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(PatentLicenseActivityModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			_patentLicenseActivityService.CreateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: PatentLicenseActivity/Edit/{id}
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var patentLicenseActivity = _patentLicenseActivityService.GetById(id.Value);
			if (patentLicenseActivity == null)
			{
				return NotFound();
			}

			return View(new PatentLicenseActivityEditModel(patentLicenseActivity));
		}

		// POST: PatentLicenseActivity/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, PatentLicenseActivityEditModel model)
		{
			if (id != model.Id || !_patentLicenseActivityService.Exists(id))
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_patentLicenseActivityService.UpdateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: PatentLicenseActivity/Delete/{id}
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var patentLicenseActivity = _patentLicenseActivityService.GetById(id.Value);
			if (patentLicenseActivity == null)
			{
				return NotFound();
			}

			return View(patentLicenseActivity);
		}

		// POST: PatentLicenseActivity/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			if (!_patentLicenseActivityService.Exists(id))
			{
				return NotFound();
			}

			_patentLicenseActivityService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
