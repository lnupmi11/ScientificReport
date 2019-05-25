using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.Grant;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class GrantController : Controller
	{
		private readonly IGrantService _grantService;

		public GrantController(IGrantService grantService)
		{
			_grantService = grantService;
		}

		// GET: Grant
		public IActionResult Index(GrantIndexModel model)
		{
			model.Grants = _grantService.GetPage(model.CurrentPage, model.PageSize);
			model.Count = _grantService.GetCount();
			return View(model);
		}

		// GET: Grant/Details/{id}
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var grant = _grantService.GetById(id.Value);
			if (grant == null)
			{
				return NotFound();
			}

			return View(grant);
		}

		// GET: Grant/Create
		public IActionResult Create() => View();

		// POST: Grant/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(GrantModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			_grantService.CreateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: Grant/Edit/{id}
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var grant = _grantService.GetById(id.Value);
			if (grant == null)
			{
				return NotFound();
			}

			return View(new GrantEditModel(grant));
		}

		// POST: Grant/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, GrantEditModel model)
		{
			if (id != model.Id || !_grantService.Exists(id))
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_grantService.UpdateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: Grant/Delete/{id}
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var grant = _grantService.GetById(id.Value);
			if (grant == null)
			{
				return NotFound();
			}

			return View(grant);
		}

		// POST: Grant/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			if (!_grantService.Exists(id))
			{
				return NotFound();
			}

			_grantService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
