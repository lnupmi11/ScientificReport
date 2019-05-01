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
	public class ArticleService : IArticleService
	{
		private readonly ArticleRepository _articleRepository;

		public ArticleService(ScientificReportDbContext context)
		{
			_articleRepository = new ArticleRepository(context);
		}

		public virtual IEnumerable<Article> GetAll()
		{
			return _articleRepository.All();
		}

		public virtual IEnumerable<Article> GetAllWhere(Func<Article, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual Article GetById(Guid id)
		{
			return _articleRepository.Get(id);
		}

		public virtual Article Get(Func<Article, bool> predicate)
		{
			return _articleRepository.Get(predicate);
		}

		public virtual void CreateItem(Article article)
		{
			_articleRepository.Create(article);
		}

		public virtual void UpdateItem(Article article)
		{
			_articleRepository.Update(article);
		}

		public virtual void DeleteById(Guid id)
		{
			_articleRepository.Delete(id);
		}

		public virtual bool Exists(Guid id)
		{
			return _articleRepository.Get(id) != null;
		}

		public virtual IEnumerable<UserProfile> GetAuthors(Guid id)
		{
			var article = _articleRepository.Get(id);
			IEnumerable<UserProfile> authors = null;
			if (article != null)
			{
				authors = article.UserProfilesArticles.Select(u => u.Author);
			}

			return authors;
		}
	}
}
