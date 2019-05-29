using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.PostgraduateGuidance;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class PostgraduateGuidanceController : Controller
	{
		private readonly IPostgraduateGuidanceService _postgraduateGuidanceService;
		private readonly IUserProfileService _userProfileService;
		private readonly IDepartmentService _departmentService;

		public PostgraduateGuidanceController(
			IPostgraduateGuidanceService postgraduateGuidanceService,
			IUserProfileService userProfileService,
			IDepartmentService departmentService
		)
		{
			_postgraduateGuidanceService = postgraduateGuidanceService;
			_userProfileService = userProfileService;
			_departmentService = departmentService;
		}

		// GET: PostgraduateGuidance
		public IActionResult Index(PostgraduateGuidanceIndexModel model)
		{
			model.PostgraduateGuidances = _postgraduateGuidanceService.GetPageByRole(model.CurrentPage, model.PageSize, User);
			model.Count = _postgraduateGuidanceService.GetCountByRole(User);
			return View(model);
		}

		// GET: PostgraduateGuidance/Details/{id}
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var postgraduateGuidance = _postgraduateGuidanceService.GetById(id.Value);
			if (postgraduateGuidance == null)
			{
				return NotFound();
			}

			if (!UserHasPermission(postgraduateGuidance))
			{
				return Forbid();
			}

			return View(postgraduateGuidance);
		}

		// GET: PostgraduateGuidance/Create
		public IActionResult Create() => View();

		// POST: PostgraduateGuidance/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(PostgraduateGuidanceModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			model.Guide = _userProfileService.Get(User);
			_postgraduateGuidanceService.CreateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: PostgraduateGuidance/Edit/{id}
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var postgraduateGuidance = _postgraduateGuidanceService.GetById(id.Value);
			if (postgraduateGuidance == null)
			{
				return NotFound();
			}
			
			if (!UserHasPermission(postgraduateGuidance))
			{
				return Forbid();
			}

			return View(new PostgraduateGuidanceEditModel(postgraduateGuidance));
		}

		// POST: PostgraduateGuidance/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, PostgraduateGuidanceEditModel model)
		{
			if (id != model.Id || !_postgraduateGuidanceService.Exists(id))
			{
				return NotFound();
			}

			if (!UserHasPermission(_postgraduateGuidanceService.GetById(id)))
			{
				return Forbid();
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_postgraduateGuidanceService.UpdateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: PostgraduateGuidance/Delete/{id}
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var postgraduateGuidance = _postgraduateGuidanceService.GetById(id.Value);
			if (postgraduateGuidance == null)
			{
				return NotFound();
			}
			
			if (!UserHasPermission(postgraduateGuidance))
			{
				return Forbid();
			}

			return View(postgraduateGuidance);
		}

		// POST: PostgraduateGuidance/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			if (!_postgraduateGuidanceService.Exists(id))
			{
				return NotFound();
			}
			
			if (!UserHasPermission(_postgraduateGuidanceService.GetById(id)))
			{
				return Forbid();
			}

			_postgraduateGuidanceService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
		
		private bool UserHasPermission(PostgraduateGuidance guidance)
		{
			var user = _userProfileService.Get(User);
			var department = _departmentService.Get(d => d.Staff.Contains(user));
			return PageHelpers.IsAdmin(User) ||
			       PageHelpers.IsHeadOfDepartment(User) &&
			       department.Staff.Contains(guidance.Guide) ||
			       guidance.Guide.Id == user.Id;
		}
	}
}
