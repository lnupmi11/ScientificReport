using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.BLL.Interfaces
{
	public interface IConferenceService
	{
		IEnumerable<Conference> GetAll();
		IEnumerable<Conference> GetAllWhere(Func<Conference, bool> predicate);
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
