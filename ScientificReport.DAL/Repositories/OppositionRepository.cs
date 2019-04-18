using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class OppositionRepository: IRepository<Opposition>
	{
		private readonly ScientificReportDbContext _context;
		
		public OppositionRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public IEnumerable<Opposition> All()
		{
			return _context.Oppositions;
		}

		public IEnumerable<Opposition> AllWhere(Func<Opposition, bool> predicate)
		{
			return All().Where(predicate);
		}

		public Opposition Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public Opposition Get(Func<Opposition, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public void Create(Opposition item)
		{
			_context.Oppositions.Add(item);
			_context.SaveChanges();
		}

		public void Update(Opposition item)
		{if (item != null)
			{
				_context.Oppositions.Update(item);
				_context.SaveChanges();
			}
		}

		public void Delete(Guid id)
		{
			var user = _context.Oppositions.Find(id);
			if (user != null)
			{
				_context.Oppositions.Remove(user);
				_context.SaveChanges();
			}
		}

		public IQueryable<Opposition> GetQuery()
		{
			return _context.Oppositions;
		}
	}
}