using System;
using System.Collections.Generic;
using System.Security.Claims;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.BLL.Interfaces
{
	public interface IArticleService
	{
		IEnumerable<Article> GetItemsByRole(ClaimsPrincipal userClaims);
		IEnumerable<Article> GetPageByRole(int page, int count, ClaimsPrincipal userClaims);
		int GetCountByRole(ClaimsPrincipal userClaims);
		IEnumerable<Article> GetAll();
		IEnumerable<Article> GetAllWhere(Func<Article, bool> predicate);
		Article GetById(Guid id);
		Article Get(Func<Article, bool> predicate);
		void CreateItem(Article article);
		void UpdateItem(Article article);
		void DeleteById(Guid id);
		bool Exists(Guid id);
		void AddAuthor(Article article, UserProfile user);
		void RemoveAuthor(Article article, UserProfile user);
		IEnumerable<UserProfile> GetAuthors(Guid id);
	}
}
