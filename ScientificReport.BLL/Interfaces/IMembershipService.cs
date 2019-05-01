using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;

namespace ScientificReport.BLL.Interfaces
{
	public interface IMembershipService
	{
		IEnumerable<Membership> GetAll();
		IEnumerable<Membership> GetAllWhere(Func<Membership, bool> predicate);
		Membership GetById(Guid id);
		Membership Get(Func<Membership, bool> predicate);
		void CreateItem(Membership membership);
		void UpdateItem(Membership membership);
		void DeleteById(Guid id);
		bool Exists(Guid id);
	}
}
