using System;
using System.Collections.Generic;
using System.Security.Claims;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.Membership;

namespace ScientificReport.BLL.Interfaces
{
	public interface IMembershipService
	{
		IEnumerable<Membership> GetAll();
		IEnumerable<Membership> GetAllWhere(Func<Membership, bool> predicate);
		IEnumerable<Membership> GetItemsByRole(ClaimsPrincipal userClaims);
		IEnumerable<Membership> GetPageByRole(int page, int count, ClaimsPrincipal userClaims);
		int GetCountByRole(ClaimsPrincipal userClaims);
		Membership GetById(Guid id);
		Membership Get(Func<Membership, bool> predicate);
		void CreateItem(MembershipModel model);
		void UpdateItem(MembershipEditModel model);
		void DeleteById(Guid id);
		bool Exists(Guid id);
	}
}
