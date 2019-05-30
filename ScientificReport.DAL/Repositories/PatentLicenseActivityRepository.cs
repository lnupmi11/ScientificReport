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
		
		public virtual IEnumerable<PatentLicenseActivity> All()
		{
			return _context.PatentLicenseActivities
						.Include(b => b.AuthorsPatentLicenseActivities)
						.ThenInclude(b => b.Author)
						.Include(b => b.CoauthorsPatentLicenseActivities)
						.Include(a=>a.ApplicantsPatentLicenseActivities)
						.ThenInclude(b => b.Applicant)
						.Include(b => b.CoApplicantsPatentLicenseActivities);
		}

		public virtual IEnumerable<PatentLicenseActivity> AllWhere(Func<PatentLicenseActivity, bool> predicate)
		{
			return All().Where(predicate);
		}

		public virtual PatentLicenseActivity Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public virtual PatentLicenseActivity Get(Func<PatentLicenseActivity, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public virtual void Create(PatentLicenseActivity item)
		{
			_context.PatentLicenseActivities.Add(item);
			_context.SaveChanges();
		}

		public virtual void Update(PatentLicenseActivity item)
		{
			if (item != null)
			{
				_context.PatentLicenseActivities.Update(item);
				_context.SaveChanges();
			}
		}

		public virtual void Delete(Guid id)
		{
			var user = _context.PatentLicenseActivities.Find(id);
			if (user != null)
			{
				_context.PatentLicenseActivities.Remove(user);
				_context.SaveChanges();
			}
		}

		public virtual IQueryable<PatentLicenseActivity> GetQuery()
		{
			return _context.PatentLicenseActivities;
		}
	}
}
