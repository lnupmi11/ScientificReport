using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class UserProfilesScientificWorksRepository: IRepository<UserProfilesScientificWorks>
	{
		private readonly ScientificReportDbContext _context;
		
		public UserProfilesScientificWorksRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public virtual IEnumerable<UserProfilesScientificWorks> All()
		{
			return _context.UserProfilesScientificWorks
				.Include(u => u.ScientificWork)
				.Include(u => u.UserProfile);
		}

		public virtual IEnumerable<UserProfilesScientificWorks> AllWhere(Func<UserProfilesScientificWorks, bool> predicate)
		{
			return All().Where(predicate);
		}

		public virtual UserProfilesScientificWorks Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public virtual UserProfilesScientificWorks Get(Func<UserProfilesScientificWorks, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public virtual void Create(UserProfilesScientificWorks item)
		{
			_context.UserProfilesScientificWorks.Add(item);
			_context.SaveChanges();
		}

		public virtual void Update(UserProfilesScientificWorks item)
		{
			if (item == null) return;
			_context.UserProfilesScientificWorks.Update(item);
			_context.SaveChanges();
		}

		public virtual void Delete(Guid id)
		{
			var user = _context.UserProfilesScientificWorks.Find(id);
			if (user == null) return;
			_context.UserProfilesScientificWorks.Remove(user);
			_context.SaveChanges();
		}

		public virtual IQueryable<UserProfilesScientificWorks> GetQuery()
		{
			return _context.UserProfilesScientificWorks;
		}
	}
}
