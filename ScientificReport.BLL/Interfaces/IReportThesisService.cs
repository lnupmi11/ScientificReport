using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.BLL.Interfaces
{
	public interface IReportThesisService
	{
		IEnumerable<ReportThesis> GetAll();
		IEnumerable<ReportThesis> GetAllWhere(Func<ReportThesis, bool> predicate);
		ReportThesis GetById(Guid id);
		ReportThesis Get(Func<ReportThesis, bool> predicate);
		void CreateItem(ReportThesis reportthesis);
		void UpdateItem(ReportThesis reportthesis);
		void DeleteById(Guid id);
		bool Exists(Guid id);
		IEnumerable<UserProfile> GetAuthors(Guid id);
		void AddAuthor(Guid id, Guid authorId);
		void RemoveAuthor(Guid id, Guid authorId);
	}
}
