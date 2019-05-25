using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.PostgraduateDissertationGuidance;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class PostgraduateDissertationGuidanceController : Controller
	{
		private readonly IPostgraduateDissertationGuidanceService _postgraduateDissertationGuidanceService;

		public PostgraduateDissertationGuidanceController(IPostgraduateDissertationGuidanceService postgraduateDissertationGuidanceService)
		{
			_postgraduateDissertationGuidanceService = postgraduateDissertationGuidanceService;
		}

		// GET: PostgraduateDissertationGuidance
		public IActionResult Index(PostgraduateDissertationGuidanceIndexModel model)
		{
			model.PostgraduateDissertationGuidances = _postgraduateDissertationGuidanceService.GetPage(model.CurrentPage, model.PageSize);
			model.Count = _postgraduateDissertationGuidanceService.GetCount();
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

			_postgraduateDissertationGuidanceService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
