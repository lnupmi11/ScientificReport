using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models;
using ScientificReport.DTO.Models.Grant;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class GrantController : Controller
	{
		private readonly IGrantService _grantService;
		private readonly IUserProfileService _userProfileService;
		private readonly IDepartmentService _departmentService;

		public GrantController(
			IGrantService grantService,
			IUserProfileService userProfileService,
			IDepartmentService departmentService
		)
		{
			_grantService = grantService;
			_userProfileService = userProfileService;
			_departmentService = departmentService;
		}

		// GET: Grant
		public IActionResult Index(GrantIndexModel model)
		{
			model.Grants = _grantService.GetPageByRole(model.CurrentPage, model.PageSize, User);
			model.Count = _grantService.GetCountByRole(User);
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

			if (!UserHasPermission(grant))
			{
				return Forbid();
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
			_grantService.AddUser(_grantService.Get(g => g.Info == model.Info), _userProfileService.Get(User));
			
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
			
			if (!UserHasPermission(grant))
			{
				return Forbid();
			}

			return View(new GrantEditModel(grant)
			{
				Users = _userProfileService.GetAll(),
				Authors = _grantService.GetUsers(grant.Id)
			});
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

			var grant = _grantService.GetById(id);
			if (!UserHasPermission(grant))
			{
				return Forbid();
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
			
			if (!UserHasPermission(grant))
			{
				return Forbid();
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
			
			if (!UserHasPermission(_grantService.GetById(id)))
			{
				return Forbid();
			}

			_grantService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
		
		// POST: Grant/AddAuthor/{id}
		[HttpPost]
		public IActionResult AddAuthor(Guid id, [FromBody] UpdateUserRequest request)
		{
			if (!_grantService.Exists(id))
			{
				return NotFound();
			}

			var grant = _grantService.GetById(id);
			if (!UserHasPermission(grant))
			{
				return Forbid();
			}
			
			_grantService.AddUser(grant, _userProfileService.GetById(request.UserId));
			return Json(ApiResponse.Ok);
		}

		// POST: Grant/DeleteAuthor/{id}
		[HttpPost]
		public IActionResult DeleteAuthor(Guid id, [FromBody] UpdateUserRequest request)
		{
			if (!_grantService.Exists(id))
			{
				return NotFound();
			}
			
			var grant = _grantService.GetById(id);
			if (!UserHasPermission(grant))
			{
				return Forbid();
			}
			
			_grantService.RemoveUser(grant, _userProfileService.GetById(request.UserId));
			return Json(ApiResponse.Ok);
		}

		private bool UserHasPermission(Grant grant)
		{
			var user = _userProfileService.Get(User);
			var department = _departmentService.Get(d => d.Staff.Contains(user));
			return PageHelpers.IsAdmin(User) ||
			       PageHelpers.IsHeadOfDepartment(User) &&
			       _grantService.GetUsers(grant.Id).Any(p => department.Staff.Contains(p)) ||
			       _grantService.GetUsers(grant.Id).Contains(user);
		}
	}
}
