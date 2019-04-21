using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.Reports;

namespace ScientificReport.BLL.Interfaces
{
	public interface IScientificWorkService<in T>
	{
			IEnumerable<ScientificWork> GetAll();
			IEnumerable<ScientificWork> GetAllWhere(Func<ScientificWork, bool> predicate);
			ScientificWork GetById(T id);
			ScientificWork Get(Func<ScientificWork, bool> predicate);
			void CreateItem(ScientificWork item);
			void UpdateItem(ScientificWork item);
			void DeleteById(T id);
			bool Any(Func<ScientificWork, bool> predicate);
			bool Exists(T id);
	}
}