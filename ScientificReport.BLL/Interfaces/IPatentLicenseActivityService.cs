using System;
using System.Collections.Generic;
using System.Security.Claims;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DTO.Models.PatentLicenseActivity;

namespace ScientificReport.BLL.Interfaces
{
	public interface IPatentLicenseActivityService
	{
		IEnumerable<PatentLicenseActivity> GetAll();
		IEnumerable<PatentLicenseActivity> GetAllWhere(Func<PatentLicenseActivity, bool> predicate);
		IEnumerable<PatentLicenseActivity> GetItemsByRole(ClaimsPrincipal userClaims);
		IEnumerable<PatentLicenseActivity> GetPageByRole(int page, int count, ClaimsPrincipal userClaims);
		int GetCountByRole(ClaimsPrincipal userClaims);
		PatentLicenseActivity GetById(Guid id);
		PatentLicenseActivity Get(Func<PatentLicenseActivity, bool> predicate);
		void CreateItem(PatentLicenseActivityModel model);
		void UpdateItem(PatentLicenseActivityEditModel model);
		void DeleteById(Guid id);
		bool Exists(Guid id);
		void AddAuthor(PatentLicenseActivity patentLicenseActivity, UserProfile user);
		void RemoveAuthor(Guid id, UserProfile user);
		void AddApplicant(PatentLicenseActivity patentLicenseActivity, UserProfile user);
		void RemoveApplicant(Guid id, UserProfile user);
		void AddCoauthor(Guid id, string coauthor);
		void RemoveCoauthor(Guid id, string coauthor);
		void AddCoApplicant(Guid id, string coApplicant);
		void RemoveCoApplicant(Guid id, string coApplicant);
		IEnumerable<UserProfile> GetAuthors(Guid id);
		IEnumerable<UserProfile> GetApplicants(Guid id);
		IEnumerable<string> GetCoauthors(Guid id);
		IEnumerable<string> GetCoApplicants(Guid id);
	}
}
