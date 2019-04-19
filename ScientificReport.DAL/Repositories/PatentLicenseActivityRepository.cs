using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class PatentLicenseActivityRepository: IRepository<PatentLicenseActivity>
	{
		private readonly ScientificReportDbContext _context;
		
		public PatentLicenseActivityRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public IEnumerable<PatentLicenseActivity> All()
		{
			return _context.PatentLicenseActivities
						.Include(b => b.AuthorsPatentLicenseActivities)
						.Include(a=>a.ApplicantsPatentLicenseActivities);
		}

		public IEnumerable<PatentLicenseActivity> AllWhere(Func<PatentLicenseActivity, bool> predicate)
		{
			return All().Where(predicate);
		}

		public PatentLicenseActivity Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public PatentLicenseActivity Get(Func<PatentLicenseActivity, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public void Create(PatentLicenseActivity item)
		{
			_context.PatentLicenseActivities.Add(item);
			_context.SaveChanges();
		}

		public void Update(PatentLicenseActivity item)
		{if (item != null)
			{
				_context.PatentLicenseActivities.Update(item);
				_context.SaveChanges();
			}
		}

		public void Delete(Guid id)
		{
			var user = _context.PatentLicenseActivities.Find(id);
			if (user != null)
			{
				_context.PatentLicenseActivities.Remove(user);
				_context.SaveChanges();
			}
		}

		public IQueryable<PatentLicenseActivity> GetQuery()
		{
			return _context.PatentLicenseActivities;
		}
	}
}