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
		
		public IEnumerable<ScientificConsultation> All()
		{
			return _context.ScientificConsultations;
		}

		public IEnumerable<ScientificConsultation> AllWhere(Func<ScientificConsultation, bool> predicate)
		{
			return All().Where(predicate);
		}

		public ScientificConsultation Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public ScientificConsultation Get(Func<ScientificConsultation, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public void Create(ScientificConsultation item)
		{
			_context.ScientificConsultations.Add(item);
			_context.SaveChanges();
		}

		public void Update(ScientificConsultation item)
		{if (item != null)
			{
				_context.ScientificConsultations.Update(item);
				_context.SaveChanges();
			}
		}

		public void Delete(Guid id)
		{
			var user = _context.ScientificConsultations.Find(id);
			if (user != null)
			{
				_context.ScientificConsultations.Remove(user);
				_context.SaveChanges();
			}
		}

		public IQueryable<ScientificConsultation> GetQuery()
		{
			return _context.ScientificConsultations;
		}
	}
}