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
	}
}
