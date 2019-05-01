using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.BLL.Interfaces
{
	public interface IOppositionService
	{
		IEnumerable<Opposition> GetAll();
		IEnumerable<Opposition> GetAllWhere(Func<Opposition, bool> predicate);
		Opposition GetById(Guid id);
		Opposition Get(Func<Opposition, bool> predicate);
		void CreateItem(Opposition opposition);
		void UpdateItem(Opposition opposition);
		void DeleteById(Guid id);
		bool Exists(Guid id);
	}
}
