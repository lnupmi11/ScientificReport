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

		public IEnumerable<ReportThesis> All()
		{
			return _context.ReportTheses
				.Include(r => r.Conference)
				.Include(r => r.UserProfilesReportTheses)
				.ThenInclude(r => r.UserProfile);
		}

		public IEnumerable<ReportThesis> AllWhere(Func<ReportThesis, bool> predicate)
		{
			return All().Where(predicate);
		}

		public ReportThesis Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public ReportThesis Get(Func<ReportThesis, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public void Create(ReportThesis item)
		{
			_context.ReportTheses.Add(item);
			_context.SaveChanges();
		}

		public void Update(ReportThesis item)
		{
			if (item == null) return;
			_context.ReportTheses.Update(item);
			_context.SaveChanges();
		}

		public void Delete(Guid id)
		{
			var user = _context.ReportTheses.Find(id);
			if (user == null) return;
			_context.ReportTheses.Remove(user);
			_context.SaveChanges();
		}

		public IQueryable<ReportThesis> GetQuery()
		{
			return _context.ReportTheses;
		}
	}
}