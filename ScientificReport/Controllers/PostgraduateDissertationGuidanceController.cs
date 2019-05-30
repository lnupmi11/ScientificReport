using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.PostgraduateDissertationGuidance;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class PostgraduateDissertationGuidanceController : Controller
	{
		private readonly IPostgraduateDissertationGuidanceService _postgraduateDissertationGuidanceService;
		private readonly IUserProfileService _userProfileService;
		private readonly IDepartmentService _departmentService;

		public PostgraduateDissertationGuidanceController(
			IPostgraduateDissertationGuidanceService postgraduateDissertationGuidanceService,
			IUserProfileService userProfileService,
			IDepartmentService departmentService
		)
		{
			_postgraduateDissertationGuidanceService = postgraduateDissertationGuidanceService;
			_userProfileService = userProfileService;
			_departmentService = departmentService;
		}

		// GET: PostgraduateDissertationGuidance
		public IActionResult Index(PostgraduateDissertationGuidanceIndexModel model)
		{
			model.PostgraduateDissertationGuidances = _postgraduateDissertationGuidanceService.GetPageByRole(model.CurrentPage, model.PageSize, User);
			model.Count = _postgraduateDissertationGuidanceService.GetCountByRole(User);
			return View(model);
		}

		// GET: PostgraduateDissertationGuidance/Details/{id}
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var postgraduateDissertationGuidance = _postgraduateDissertationGuidanceService.GetById(id.Value);
			if (postgraduateDissertationGuidance == null)
			{
				return NotFound();
			}

			if (!UserHasPermission(postgraduateDissertationGuidance))
			{
				return Forbid();
			}

			return View(postgraduateDissertationGuidance);
		}

		// GET: PostgraduateDissertationGuidance/Create
		public IActionResult Create() => View();

		// POST: PostgraduateDissertationGuidance/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(PostgraduateDissertationGuidanceModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			model.Guide = _userProfileService.Get(User);
			_postgraduateDissertationGuidanceService.CreateItem(model);
			
			return RedirectToAction(nameof(Index));
		}

		// GET: PostgraduateDissertationGuidance/Edit/{id}
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var postgraduateDissertationGuidance = _postgraduateDissertationGuidanceService.GetById(id.Value);
			if (postgraduateDissertationGuidance == null)
			{
				return NotFound();
			}
			
			if (!UserHasPermission(postgraduateDissertationGuidance))
			{
				return Forbid();
			}

			return View(new PostgraduateDissertationGuidanceEditModel(postgraduateDissertationGuidance));
		}

		// POST: PostgraduateDissertationGuidance/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, PostgraduateDissertationGuidanceEditModel model)
		{
			if (id != model.Id || !_postgraduateDissertationGuidanceService.Exists(id))
			{
				return NotFound();
			}

			if (!UserHasPermission(_postgraduateDissertationGuidanceService.GetById(id)))
			{
				return Forbid();
			}
			
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_postgraduateDissertationGuidanceService.UpdateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: PostgraduateDissertationGuidance/Delete/{id}
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var postgraduateDissertationGuidance = _postgraduateDissertationGuidanceService.GetById(id.Value);
			if (postgraduateDissertationGuidance == null)
			{
				return NotFound();
			}
			
			if (!UserHasPermission(postgraduateDissertationGuidance))
			{
				return Forbid();
			}

			return View(postgraduateDissertationGuidance);
		}

		// POST: PostgraduateDissertationGuidance/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			if (!_postgraduateDissertationGuidanceService.Exists(id))
			{
				return NotFound();
			}
			
			if (!UserHasPermission(_postgraduateDissertationGuidanceService.GetById(id)))
			{
				return Forbid();
			}

			_postgraduateDissertationGuidanceService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
		
		private bool UserHasPermission(PostgraduateDissertationGuidance guidance)
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
