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
		
		public IEnumerable<UserProfile> All()
		{
			return _context.UserProfiles
				.Include(b => b.UserProfilesPublications)
				.Include(g => g.UserProfilesGrants)
				.Include(sw => sw.UserProfilesScientificWorks)
				.Include(a => a.UserProfilesArticles)
				.Include(rt => rt.UserProfilesReportTheses)
				.Include(r => r.UserProfilesReviews)
				.Include(si => si.UserProfilesScientificInternships)
				.Include(ap => ap.AuthorsPatentLicenseActivities)
				.Include(apl => apl.ApplicantsPatentLicenseActivities);
		}

		public IEnumerable<UserProfile> AllWhere(Func<UserProfile, bool> predicate)
		{
			return All().Where(predicate);
		}

		public UserProfile Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public UserProfile Get(Func<UserProfile, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public void Create(UserProfile item)
		{
			_context.UserProfiles.Add(item);
			_context.SaveChanges();
		}

		public void Update(UserProfile item)
		{
			if (item == null) return;
			_context.UserProfiles.Update(item);
			_context.SaveChanges();
		}

		public void Delete(Guid id)
		{
			var user = _context.UserProfiles.Find(id);
			if (user == null) return;
			_context.UserProfiles.Remove(user);
			_context.SaveChanges();
		}

		public IQueryable<UserProfile> GetQuery()
		{
			return _context.UserProfiles;
		}
	}
}
