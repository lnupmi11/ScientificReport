using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Repositories;

namespace ScientificReport.BLL.Services
{
	public class ReviewService : IReviewService
	{
		private readonly ReviewRepository _reviewRepository;

		public ReviewService(ScientificReportDbContext context)
		{
			_reviewRepository = new ReviewRepository(context);
		}

		public virtual IEnumerable<Review> GetAll()
		{
			return _reviewRepository.All();
		}

		public virtual IEnumerable<Review> GetAllWhere(Func<Review, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual Review GetById(Guid id)
		{
			return _reviewRepository.Get(id);
		}

		public virtual Review Get(Func<Review, bool> predicate)
		{
			return _reviewRepository.Get(predicate);
		}

		public virtual void CreateItem(Review review)
		{
			_reviewRepository.Create(review);
		}

		public virtual void UpdateItem(Review review)
		{
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

		public virtual IEnumerable<UserProfile> GetReviewers(Guid id)
		{
			var review = _reviewRepository.Get(id);
			IEnumerable<UserProfile> reviewers = null;
			if (review != null)
			{
				reviewers = review.UserProfilesReviews.Select(u => u.Reviewer);
			}

			return reviewers;
		}
	}
}
