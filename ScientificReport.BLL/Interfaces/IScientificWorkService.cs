using System;
using System.Collections.Generic;
using System.Security.Claims;
using ScientificReport.DAL.Entities.Publications;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.BLL.Interfaces
{
	public interface IScientificWorkService
	{
		IEnumerable<ScientificWork> GetAll();
		IEnumerable<ScientificWork> GetAllWhere(Func<ScientificWork, bool> predicate);
		IEnumerable<ScientificWork> GetItemsByRole(ClaimsPrincipal userClaims);
		IEnumerable<ScientificWork> GetPageByRole(int page, int count, ClaimsPrincipal userClaims);
		int GetCountByRole(ClaimsPrincipal userClaims);
		ScientificWork GetById(Guid id);
		ScientificWork Get(Func<ScientificWork, bool> predicate);
		void CreateItem(ScientificWork item);
		void UpdateItem(ScientificWork item);
		void DeleteById(Guid id);
		bool Any(Func<ScientificWork, bool> predicate);
		bool Exists(Guid id);
		IEnumerable<UserProfile> GetAuthors(Guid id);
		void AddAuthor(Guid id, Guid authorId);
		void RemoveAuthor(Guid id, Guid authorId);
	}
}
