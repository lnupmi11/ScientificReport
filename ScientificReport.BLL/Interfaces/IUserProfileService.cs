using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;

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
