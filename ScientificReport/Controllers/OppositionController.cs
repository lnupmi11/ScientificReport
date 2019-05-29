using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.Opposition;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class OppositionController : Controller
	{
		private readonly IOppositionService _oppositionService;
		private readonly IUserProfileService _userProfileService;
		private readonly IDepartmentService _departmentService;

		public OppositionController(
			IOppositionService oppositionService,
			IDepartmentService departmentService,
			IUserProfileService userProfileService
		)
		{
			_oppositionService = oppositionService;
			_userProfileService = userProfileService;
			_departmentService = departmentService;
		}

		// GET: Opposition
		public IActionResult Index(OppositionIndexModel model)
		{
			model.Oppositions = _oppositionService.GetPageByRole(model.CurrentPage, model.PageSize, User);
			model.Count = _oppositionService.GetCountByRole(User);
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

			if (!UserHasPermission(opposition))
			{
				return Forbid();
			}

			return View(opposition);
		}

		// GET: Opposition/Create
		public IActionResult Create() => View();

		// POST: Opposition/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(OppositionModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			model.Opponent = _userProfileService.Get(User);
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
			
			if (!UserHasPermission(opposition))
			{
				return Forbid();
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

			if (!UserHasPermission(_oppositionService.GetById(id)))
			{
				return Forbid();
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
			
			if (!UserHasPermission(opposition))
			{
				return Forbid();
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
			
			if (!UserHasPermission(_oppositionService.GetById(id)))
			{
				return Forbid();
			}
			
			_oppositionService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
		
		private bool UserHasPermission(Opposition opposition)
		{
			var user = _userProfileService.Get(User);
			var department = _departmentService.Get(d => d.Staff.Contains(user));
			return PageHelpers.IsAdmin(User) ||
			       PageHelpers.IsHeadOfDepartment(User) &&
			       department.Staff.Contains(opposition.Opponent) ||
			       opposition.Opponent.Id == user.Id;
		}
	}
}
