using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class ReportThesisRepository : IRepository<ReportThesis>
	{
		private readonly ScientificReportDbContext _context;

		public ReportThesisRepository(ScientificReportDbContext context)
		{
			_context = context;
		}

		public virtual IEnumerable<ReportThesis> All()
		{
			return _context.ReportTheses
				.Include(r => r.Conference)
				.Include(r => r.UserProfilesReportTheses)
				.ThenInclude(r => r.UserProfile);
		}

		public virtual IEnumerable<ReportThesis> AllWhere(Func<ReportThesis, bool> predicate)
		{
			return All().Where(predicate);
		}

		public virtual ReportThesis Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public virtual ReportThesis Get(Func<ReportThesis, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public virtual void Create(ReportThesis item)
		{
			_context.ReportTheses.Add(item);
			_context.SaveChanges();
		}

		public virtual void Update(ReportThesis item)
		{
			if (item == null) return;
			_context.ReportTheses.Update(item);
			_context.SaveChanges();
		}

		public virtual void Delete(Guid id)
		{
			var item = _context.ReportTheses.Find(id);
			if (item == null)
			{
				return;
			}
			
			_context.ReportTheses.Remove(item);
			_context.SaveChanges();
		}

		public virtual IQueryable<ReportThesis> GetQuery()
		{
			return _context.ReportTheses;
		}
	}
}