using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class ScientificConsultationRepository: IRepository<ScientificConsultation>
	{
		private readonly ScientificReportDbContext _context;
		
		public ScientificConsultationRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public virtual IEnumerable<ScientificConsultation> All()
		{
			return _context.ScientificConsultations.Include(sc => sc.Guide);
		}

		public virtual IEnumerable<ScientificConsultation> AllWhere(Func<ScientificConsultation, bool> predicate)
		{
			return All().Where(predicate);
		}

		public virtual ScientificConsultation Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public virtual ScientificConsultation Get(Func<ScientificConsultation, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public virtual void Create(ScientificConsultation item)
		{
			_context.ScientificConsultations.Add(item);
			_context.SaveChanges();
		}

		public virtual void Update(ScientificConsultation item)
		{
			if (item != null)
			{
				_context.ScientificConsultations.Update(item);
				_context.SaveChanges();
			}
		}

		public virtual void Delete(Guid id)
		{
			var item = _context.ScientificConsultations.Find(id);
			if (item != null)
			{
				_context.ScientificConsultations.Remove(item);
				_context.SaveChanges();
			}
		}

		public virtual IQueryable<ScientificConsultation> GetQuery()
		{
			return _context.ScientificConsultations;
		}
	}
}
