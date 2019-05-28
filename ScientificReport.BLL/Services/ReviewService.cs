using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using ScientificReport.DTO.Models.Review;

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

		public virtual IEnumerable<Review> GetPage(int page, int count)
		{
			return _reviewRepository.All().Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCount()
		{
			return _reviewRepository.All().Count();
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
