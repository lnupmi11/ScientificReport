using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.Opposition;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class OppositionController : Controller
	{
		private readonly IOppositionService _oppositionService;

		public OppositionController(IOppositionService oppositionService)
		{
			_oppositionService = oppositionService;
		}

		// GET: Opposition
		public IActionResult Index(OppositionIndexModel model)
		{
			model.Oppositions = _oppositionService.GetPage(model.CurrentPage, model.PageSize);
			model.Count = _oppositionService.GetCount();
			return View(model);
		}

		// GET: Opposition/Details/{id}
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var opposition = _oppositionService.GetById(id.Value);
			if (opposition == null)
			{
				return NotFound();
			}

			return View(opposition);
		}

		// GET: Opposition/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Opposition/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(OppositionModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			_oppositionService.CreateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: Opposition/Edit/{id}
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var opposition = _oppositionService.GetById(id.Value);
			if (opposition == null)
			{
				return NotFound();
			}

			return View(new OppositionEditModel(opposition));
		}

		// POST: Opposition/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, OppositionEditModel model)
		{
			if (id != model.Id || !_oppositionService.Exists(id))
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_oppositionService.UpdateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: Opposition/Delete/{id}
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var opposition = _oppositionService.GetById(id.Value);
			if (opposition == null)
			{
				return NotFound();
			}

			return View(opposition);
		}

		// POST: Opposition/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			if (!_oppositionService.Exists(id))
			{
				return NotFound();
			}
			
			_oppositionService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
