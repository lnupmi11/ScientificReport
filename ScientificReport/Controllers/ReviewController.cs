using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.Review;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class ReviewController : Controller
	{
		private readonly IReviewService _reviewService;

		public ReviewController(IReviewService reviewService)
		{
			_reviewService = reviewService;
		}

		// GET: Review
		public IActionResult Index(ReviewIndexModel model)
		{
			model.Reviews = _reviewService.GetPage(model.CurrentPage, model.PageSize);
			model.Count = _reviewService.GetCount();
			return View(model);
		}

		// GET: Review/Details/{id}
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var review = _reviewService.GetById(id.Value);
			if (review == null)
			{
				return NotFound();
			}

			return View(review);
		}

		// GET: Review/Create
		public IActionResult Create() => View();

		// POST: Review/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ReviewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			_reviewService.CreateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: Review/Edit/{id}
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var review = _reviewService.GetById(id.Value);
			if (review == null)
			{
				return NotFound();
			}

			return View(new ReviewEditModel(review));
		}

		// POST: Review/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, ReviewEditModel model)
		{
			if (id != model.Id || !_reviewService.Exists(id))
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_reviewService.UpdateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: Review/Delete/{id}
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var review = _reviewService.GetById(id.Value);
			if (review == null)
			{
				return NotFound();
			}

			return View(review);
		}

		// POST: Review/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			if (!_reviewService.Exists(id))
			{
				return NotFound();
			}

			_reviewService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
