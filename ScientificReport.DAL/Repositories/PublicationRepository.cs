using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class PublicationRepository : IRepository<Publication>
	{
		private readonly ScientificReportDbContext _context;

		public PublicationRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public virtual IEnumerable<Publication> All()
		{
			return _context.Publications
				.Include(b => b.UserProfilesPublications)
				.ThenInclude(b => b.UserProfile);
		}

		public virtual IEnumerable<Publication> AllWhere(Func<Publication, bool> predicate)
		{
			return All().Where(predicate);
		}

		public virtual Publication Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id.Equals(id));
		}

		public virtual Publication Get(Func<Publication, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public virtual void Create(Publication item)
		{
			_context.Publications.Add(item);
			_context.SaveChanges();
		}

		public virtual void Update(Publication item)
		{
			if (item != null)
			{
				_context.Publications.Update(item);
				_context.SaveChanges();
			}
		}

		public virtual void Delete(Guid id)
		{
			var item = _context.Publications.Find(id);
			if (item != null)
			{
				_context.Publications.Remove(item);
				_context.SaveChanges();
			}
		}

		public virtual IQueryable<Publication> GetQuery()
		{
			return _context.Publications;
		}
	}
}
