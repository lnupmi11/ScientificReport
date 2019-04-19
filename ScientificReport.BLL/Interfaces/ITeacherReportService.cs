using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.Reports;

namespace ScientificReport.BLL.Interfaces
{
	public interface ITeacherReportService<in T>
	{
			IEnumerable<TeacherReport> GetAll();
			IEnumerable<TeacherReport> GetAllWhere(Func<TeacherReport, bool> predicate);
			TeacherReport GetById(T id);
			TeacherReport Get(Func<TeacherReport, bool> predicate);
			void CreateItem(TeacherReport item);
			void UpdateItem(TeacherReport item);
			void DeleteById(T id);
			bool Any(Func<TeacherReport, bool> predicate);
			bool Exists(T id);
	}
}