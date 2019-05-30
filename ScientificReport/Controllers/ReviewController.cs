using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.Review;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class ReviewController : Controller
	{
		private readonly IReviewService _reviewService;
		private readonly IUserProfileService _userProfileService;
		private readonly IDepartmentService _departmentService;
		private readonly IPublicationService _publicationService;

		public ReviewController(
			IReviewService reviewService,
			IUserProfileService userProfileService,
			IDepartmentService departmentService,
			IPublicationService publicationService
		)
		{
			_reviewService = reviewService;
			_userProfileService = userProfileService;
			_departmentService = departmentService;
			_publicationService = publicationService;
		}

		// GET: Review
		public IActionResult Index(ReviewIndexModel model)
		{
			model.Reviews = _reviewService.GetPageByRole(model.CurrentPage, model.PageSize, User);
			model.Count = _reviewService.GetCountByRole(User);
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

			if (!UserHasPermission(review))
			{
				return Forbid();
			}

			return View(review);
		}

		// GET: Review/Create
		public IActionResult Create()
		{
			return View(new ReviewModel
			{
				Publications = _publicationService.GetAll()
			});
		}

		// POST: Review/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ReviewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Publications = _publicationService.GetAll();
				return View(model);
			}

			if (!_publicationService.PublicationExists(model.WorkId))
			{
				return NotFound();
			}

			model.Work = _publicationService.GetById(model.WorkId);
			model.Reviewer = _userProfileService.Get(User);
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
			
			if (!UserHasPermission(review))
			{
				return Forbid();
			}

			return View(new ReviewEditModel(review)
			{
				Publications = _publicationService.GetAll()
			});
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

			if (!UserHasPermission(_reviewService.GetById(id)))
			{
				return Forbid();
			}
			
			if (!ModelState.IsValid)
			{
				model.Publications = _publicationService.GetAll();
				return View(model);
			}

			if (!_publicationService.PublicationExists(model.WorkId))
			{
				return NotFound();
			}
			
			model.Work = _publicationService.GetById(model.WorkId);
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
			
			if (!UserHasPermission(review))
			{
				return Forbid();
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
			
			if (!UserHasPermission(_reviewService.GetById(id)))
			{
				return Forbid();
			}

			_reviewService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
		
		private bool UserHasPermission(Review guidance)
		{
			var user = _userProfileService.Get(User);
			var department = _departmentService.Get(d => d.Staff.Contains(user));
			return PageHelpers.IsAdmin(User) ||
			       PageHelpers.IsHeadOfDepartment(User) &&
			       department.Staff.Contains(guidance.Reviewer) ||
			       guidance.Reviewer.Id == user.Id;
		}
	}
}
