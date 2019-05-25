using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.ScientificConsultation;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class ScientificConsultationController : Controller
	{
		private readonly IScientificConsultationService _scientificConsultationService;

		public ScientificConsultationController(IScientificConsultationService scientificConsultationService)
		{
			_scientificConsultationService = scientificConsultationService;
		}

		// GET: ScientificConsultation
		public IActionResult Index(ScientificConsultationIndexModel model)
		{
			model.ScientificConsultations = _scientificConsultationService.GetPage(model.CurrentPage, model.PageSize);
			model.Count = _scientificConsultationService.GetCount();
			return View(model);
		}

		// GET: ScientificConsultation/Details/{id}
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var scientificConsultation = _scientificConsultationService.GetById(id.Value);
			if (scientificConsultation == null)
			{
				return NotFound();
			}

			return View(scientificConsultation);
		}

		// GET: ScientificConsultation/Create
		public IActionResult Create() => View();

		// POST: ScientificConsultation/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ScientificConsultationModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			_scientificConsultationService.CreateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: ScientificConsultation/Edit/{id}
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var scientificConsultation = _scientificConsultationService.GetById(id.Value);
			if (scientificConsultation == null)
			{
				return NotFound();
			}

			return View(new ScientificConsultationEditModel(scientificConsultation));
		}

		// POST: ScientificConsultation/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, ScientificConsultationEditModel model)
		{
			if (id != model.Id || !_scientificConsultationService.Exists(id))
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_scientificConsultationService.UpdateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: ScientificConsultation/Delete/{id}
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var scientificConsultation = _scientificConsultationService.GetById(id.Value);
			if (scientificConsultation == null)
			{
				return NotFound();
			}

			return View(scientificConsultation);
		}

		// POST: ScientificConsultation/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			if (!_scientificConsultationService.Exists(id))
			{
				return NotFound();
			}

			_scientificConsultationService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
