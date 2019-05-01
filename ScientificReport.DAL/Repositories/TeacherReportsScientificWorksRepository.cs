using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.Reports;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class TeacherReportsScientificWorksRepository : IRepository<TeacherReportsScientificWorks>
	{
		private readonly ScientificReportDbContext _context;

		public TeacherReportsScientificWorksRepository(ScientificReportDbContext context)
		{
			_context = context;
		}

		public virtual IEnumerable<TeacherReportsScientificWorks> All()
		{
			return _context.TeacherReportsScientificWorks;
		}

		public virtual IEnumerable<TeacherReportsScientificWorks> AllWhere(Func<TeacherReportsScientificWorks, bool> predicate)
		{
			return All().Where(predicate);
		}

		public virtual TeacherReportsScientificWorks Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public virtual TeacherReportsScientificWorks Get(Func<TeacherReportsScientificWorks, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public virtual void Create(TeacherReportsScientificWorks item)
		{
			_context.TeacherReportsScientificWorks.Add(item);
			_context.SaveChanges();
		}

		public virtual void Update(TeacherReportsScientificWorks item)
		{
			if (item == null) return;
			_context.TeacherReportsScientificWorks.Update(item);
			_context.SaveChanges();
		}

		public virtual void Delete(Guid id)
		{
			var item = _context.TeacherReportsScientificWorks.Find(id);
			if (item == null) return;
			_context.TeacherReportsScientificWorks.Remove(item);
			_context.SaveChanges();
		}

		public virtual IQueryable<TeacherReportsScientificWorks> GetQuery()
		{
			return _context.TeacherReportsScientificWorks;
		}
	}
}
