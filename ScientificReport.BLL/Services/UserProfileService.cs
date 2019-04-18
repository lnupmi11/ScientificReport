using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;

namespace ScientificReport.BLL.Services
{
	public class UserProfileService : IUserProfileService
	{
		private readonly UserProfileRepository _userProfileRepository;

		public UserProfileService(ScientificReportDbContext context)
		{
			_userProfileRepository = new UserProfileRepository(context);
		}

		public IEnumerable<UserProfile> GetAll()
		{
			return _userProfileRepository.All();
		}

		public IEnumerable<UserProfile> GetAllWhere(Func<UserProfile, bool> predicate)
		{
			return _userProfileRepository.AllWhere(predicate);
		}

		public UserProfile GetById(Guid id)
		{
			return _userProfileRepository.Get(id);
		}

		public UserProfile Get(Func<UserProfile, bool> predicate)
		{
			return _userProfileRepository.Get(predicate);
		}

		public void CreateItem(UserProfile item)
		{
			if (item != null)
			{
				_userProfileRepository.Create(item);
			}
		}

		public void UpdateItem(UserProfile item)
		{
			if (item != null)
			{
				_userProfileRepository.Update(item);
			}
		}

		public void DeleteById(Guid id)
		{
			_userProfileRepository.Delete(id);
		}

		public void SetApproved(Guid id, bool isApproved)
		{
			var user = _userProfileRepository.Get(id);
			if (user != null)
			{
				user.IsApproved = isApproved;
				_userProfileRepository.Update(user);
			}
		}

		public bool UserExists(Guid id)
		{
			return _userProfileRepository.Get(id) != null;
		}

		public ICollection<Publication> GetUserPublications(Guid id)
		{
			var user = _userProfileRepository.Get(id);
			ICollection<Publication> result = null;
			if (user != null)
			{
				result = user.UserProfilesPublications.Select(item => item.Publication).ToList();
			}

			return result;
		}

		public ICollection<Grant> GetUserGrants(Guid id)
		{
			var user = _userProfileRepository.Get(id);
			ICollection<Grant> result = null;
			if (user != null)
			{
				result = user.UserProfilesGrants.Select(item => item.Grant).ToList();
			}

			return result;
		}

		public ICollection<ScientificWork> GetUserScientificWorks(Guid id)
		{
			var user = _userProfileRepository.Get(id);
			ICollection<ScientificWork> result = null;
			if (user != null)
			{
				result = user.UserProfilesScientificWorks.Select(item => item.ScientificWork).ToList();
			}

			return result;
		}

		public ICollection<Article> GetUserArticles(Guid id)
		{
			var user = _userProfileRepository.Get(id);
			ICollection<Article> result = null;
			if (user != null)
			{
				result = user.UserProfilesArticles.Select(item => item.Article).ToList();
			}

			return result;
		}

		public ICollection<ReportThesis> GetUserReportTheses(Guid id)
		{
			var user = _userProfileRepository.Get(id);
			ICollection<ReportThesis> result = null;
			if (user != null)
			{
				result = user.UserProfilesReportTheses.Select(item => item.ReportThesis).ToList();
			}

			return result;
		}

		public ICollection<ScientificInternship> GetUserScientificInternships(Guid id)
		{
			var user = _userProfileRepository.Get(id);
			ICollection<ScientificInternship> result = null;
			if (user != null)
			{
				result = user.UserProfilesScientificInternships.Select(item => item.ScientificInternship).ToList();
			}

			return result;
		}

		public ICollection<Review> GetUserReviews(Guid id)
		{
			var user = _userProfileRepository.Get(id);
			ICollection<Review> result = null;
			if (user != null)
			{
				result = user.UserProfilesReviews.Select(item => item.Review).ToList();
			}

			return result;
		}

		public ICollection<PatentLicenseActivity> GetUserPatentLicenseActivitiesAsAuthor(Guid id)
		{
			var user = _userProfileRepository.Get(id);
			ICollection<PatentLicenseActivity> result = null;
			if (user != null)
			{
				result = user.AuthorsPatentLicenseActivities.Select(item => item.PatentLicenseActivity).ToList();
			}

			return result;
		}

		public ICollection<PatentLicenseActivity> GetUserPatentLicenseActivitiesAsApplicant(Guid id)
		{
			var user = _userProfileRepository.Get(id);
			ICollection<PatentLicenseActivity> result = null;
			if (user != null)
			{
				result = user.ApplicantsPatentLicenseActivities.Select(item => item.PatentLicenseActivity).ToList();
			}

			return result;
		}
	}
}
