using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DTO.Models.ScientificInternship;

namespace ScientificReport.BLL.Interfaces
{
	public interface IScientificInternshipService
	{
		IEnumerable<ScientificInternship> GetAll();
		IEnumerable<ScientificInternship> GetAllWhere(Func<ScientificInternship, bool> predicate);
		IEnumerable<ScientificInternship> GetPage(int page, int count);
		int GetCount();
		ScientificInternship GetById(Guid id);
		ScientificInternship Get(Func<ScientificInternship, bool> predicate);
		void CreateItem(ScientificInternshipModel model);
		void UpdateItem(ScientificInternshipEditModel model);
		void DeleteById(Guid id);
		bool Exists(Guid id);
		IEnumerable<UserProfile> GetUsers(Guid id);
	}
}
