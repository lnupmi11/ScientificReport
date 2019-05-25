using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.ScientificInternship;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class ScientificInternshipController : Controller
	{
		private readonly IScientificInternshipService _scientificInternshipService;

		public ScientificInternshipController(IScientificInternshipService scientificInternshipService)
		{
			_scientificInternshipService = scientificInternshipService;
		}

		// GET: ScientificInternship
		public IActionResult Index(ScientificInternshipIndexModel model)
		{
			model.ScientificInternships = _scientificInternshipService.GetPage(model.CurrentPage, model.PageSize);
			model.Count = _scientificInternshipService.GetCount();
			return View(model);
		}

		// GET: ScientificInternship/Details/{id}
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var scientificInternship = _scientificInternshipService.GetById(id.Value);
			if (scientificInternship == null)
			{
				return NotFound();
			}

			return View(scientificInternship);
		}

		// GET: ScientificInternship/Create
		public IActionResult Create() => View();

		// POST: ScientificInternship/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ScientificInternshipModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			_scientificInternshipService.CreateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: ScientificInternship/Edit/{id}
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var scientificInternship = _scientificInternshipService.GetById(id.Value);
			if (scientificInternship == null)
			{
				return NotFound();
			}

			return View(new ScientificInternshipEditModel(scientificInternship));
		}

		// POST: ScientificInternship/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, ScientificInternshipEditModel model)
		{
			if (id != model.Id || !_scientificInternshipService.Exists(id))
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_scientificInternshipService.UpdateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: ScientificInternship/Delete/{id}
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var scientificInternship = _scientificInternshipService.GetById(id.Value);
			if (scientificInternship == null)
			{
				return NotFound();
			}

			return View(scientificInternship);
		}

		// POST: ScientificInternship/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			if (!_scientificInternshipService.Exists(id))
			{
				return NotFound();
			}

			_scientificInternshipService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
