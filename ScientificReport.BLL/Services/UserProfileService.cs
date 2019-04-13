using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Design;
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

		public UserProfile GetById(string id)
		{
			return _userProfileRepository.Get(id);
		}

		public UserProfile Get(Func<UserProfile, bool> predicate)
		{
			return _userProfileRepository.Get(predicate);
		}

		public void CreateItem(UserProfile item)
		{
			if (UserExists(item.Id))
			{
				throw new OperationException($"CreateItem: user with id {item.Id} already exists");
			}

			_userProfileRepository.Create(item);
		}

		public void UpdateItem(UserProfile item)
		{
			if (UserExists(item.Id))
			{
				_userProfileRepository.Update(item);
			}
			else
			{
				throw new OperationException($"UpdateItem: user with id {item.Id} does not exist");
			}
		}

		public void DeleteById(string id)
		{
			if (UserExists(id))
			{
				_userProfileRepository.Delete(id);
			}
			else
			{
				throw new OperationException($"DeleteById: user with id {id} does not exist");
			}
		}

		public void SetApproved(string id, bool isApproved)
		{
			var user = _userProfileRepository.Get(id);
			if (user != null)
			{
				user.IsApproved = isApproved;
				_userProfileRepository.Update(user);
			}
			else
			{
				throw new OperationException($"SetApproved: user with id {id} does not exist");
			}
		}

		public bool UserExists(string id)
		{
			return _userProfileRepository.Get(id) != null;
		}

		public ICollection<Publication> GetUserPublications(string id)
		{
			var user = _userProfileRepository.Get(id);
			if (user != null)
			{
				return user.UserProfilesPublications.Select(item => item.Publication).ToList();
			}

			throw new OperationException($"GetUserPublications: user with id {id} does not exist");
		}

		public ICollection<Grant> GetUserGrants(string id)
		{
			var user = _userProfileRepository.Get(id);
			if (user != null)
			{
				return user.UserProfilesGrants.Select(item => item.Grant).ToList();
			}

			throw new OperationException($"GetUserGrants: user with id {id} does not exist");
		}

		public ICollection<ScientificWork> GetUserScientificWorks(string id)
		{
			var user = _userProfileRepository.Get(id);
			if (user != null)
			{
				return user.UserProfilesScientificWorks.Select(item => item.ScientificWork).ToList();
			}

			throw new OperationException($"GetUserScientificWorks: user with id {id} does not exist");
		}

		public ICollection<Article> GetUserArticles(string id)
		{
			var user = _userProfileRepository.Get(id);
			if (user != null)
			{
				return user.UserProfilesArticles.Select(item => item.Article).ToList();
			}

			throw new OperationException($"GetUserArticles: user with id {id} does not exist");
		}

		public ICollection<ReportThesis> GetUserReportTheses(string id)
		{
			var user = _userProfileRepository.Get(id);
			if (user != null)
			{
				return user.UserProfilesReportTheses.Select(item => item.ReportThesis).ToList();
			}

			throw new OperationException($"GetUserReportTheses: user with id {id} does not exist");
		}

		public ICollection<ScientificInternship> GetUserScientificInternships(string id)
		{
			var user = _userProfileRepository.Get(id);
			if (user != null)
			{
				return user.UserProfilesScientificInternships.Select(item => item.ScientificInternship).ToList();
			}

			throw new OperationException($"GetUserScientificInternships: user with id {id} does not exist");
		}

		public ICollection<Review> GetUserReviews(string id)
		{
			var user = _userProfileRepository.Get(id);
			if (user != null)
			{
				return user.UserProfilesReviews.Select(item => item.Review).ToList();
			}

			throw new OperationException($"GetUserReviews: user with id {id} does not exist");
		}

		public ICollection<PatentLicenseActivity> GetUserPatentLicenseActivitiesAsAuthor(string id)
		{
			var user = _userProfileRepository.Get(id);
			if (user != null)
			{
				return user.AuthorsPatentLicenseActivities.Select(item => item.PatentLicenseActivity).ToList();
			}

			throw new OperationException($"GetUserPatentLicenseActivitiesAsAuthor: user with id {id} does not exist");
		}

		public ICollection<PatentLicenseActivity> GetUserPatentLicenseActivitiesAsApplicant(string id)
		{
			var user = _userProfileRepository.Get(id);
			if (user != null)
			{
				return user.ApplicantsPatentLicenseActivities.Select(item => item.PatentLicenseActivity).ToList();
			}

			throw new OperationException($"GetUserPatentLicenseActivitiesAsApplicant: user with id {id} does not exist");
		}
	}
}
