using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ScientificReport.BLL.Interfaces;
using ScientificReport.BLL.Utils;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using ScientificReport.DTO.Models.Review;

namespace ScientificReport.BLL.Services
{
	public class ReviewService : IReviewService
	{
		private readonly ReviewRepository _reviewRepository;
		private readonly DepartmentRepository _departmentRepository;
		private readonly UserProfileRepository _userProfileRepository;

		public ReviewService(ScientificReportDbContext context)
		{
			_reviewRepository = new ReviewRepository(context);
			_departmentRepository = new DepartmentRepository(context);
			_userProfileRepository = new UserProfileRepository(context);
		}

		public virtual IEnumerable<Review> GetAll()
		{
			return _reviewRepository.All();
		}

		public virtual IEnumerable<Review> GetAllWhere(Func<Review, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual IEnumerable<Review> GetItemsByRole(ClaimsPrincipal userClaims)
		{
			IEnumerable<Review> items;
			if (UserHelpers.IsAdmin(userClaims))
			{
				items = _reviewRepository.All();
			}
			else if (UserHelpers.IsHeadOfDepartment(userClaims))
			{
				var department = _departmentRepository.Get(r => r.Head.UserName == userClaims.Identity.Name);
				items = _reviewRepository.AllWhere(m => department.Staff.Contains(m.Reviewer));
			}
			else
			{
				var user = _userProfileRepository.Get(u => u.UserName == userClaims.Identity.Name);
				items = _reviewRepository.AllWhere(m => m.Reviewer.Id == user.Id);
			}

			return items;
		}

		public virtual IEnumerable<Review> GetPageByRole(int page, int count, ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCountByRole(ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Count();
		}

		public virtual Review GetById(Guid id)
		{
			return _reviewRepository.Get(id);
		}

		public virtual Review Get(Func<Review, bool> predicate)
		{
			return _reviewRepository.Get(predicate);
		}

		public virtual void CreateItem(ReviewModel model)
		{
			_reviewRepository.Create(new Review
			{
				Work = model.Work,
				DateOfReview = model.DateOfReview,
				Reviewer = model.Reviewer
			});
		}

		public virtual void UpdateItem(ReviewEditModel model)
		{
			var review = GetById(model.Id);
			if (review == null)
			{
				return;
			}

			review.Work = model.Work;
			review.DateOfReview = model.DateOfReview;
			_reviewRepository.Update(review);
		}

		public virtual void DeleteById(Guid id)
		{
			_reviewRepository.Delete(id);
		}

		public virtual bool Exists(Guid id)
		{
			return _reviewRepository.Get(id) != null;
		}
	}
}
