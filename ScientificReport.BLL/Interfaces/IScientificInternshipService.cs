using System;
using System.Collections.Generic;
using System.Security.Claims;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DTO.Models.ScientificInternship;

namespace ScientificReport.BLL.Interfaces
{
	public interface IScientificInternshipService
	{
		IEnumerable<ScientificInternship> GetAll();
		IEnumerable<ScientificInternship> GetAllWhere(Func<ScientificInternship, bool> predicate);
		IEnumerable<ScientificInternship> GetItemsByRole(ClaimsPrincipal userClaims);
		IEnumerable<ScientificInternship> GetPageByRole(int page, int count, ClaimsPrincipal userClaims);
		int GetCountByRole(ClaimsPrincipal userClaims);
		ScientificInternship GetById(Guid id);
		ScientificInternship Get(Func<ScientificInternship, bool> predicate);
		void CreateItem(ScientificInternshipModel model);
		void UpdateItem(ScientificInternshipEditModel model);
		void DeleteById(Guid id);
		bool Exists(Guid id);
		void AddUser(ScientificInternship scientificInternship, UserProfile user);
		void RemoveUser(ScientificInternship scientificInternship, UserProfile user);
		IEnumerable<UserProfile> GetUsers(Guid id);
	}
}
