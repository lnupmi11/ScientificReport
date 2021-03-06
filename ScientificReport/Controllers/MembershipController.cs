using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.Membership;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class MembershipController : Controller
	{
		private readonly IMembershipService _membershipService;
		private readonly IDepartmentService _departmentService;
		private readonly IUserProfileService _userProfileService;

		public MembershipController(
			IMembershipService membershipService,
			IDepartmentService departmentService,
			IUserProfileService userProfileService
		)
		{
			_membershipService = membershipService;
			_departmentService = departmentService;
			_userProfileService = userProfileService;
		}

		// GET: Membership
		public IActionResult Index(MembershipIndexModel model)
		{
			model.Memberships = _membershipService.GetPageByRole(model.CurrentPage, model.PageSize, User);
			model.Count = _membershipService.GetCountByRole(User);
			return View(model);
		}

		// GET: Membership/Details/{id}
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var membership = _membershipService.GetById(id.Value);
			if (membership == null)
			{
				return NotFound();
			}

			if (!UserHasPermission(membership))
			{
				return Forbid();
			}

			return View(membership);
		}

		// GET: Membership/Create
		public IActionResult Create() => View();

		// POST: Membership/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(MembershipModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			model.User = _userProfileService.Get(User);
			_membershipService.CreateItem(model);

			return RedirectToAction(nameof(Index));
		}

		// GET: Membership/Edit/{id}
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var membership = _membershipService.GetById(id.Value);
			if (membership == null)
			{
				return NotFound();
			}
			
			if (!UserHasPermission(membership))
			{
				return Forbid();
			}

			return View(new MembershipEditModel(membership));
		}

		// POST: Membership/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, MembershipEditModel model)
		{
			if (id != model.Id || !_membershipService.Exists(id))
			{
				return NotFound();
			}
			
			if (!UserHasPermission(_membershipService.GetById(id)))
			{
				return Forbid();
			}
			
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			
			_membershipService.UpdateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: Membership/Delete/{id}
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var membership = _membershipService.GetById(id.Value);
			if (membership == null)
			{
				return NotFound();
			}

			if (!UserHasPermission(membership))
			{
				return Forbid();
			}

			return View(membership);
		}

		// POST: Membership/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			if (!_membershipService.Exists(id))
			{
				return NotFound();
			}
			
			if (!UserHasPermission(_membershipService.GetById(id)))
			{
				return Forbid();
			}

			_membershipService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
		
		private bool UserHasPermission(Membership membership)
		{
			var user = _userProfileService.Get(User);
			var department = _departmentService.Get(d => d.Staff.Contains(user));
			return PageHelpers.IsAdmin(User) ||
			       PageHelpers.IsHeadOfDepartment(User) &&
			       department.Staff.Contains(membership.User) ||
			       membership.User.Id == user.Id;
		}
	}
}
