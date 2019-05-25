using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.Opposition;

namespace ScientificReport.BLL.Interfaces
{
	public interface IOppositionService
	{
		IEnumerable<Opposition> GetAll();
		IEnumerable<Opposition> GetAllWhere(Func<Opposition, bool> predicate);
		IEnumerable<Opposition> GetPage(int page, int count);
		int GetCount();
		Opposition GetById(Guid id);
		Opposition Get(Func<Opposition, bool> predicate);
		void CreateItem(OppositionModel model);
		void UpdateItem(OppositionEditModel model);
		void DeleteById(Guid id);
		bool Exists(Guid id);
	}
}
