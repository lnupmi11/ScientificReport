using System;
using System.Collections.Generic;
using System.Security.Claims;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DTO.Models.Grant;

namespace ScientificReport.BLL.Interfaces
{
	public interface IGrantService
	{
		IEnumerable<Grant> GetAll();
		IEnumerable<Grant> GetAllWhere(Func<Grant, bool> predicate);
		IEnumerable<Grant> GetItemsByRole(ClaimsPrincipal userClaims);
		IEnumerable<Grant> GetPageByRole(int page, int count, ClaimsPrincipal userClaims);
		int GetCountByRole(ClaimsPrincipal userClaims);
		Grant GetById(Guid id);
		Grant Get(Func<Grant, bool> predicate);
		void CreateItem(GrantModel model);
		void UpdateItem(GrantEditModel model);
		void DeleteById(Guid id);
		bool Exists(Guid id);
		void AddUser(Grant grant, UserProfile userProfile);
		void RemoveUser(Grant grant, UserProfile userProfile);
		IEnumerable<UserProfile> GetUsers(Guid id);
	}
}
