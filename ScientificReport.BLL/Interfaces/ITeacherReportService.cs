using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.Reports;

namespace ScientificReport.BLL.Interfaces
{
	public interface ITeacherReportService
	{
			IEnumerable<TeacherReport> GetAll();
			IEnumerable<TeacherReport> GetAllWhere(Func<TeacherReport, bool> predicate);
			TeacherReport GetById(Guid id);
			TeacherReport Get(Func<TeacherReport, bool> predicate);
			void CreateItem(TeacherReport item);
			void UpdateItem(TeacherReport item);
			void DeleteById(Guid id);
			bool Any(Func<TeacherReport, bool> predicate);
			bool Exists(Guid id);
			void AddPublication(Guid id, Guid entityId);
			void RemovePublication(Guid id, Guid entityId);
			void AddArticle(Guid id, Guid entityId);
			void RemoveArticle(Guid id, Guid entityId);
			void AddScientificWork(Guid id, Guid entityId);
			void RemoveScientificWork(Guid id, Guid entityId);
			void AddReportThesis(Guid id, Guid entityId);
			void RemoveReportThesis(Guid id, Guid entityId);
			void AddGrant(Guid id, Guid entityId);
			void RemoveGrant(Guid id, Guid entityId);
			void AddScientificInternship(Guid id, Guid entityId);
			void RemoveScientificInternship(Guid id, Guid entityId);
			void AddPostgraduateGuidance(Guid id, Guid entityId);
			void RemovePostgraduateGuidance(Guid id, Guid entityId);
			void AddScientificConsultation(Guid id, Guid entityId);
			void RemoveScientificConsultation(Guid id, Guid entityId);
			void AddPostgraduateDissertationGuidance(Guid id, Guid entityId);
			void RemovePostgraduateDissertationGuidance(Guid id, Guid entityId);
			void AddReview(Guid id, Guid entityId);
			void RemoveReview(Guid id, Guid entityId);
			void AddOpposition(Guid id, Guid entityId);
			void RemoveOpposition(Guid id, Guid entityId);
			void AddPatent(Guid id, Guid entityId);
			void RemovePatent(Guid id, Guid entityId);
			void AddMembership(Guid id, Guid entityId);
			void RemoveMembership(Guid id, Guid entityId);
	}
}
