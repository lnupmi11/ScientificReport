using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.BLL.Interfaces
{
	public interface IScientificInternshipService
	{
		IEnumerable<ScientificInternship> GetAll();
		IEnumerable<ScientificInternship> GetAllWhere(Func<ScientificInternship, bool> predicate);
		ScientificInternship GetById(Guid id);
		ScientificInternship Get(Func<ScientificInternship, bool> predicate);
		void CreateItem(ScientificInternship scientificinternship);
		void UpdateItem(ScientificInternship scientificinternship);
		void DeleteById(Guid id);
		bool Exists(Guid id);
		IEnumerable<UserProfile> GetUsers(Guid id);
	}
}
