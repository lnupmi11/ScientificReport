using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.BLL.Interfaces
{
	public interface IArticleService
	{
		IEnumerable<Article> GetAll();
		IEnumerable<Article> GetAllWhere(Func<Article, bool> predicate);
		Article GetById(Guid id);
		Article Get(Func<Article, bool> predicate);
		void CreateItem(Article article);
		void UpdateItem(Article article);
		void DeleteById(Guid id);
		bool Exists(Guid id);
		IEnumerable<UserProfile> GetAuthors(Guid id);
	}
}
