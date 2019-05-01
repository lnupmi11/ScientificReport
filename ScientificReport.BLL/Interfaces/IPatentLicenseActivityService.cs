using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.BLL.Interfaces
{
	public interface IPatentLicenseActivityService
	{
		IEnumerable<PatentLicenseActivity> GetAll();
		IEnumerable<PatentLicenseActivity> GetAllWhere(Func<PatentLicenseActivity, bool> predicate);
		PatentLicenseActivity GetById(Guid id);
		PatentLicenseActivity Get(Func<PatentLicenseActivity, bool> predicate);
		void CreateItem(PatentLicenseActivity patentlicenseactivity);
		void UpdateItem(PatentLicenseActivity patentlicenseactivity);
		void DeleteById(Guid id);
		bool Exists(Guid id);
		IEnumerable<UserProfile> GetAuthors(Guid id);
		IEnumerable<UserProfile> GetApplicants(Guid id);
	}
}
