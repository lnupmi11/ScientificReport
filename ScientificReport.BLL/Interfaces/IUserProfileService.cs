using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;

namespace ScientificReport.BLL.Interfaces
{
	public interface IUserProfileService
	{
		IEnumerable<UserProfile> GetAll();
		IEnumerable<UserProfile> GetAllWhere(Func<UserProfile, bool> predicate);
		UserProfile GetById(string id);
		UserProfile Get(Func<UserProfile, bool> predicate);
		void CreateItem(UserProfile item);
		void UpdateItem(UserProfile item);
		void DeleteById(string id);
		void SetApproved(string id, bool isApproved);
		bool UserExists(string id);
		ICollection<Publication> GetUserPublications(string id);
		ICollection<Grant> GetUserGrants(string id);
		ICollection<ScientificWork> GetUserScientificWorks(string id);
		ICollection<Article> GetUserArticles(string id);
		ICollection<ReportThesis> GetUserReportTheses(string id);
		ICollection<ScientificInternship> GetUserScientificInternships(string id);
		ICollection<Review> GetUserReviews(string id);
		ICollection<PatentLicenseActivity> GetUserPatentLicenseActivitiesAsAuthor(string id);
		ICollection<PatentLicenseActivity> GetUserPatentLicenseActivitiesAsApplicant(string id);
	}
}
