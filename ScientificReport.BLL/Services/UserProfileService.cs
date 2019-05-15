using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
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

		public virtual IEnumerable<UserProfile> GetAll()
		{
			return _userProfileRepository.All();
		}

		public virtual IEnumerable<UserProfile> GetAllWhere(Func<UserProfile, bool> predicate)
		{
			return _userProfileRepository.AllWhere(predicate);
		}

		public virtual UserProfile Get(ClaimsPrincipal claimsPrincipal)
		{
			return _userProfileRepository.Get(u => u.UserName == claimsPrincipal.Identity.Name);
		}

		public virtual UserProfile GetById(Guid id)
		{
			return _userProfileRepository.Get(id);
		}

		public virtual UserProfile Get(Func<UserProfile, bool> predicate)
		{
			return _userProfileRepository.Get(predicate);
		}

		public virtual void CreateItem(UserProfile item)
		{
			if (item != null)
			{
				_userProfileRepository.Create(item);
			}
		}

		public virtual void UpdateItem(UserProfile item)
		{
			if (item != null)
			{
				_userProfileRepository.Update(item);
			}
		}

		public virtual void DeleteById(Guid id)
		{
			_userProfileRepository.Delete(id);
		}

		public virtual void SetActiveById(Guid id, bool isActive)
		{
			var user = _userProfileRepository.Get(id);
			user.IsActive = isActive;
			_userProfileRepository.Update(user);
		}

		public virtual void SetApproved(Guid id, bool isApproved)
		{
			var user = _userProfileRepository.Get(id);
			if (user == null) return;
			user.IsApproved = isApproved;
			_userProfileRepository.Update(user);
		}

		public virtual bool UserExists(Guid id)
		{
			return _userProfileRepository.Get(id) != null;
		}

		public virtual async Task<IdentityResult> AddToRoleAsync(UserProfile userProfile, string roleName, UserManager<UserProfile> userManager)
		{
			return await userManager.AddToRoleAsync(userProfile, roleName);
		}

		public virtual async Task<IdentityResult> RemoveFromRoleAsync(UserProfile userProfile, string roleName, UserManager<UserProfile> userManager)
		{
			return await userManager.RemoveFromRoleAsync(userProfile, roleName);
		}

		public virtual async Task<bool> IsInRoleAsync(UserProfile user, string roleName, UserManager<UserProfile> userManager)
		{
			return await userManager.IsInRoleAsync(user, roleName);
		}

		public virtual async Task<string> ChangePassword(UserProfile user, string oldPassword, string newPassword, string newPasswordRepeat, UserManager<UserProfile> userManager)
		{
			string result = null;
			
			// Checks if entered password is equal to current user password
			if (await userManager.CheckPasswordAsync(user, oldPassword))
			{
				// Checks if new password and repeated password are equal
				if (newPassword.Equals(newPasswordRepeat))
				{
					// Checks if new password is equal to current user password
					if (!oldPassword.Equals(newPassword))
					{
						var passwordValidator = new PasswordValidator<UserProfile>();
						
						// Validates new password
						var validationResult = await passwordValidator.ValidateAsync(userManager, user, newPassword);
						if (validationResult.Succeeded)
						{
							await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
						}
						else
						{
							result = validationResult.ToString();	
						}
					}
					else
					{
						result = "New password must differ from an old one";	
					}
				}
				else
				{
					result = "Repeated password does not mach a new one";
				}
			}
			else
			{
				result = "Old password is incorrect";	
			}

			return result;
		}

		public virtual ICollection<Publication> GetUserPublications(Guid id)
		{
			var user = _userProfileRepository.Get(id);
			ICollection<Publication> result = null;
			if (user != null)
			{
				result = user.UserProfilesPublications.Select(item => item.Publication).ToList();
			}

			return result;
		}

		public virtual ICollection<Grant> GetUserGrants(Guid id)
		{
			var user = _userProfileRepository.Get(id);
			ICollection<Grant> result = null;
			if (user != null)
			{
				result = user.UserProfilesGrants.Select(item => item.Grant).ToList();
			}

			return result;
		}

		public virtual ICollection<ScientificWork> GetUserScientificWorks(Guid id)
		{
			var user = _userProfileRepository.Get(id);
			ICollection<ScientificWork> result = null;
			if (user != null)
			{
				result = user.UserProfilesScientificWorks.Select(item => item.ScientificWork).ToList();
			}

			return result;
		}

		public virtual ICollection<Article> GetUserArticles(Guid id)
		{
			var user = _userProfileRepository.Get(id);
			ICollection<Article> result = null;
			if (user != null)
			{
				result = user.UserProfilesArticles.Select(item => item.Article).ToList();
			}

			return result;
		}

		public virtual ICollection<ReportThesis> GetUserReportTheses(Guid id)
		{
			var user = _userProfileRepository.Get(id);
			ICollection<ReportThesis> result = null;
			if (user != null)
			{
				result = user.UserProfilesReportTheses.Select(item => item.ReportThesis).ToList();
			}

			return result;
		}

		public virtual ICollection<ScientificInternship> GetUserScientificInternships(Guid id)
		{
			var user = _userProfileRepository.Get(id);
			ICollection<ScientificInternship> result = null;
			if (user != null)
			{
				result = user.UserProfilesScientificInternships.Select(item => item.ScientificInternship).ToList();
			}

			return result;
		}

		public virtual ICollection<Review> GetUserReviews(Guid id)
		{
			var user = _userProfileRepository.Get(id);
			ICollection<Review> result = null;
			if (user != null)
			{
				result = user.UserProfilesReviews.Select(item => item.Review).ToList();
			}

			return result;
		}

		public virtual ICollection<PatentLicenseActivity> GetUserPatentLicenseActivitiesAsAuthor(Guid id)
		{
			var user = _userProfileRepository.Get(id);
			ICollection<PatentLicenseActivity> result = null;
			if (user != null)
			{
				result = user.AuthorsPatentLicenseActivities.Select(item => item.PatentLicenseActivity).ToList();
			}

			return result;
		}

		public virtual ICollection<PatentLicenseActivity> GetUserPatentLicenseActivitiesAsApplicant(Guid id)
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
