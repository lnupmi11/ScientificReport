using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.BLL.Interfaces
{
	public interface IUserProfileService
	{
		IEnumerable<UserProfile> GetAll();
		IEnumerable<UserProfile> GetAllWhere(Func<UserProfile, bool> predicate);
		UserProfile GetById(Guid id);
		UserProfile Get(Func<UserProfile, bool> predicate);
		void CreateItem(UserProfile item);
		void UpdateItem(UserProfile item);
		void DeleteById(Guid id);
		void SetApproved(Guid id, bool isApproved);
		bool UserExists(Guid id);
		Task<IdentityResult> AddToRoleAsync(UserProfile user, string roleName, UserManager<UserProfile> userManager);
		Task<IdentityResult> RemoveFromRoleAsync(UserProfile user, string roleName, UserManager<UserProfile> userManager);
		Task<bool> IsInRoleAsync(UserProfile user, string roleName, UserManager<UserProfile> userManager);
		Task<string> ChangePassword(UserProfile user, string oldPassword, string newPassword, string newPasswordRepeat, UserManager<UserProfile> userManager);
		ICollection<Publication> GetUserPublications(Guid id);
		ICollection<Grant> GetUserGrants(Guid id);
		ICollection<ScientificWork> GetUserScientificWorks(Guid id);
		ICollection<Article> GetUserArticles(Guid id);
		ICollection<ReportThesis> GetUserReportTheses(Guid id);
		ICollection<ScientificInternship> GetUserScientificInternships(Guid id);
		ICollection<Review> GetUserReviews(Guid id);
		ICollection<PatentLicenseActivity> GetUserPatentLicenseActivitiesAsAuthor(Guid id);
		ICollection<PatentLicenseActivity> GetUserPatentLicenseActivitiesAsApplicant(Guid id);
	}
}
