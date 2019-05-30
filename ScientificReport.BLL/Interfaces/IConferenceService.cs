using System;
using System.Collections.Generic;
using System.Security.Claims;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.BLL.Interfaces
{
	public interface IConferenceService
	{
		IEnumerable<Conference> GetAll();
		IEnumerable<Conference> GetAllWhere(Func<Conference, bool> predicate);
		IEnumerable<Conference> GetItemsByRole(ClaimsPrincipal userClaims);
		IEnumerable<Conference> GetPageByRole(int page, int count, ClaimsPrincipal userClaims);
		int GetCountByRole(ClaimsPrincipal userClaims);
		Conference GetById(Guid id);
		Conference Get(Func<Conference, bool> predicate);
		void CreateItem(Conference conference);
		void UpdateItem(Conference conference);
		void DeleteById(Guid id);
		bool Exists(Guid id);
		IEnumerable<ReportThesis> GetReportTheses(Guid id);
		IEnumerable<UserProfile> GetParticipators(Guid id);
	}
}
