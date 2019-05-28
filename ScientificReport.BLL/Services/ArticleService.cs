using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ScientificReport.BLL.Interfaces;
using ScientificReport.BLL.Utils;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Repositories;

namespace ScientificReport.BLL.Services
{
	public class ArticleService : IArticleService
	{
		private readonly ArticleRepository _articleRepository;
		private readonly DepartmentRepository _departmentRepository;
		private readonly UserProfileRepository _userProfileRepository;

		public ArticleService(ScientificReportDbContext context)
		{
			_articleRepository = new ArticleRepository(context);
			_departmentRepository = new DepartmentRepository(context);
			_userProfileRepository = new UserProfileRepository(context);
		}

		public virtual IEnumerable<Article> GetItemsByRole(ClaimsPrincipal userClaims)
		{
			IEnumerable<Article> items;
			if (UserHelpers.IsAdmin(userClaims))
			{
				items = _articleRepository.All();
			}
			else if (UserHelpers.IsHeadOfDepartment(userClaims))
			{
				var department = _departmentRepository.Get(r => r.Head.UserName == userClaims.Identity.Name);
				items = _articleRepository.AllWhere(a => a.UserProfilesArticles.Any(u => department.Staff.Contains(u.Author)));
			}
			else
			{
				var user = _userProfileRepository.Get(u => u.UserName == userClaims.Identity.Name);
				items = _articleRepository.AllWhere(a => a.UserProfilesArticles.Any(u => u.Author.Id == user.Id));
			}

			return items;
		}
		
		public virtual IEnumerable<Article> GetPageByRole(int page, int count, ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCountByRole(ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Count();
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

		public virtual void AddAuthor(Article article, UserProfile user)
		{
			article.UserProfilesArticles.Add(new UserProfilesArticles
			{
				Article = article,
				Author = user,
				ArticleId = article.Id,
				AuthorId = user.Id
			});
			_articleRepository.Update(article);
		}
		
		public virtual void RemoveAuthor(Article article, UserProfile user)
		{
			article.UserProfilesArticles.Remove(article.UserProfilesArticles.First(up => up.Author.Id == user.Id));
			_articleRepository.Update(article);
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
