using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.BLL.Interfaces
{
	public interface IGrantService
	{
		IEnumerable<Grant> GetAll();
		IEnumerable<Grant> GetAllWhere(Func<Grant, bool> predicate);
		Grant GetById(Guid id);
		Grant Get(Func<Grant, bool> predicate);
		void CreateItem(Grant grant);
		void UpdateItem(Grant grant);
		void DeleteById(Guid id);
		bool Exists(Guid id);
		IEnumerable<UserProfile> GetUsers(Guid id);
	}
}
