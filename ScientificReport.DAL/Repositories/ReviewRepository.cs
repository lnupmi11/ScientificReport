using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class ReviewRepository: IRepository<Review>
	{
		private readonly ScientificReportDbContext _context;
		
		public ReviewRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public IEnumerable<Review> All()
		{
			return _context.Reviews
						.Include(b => b.UserProfilesReviews);
		}

		public IEnumerable<Review> AllWhere(Func<Review, bool> predicate)
		{
			return All().Where(predicate);
		}

		public Review Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public Review Get(Func<Review, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public void Create(Review item)
		{
			_context.Reviews.Add(item);
			_context.SaveChanges();
		}

		public void Update(Review item)
		{if (item != null)
			{
				_context.Reviews.Update(item);
				_context.SaveChanges();
			}
		}

		public void Delete(Guid id)
		{
			var user = _context.Reviews.Find(id);
			if (user != null)
			{
				_context.Reviews.Remove(user);
				_context.SaveChanges();
			}
		}

		public IQueryable<Review> GetQuery()
		{
			return _context.Reviews;
		}
	}
}