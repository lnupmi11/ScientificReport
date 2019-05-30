using System;
using System.Collections.Generic;
using System.Security.Claims;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.Review;

namespace ScientificReport.BLL.Interfaces
{
	public interface IReviewService
	{
		IEnumerable<Review> GetAll();
		IEnumerable<Review> GetAllWhere(Func<Review, bool> predicate);
		IEnumerable<Review> GetItemsByRole(ClaimsPrincipal userClaims);
		IEnumerable<Review> GetPageByRole(int page, int count, ClaimsPrincipal userClaims);
		int GetCountByRole(ClaimsPrincipal userClaims);
		Review GetById(Guid id);
		Review Get(Func<Review, bool> predicate);
		void CreateItem(ReviewModel model);
		void UpdateItem(ReviewEditModel model);
		void DeleteById(Guid id);
		bool Exists(Guid id);
	}
}
