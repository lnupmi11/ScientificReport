using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class ArticleRepository: IRepository<Article>
	{
		private readonly ScientificReportDbContext _context;

		public ArticleRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public IEnumerable<Article> All()
		{
			return _context.Articles
						.Include(b => b.UserProfilesArticles);
		}

		public IEnumerable<Article> AllWhere(Func<Article, bool> predicate)
		{
			return All().Where(predicate);
		}

		public Article Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public Article Get(Func<Article, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public void Create(Article item)
		{
			_context.Articles.Add(item);
			_context.SaveChanges();
		}

		public void Update(Article item)
		{if (item != null)
			{
				_context.Articles.Update(item);
				_context.SaveChanges();
			}
		}

		public void Delete(Guid id)
		{
			var user = _context.Articles.Find(id);
			if (user != null)
			{
				_context.Articles.Remove(user);
				_context.SaveChanges();
			}
		}

		public IQueryable<Article> GetQuery()
		{
			return _context.Articles;
		}
	}
}