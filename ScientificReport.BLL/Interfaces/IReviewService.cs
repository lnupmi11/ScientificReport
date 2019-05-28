using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DTO.Models.Review;

namespace ScientificReport.BLL.Interfaces
{
	public interface IReviewService
	{
		IEnumerable<Review> GetAll();
		IEnumerable<Review> GetAllWhere(Func<Review, bool> predicate);
		IEnumerable<Review> GetPage(int page, int count);
		int GetCount();
		Review GetById(Guid id);
		Review Get(Func<Review, bool> predicate);
		void CreateItem(ReviewModel model);
		void UpdateItem(ReviewEditModel model);
		void DeleteById(Guid id);
		bool Exists(Guid id);
		IEnumerable<UserProfile> GetReviewers(Guid id);
	}
}
