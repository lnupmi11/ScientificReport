using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.BLL.Interfaces
{
	public interface IReviewService
	{
		IEnumerable<Review> GetAll();
		IEnumerable<Review> GetAllWhere(Func<Review, bool> predicate);
		Review GetById(Guid id);
		Review Get(Func<Review, bool> predicate);
		void CreateItem(Review review);
		void UpdateItem(Review review);
		void DeleteById(Guid id);
		bool Exists(Guid id);
		IEnumerable<UserProfile> GetReviewers(Guid id);
	}
}
