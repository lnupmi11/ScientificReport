using System;
using System.Collections.Generic;
using System.Security.Claims;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.Opposition;

namespace ScientificReport.BLL.Interfaces
{
	public interface IOppositionService
	{
		IEnumerable<Opposition> GetAll();
		IEnumerable<Opposition> GetAllWhere(Func<Opposition, bool> predicate);
		IEnumerable<Opposition> GetItemsByRole(ClaimsPrincipal userClaims);
		IEnumerable<Opposition> GetPageByRole(int page, int count, ClaimsPrincipal userClaims);
		int GetCountByRole(ClaimsPrincipal userClaims);
		Opposition GetById(Guid id);
		Opposition Get(Func<Opposition, bool> predicate);
		void CreateItem(OppositionModel model);
		void UpdateItem(OppositionEditModel model);
		void DeleteById(Guid id);
		bool Exists(Guid id);
	}
}
