using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Interfaces;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.DAL.Repositories
{
	public class UserProfileRepository : IRepository<UserProfile>
	{
		private readonly ScientificReportDbContext _context;

		public UserProfileRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public virtual IEnumerable<UserProfile> All()
		{
			return _context.UserProfiles
				.Include(b => b.UserProfilesPublications)
				.Include(g => g.UserProfilesGrants)
				.Include(sw => sw.UserProfilesScientificWorks)
				.Include(a => a.UserProfilesArticles)
				.Include(rt => rt.UserProfilesReportTheses)
				.Include(si => si.UserProfilesScientificInternships)
				.Include(ap => ap.AuthorsPatentLicenseActivities)
				.Include(apl => apl.ApplicantsPatentLicenseActivities);
		}

		public virtual IEnumerable<UserProfile> AllWhere(Func<UserProfile, bool> predicate)
		{
			return All().Where(predicate);
		}

		public virtual UserProfile Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public virtual UserProfile Get(Func<UserProfile, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public virtual void Create(UserProfile item)
		{
			_context.UserProfiles.Add(item);
			_context.SaveChanges();
		}

		public virtual void Update(UserProfile item)
		{
			if (item == null) return;
			_context.UserProfiles.Update(item);
			_context.SaveChanges();
		}

		public virtual void Delete(Guid id)
		{
			var user = _context.UserProfiles.Find(id);
			if (user == null) return;
			_context.UserProfiles.Remove(user);
			_context.SaveChanges();
		}

		public virtual IQueryable<UserProfile> GetQuery()
		{
			return _context.UserProfiles;
		}
	}
}
