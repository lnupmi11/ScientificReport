using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.PostgraduateGuidance;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class PostgraduateGuidanceController : Controller
	{
		private readonly IPostgraduateGuidanceService _postgraduateGuidanceService;

		public PostgraduateGuidanceController(IPostgraduateGuidanceService postgraduateGuidanceService)
		{
			_postgraduateGuidanceService = postgraduateGuidanceService;
		}

		// GET: PostgraduateGuidance
		public IActionResult Index(PostgraduateGuidanceIndexModel model)
		{
			model.PostgraduateGuidances = _postgraduateGuidanceService.GetPage(model.CurrentPage, model.PageSize);
			model.Count = _postgraduateGuidanceService.GetCount();
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

			_postgraduateGuidanceService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
