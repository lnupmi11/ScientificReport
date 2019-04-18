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
		
		public IEnumerable<Publication> All()
		{
			return _context.Publications
				.Include(b => b.UserProfilesPublications);
		}

		public IEnumerable<Publication> AllWhere(Func<Publication, bool> predicate)
		{
			return All().Where(predicate);
		}

		public Publication Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id.Equals(id));
		}

		public Publication Get(Func<Publication, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public void Create(Publication item)
		{
			_context.Publications.Add(item);
			_context.SaveChanges();
		}

		public void Update(Publication item)
		{
			if (item != null)
			{
				_context.Publications.Update(item);
				_context.SaveChanges();
			}
		}

		public void Delete(Guid id)
		{
			var user = _context.Publications.Find(id);
			if (user != null)
			{
				_context.Publications.Remove(user);
				_context.SaveChanges();
			}
		}

		public IQueryable<Publication> GetQuery()
		{
			return _context.Publications;
		}
	}
}
