using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.ScientificConsultation;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class ScientificConsultationController : Controller
	{
		private readonly IScientificConsultationService _scientificConsultationService;
		private readonly IUserProfileService _userProfileService;
		private readonly IDepartmentService _departmentService;

		public ScientificConsultationController(
			IScientificConsultationService scientificConsultationService,
			IUserProfileService userProfileService,
			IDepartmentService departmentService
		)
		{
			_scientificConsultationService = scientificConsultationService;
			_userProfileService = userProfileService;
			_departmentService = departmentService;
		}

		// GET: ScientificConsultation
		public IActionResult Index(ScientificConsultationIndexModel model)
		{
			model.ScientificConsultations = _scientificConsultationService.GetPageByRole(model.CurrentPage, model.PageSize, User);
			model.Count = _scientificConsultationService.GetCountByRole(User);
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

			if (!UserHasPermission(scientificConsultation))
			{
				return Forbid();
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

			model.Guide = _userProfileService.Get(User);
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
			
			if (!UserHasPermission(scientificConsultation))
			{
				return Forbid();
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
			
			if (!UserHasPermission(_scientificConsultationService.GetById(id)))
			{
				return Forbid();
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
			
			if (!UserHasPermission(scientificConsultation))
			{
				return Forbid();
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
			
			if (!UserHasPermission(_scientificConsultationService.GetById(id)))
			{
				return Forbid();
			}

			_scientificConsultationService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}

		private bool UserHasPermission(ScientificConsultation scientificConsultation)
		{
			var user = _userProfileService.Get(User);
			var department = _departmentService.Get(d => d.Staff.Contains(user));
			return PageHelpers.IsAdmin(User) ||
			       PageHelpers.IsHeadOfDepartment(User) &&
			       department.Staff.Contains(scientificConsultation.Guide) ||
			       scientificConsultation.Guide.Id == user.Id;
		}
	}
}
