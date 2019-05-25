using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DTO.Models.Grant;

namespace ScientificReport.BLL.Interfaces
{
	public interface IGrantService
	{
		IEnumerable<Grant> GetAll();
		IEnumerable<Grant> GetAllWhere(Func<Grant, bool> predicate);
		IEnumerable<Grant> GetPage(int page, int count);
		int GetCount();
		Grant GetById(Guid id);
		Grant Get(Func<Grant, bool> predicate);
		void CreateItem(GrantModel model);
		void UpdateItem(GrantEditModel model);
		void DeleteById(Guid id);
		bool Exists(Guid id);
		IEnumerable<UserProfile> GetUsers(Guid id);
	}
}
