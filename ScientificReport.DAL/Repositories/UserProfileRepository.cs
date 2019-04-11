using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Interfaces;
using ScientificReport.DAL.Entities;

namespace ScientificReport.DAL.Repositories
{
	public class UserProfileRepository : IRepository<UserProfile, string>
	{
		private readonly ScientificReportDbContext _context;
		private readonly DbSet<UserProfile> _userProfiles;

		public UserProfileRepository(ScientificReportDbContext context)
		{
			_context = context;
			_userProfiles = context.UserProfiles;
		}
		
		public IEnumerable<UserProfile> All()
		{
			return _userProfiles
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

		public UserProfile Get(string id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public UserProfile Get(Func<UserProfile, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public void Create(UserProfile item)
		{
			_userProfiles.Add(item);
			_context.SaveChanges();
		}

		public void Update(UserProfile item)
		{
			if (item == null)
			{
				throw new ArgumentNullException(nameof(item), "unable to create null user profile");
			}

			_userProfiles.Update(item);
			_context.SaveChanges();
		}

		public void Delete(string id)
		{
			var user = _userProfiles.Find(id);
			if (user == null)
			{
				return;
			}

			_userProfiles.Remove(user);
			_context.SaveChanges();
		}

		public IQueryable<UserProfile> GetQuery()
		{
			return _userProfiles;
		}
	}
}
