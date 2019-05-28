using System;
using System.Collections.Generic;
using System.Security.Claims;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DTO.Models.ReportThesis;

namespace ScientificReport.BLL.Interfaces
{
	public interface IReportThesisService
	{
		IEnumerable<ReportThesis> GetAll();
		IEnumerable<ReportThesis> GetAllWhere(Func<ReportThesis, bool> predicate);
		IEnumerable<ReportThesis> GetItemsByRole(ClaimsPrincipal userClaims);
		IEnumerable<ReportThesis> GetPageByRole(int page, int count, ClaimsPrincipal userClaims);
		int GetCountByRole(ClaimsPrincipal userClaims);
		ReportThesis GetById(Guid id);
		ReportThesis Get(Func<ReportThesis, bool> predicate);
		void CreateItem(ReportThesisModel model);
		void UpdateItem(ReportThesisEdit model);
		void DeleteById(Guid id);
		bool Exists(Guid id);
		IEnumerable<UserProfile> GetAuthors(Guid id);
		void AddAuthor(Guid id, Guid authorId);
		void RemoveAuthor(Guid id, Guid authorId);
	}
}
