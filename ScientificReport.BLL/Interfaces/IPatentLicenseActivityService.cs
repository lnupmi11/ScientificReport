using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DTO.Models.PatentLicenseActivity;

namespace ScientificReport.BLL.Interfaces
{
	public interface IPatentLicenseActivityService
	{
		IEnumerable<PatentLicenseActivity> GetAll();
		IEnumerable<PatentLicenseActivity> GetAllWhere(Func<PatentLicenseActivity, bool> predicate);
		IEnumerable<PatentLicenseActivity> GetPage(int page, int count);
		int GetCount();
		PatentLicenseActivity GetById(Guid id);
		PatentLicenseActivity Get(Func<PatentLicenseActivity, bool> predicate);
		void CreateItem(PatentLicenseActivityModel model);
		void UpdateItem(PatentLicenseActivityEditModel model);
		void DeleteById(Guid id);
		bool Exists(Guid id);
		IEnumerable<UserProfile> GetAuthors(Guid id);
		IEnumerable<UserProfile> GetApplicants(Guid id);
		IEnumerable<string> GetCoauthors(Guid id);
		IEnumerable<string> GetCoApplicants(Guid id);
	}
}
