using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.Reports;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class TeacherReportRepository : IRepository<TeacherReport>
	{
		private readonly ScientificReportDbContext _context;

		public TeacherReportRepository(ScientificReportDbContext context)
		{
			_context = context;
		}

		public IEnumerable<TeacherReport> All()
		{
			return _context.TeacherReports
				.Include(p => p.Teacher)
				.Include(p => p.TeacherReportsPublications).ThenInclude(p => p.Publication)
				.Include(p => p.TeacherReportsArticles).ThenInclude(p => p.Article)
				.Include(p => p.TeacherReportsReportThesis).ThenInclude(p => p.ReportThesis)
				.ThenInclude(p => p.Conference)
				.Include(p => p.TeacherReportsScientificWorks).ThenInclude(p => p.ScientificWork)
				
				.Include(p => p.TeacherReportsGrants).ThenInclude(p => p.Grant)
				.Include(p => p.TeacherReportsScientificInternships).ThenInclude(p => p.ScientificInternship)
				.Include(p => p.TeacherReportsPostgraduateGuidances).ThenInclude(p => p.PostgraduateGuidance)
				.Include(p => p.TeacherReportsScientificConsultations).ThenInclude(p => p.ScientificConsultation)
				.Include(p => p.TeacherReportsPostgraduateDissertationGuidances)
				.ThenInclude(p => p.PostgraduateDissertationGuidance)
				.Include(p => p.TeacherReportsReviews).ThenInclude(p => p.Review)
				.Include(p => p.TeacherReportsOppositions).ThenInclude(p => p.Opposition)
				.Include(p => p.TeacherReportsPatents).ThenInclude(p => p.Patent)
				.Include(p => p.TeacherReportsMemberships).ThenInclude(p => p.Membership)
				;
		}

		public virtual IEnumerable<TeacherReport> AllWhere(Func<TeacherReport, bool> predicate)
		{
			return All().Where(predicate);
		}

		public virtual TeacherReport Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public virtual TeacherReport Get(Func<TeacherReport, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public virtual void Create(TeacherReport item)
		{
			_context.TeacherReports.Add(item);
			_context.SaveChanges();
		}

		public virtual void Update(TeacherReport item)
		{
			if (item == null) return;
			_context.TeacherReports.Update(item);
			_context.SaveChanges();
		}

		public virtual void Delete(Guid id)
		{
			var item = _context.TeacherReports.Find(id);
			if (item == null)
			{
				return;
			}
			
			_context.TeacherReports.Remove(item);
			_context.SaveChanges();
		}

		public virtual IQueryable<TeacherReport> GetQuery()
		{
			return _context.TeacherReports;
		}
	}
}
