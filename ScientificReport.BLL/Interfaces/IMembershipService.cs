using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.Membership;

namespace ScientificReport.BLL.Interfaces
{
	public interface IMembershipService
	{
		IEnumerable<Membership> GetAll();
		IEnumerable<Membership> GetAllWhere(Func<Membership, bool> predicate);
		IEnumerable<Membership> GetPage(int page, int count);
		int GetCount();
		Membership GetById(Guid id);
		Membership Get(Func<Membership, bool> predicate);
		void CreateItem(MembershipModel model);
		void UpdateItem(MembershipEditModel model);
		void DeleteById(Guid id);
		bool Exists(Guid id);
	}
}
