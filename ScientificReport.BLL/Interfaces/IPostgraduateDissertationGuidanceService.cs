using System;
using System.Collections.Generic;
using System.Security.Claims;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.PostgraduateDissertationGuidance;

namespace ScientificReport.BLL.Interfaces
{
	public interface IPostgraduateDissertationGuidanceService
	{
		IEnumerable<PostgraduateDissertationGuidance> GetAll();
		IEnumerable<PostgraduateDissertationGuidance> GetAllWhere(Func<PostgraduateDissertationGuidance, bool> predicate);
		IEnumerable<PostgraduateDissertationGuidance> GetItemsByRole(ClaimsPrincipal userClaims);
		IEnumerable<PostgraduateDissertationGuidance> GetPageByRole(int page, int count, ClaimsPrincipal userClaims);
		int GetCountByRole(ClaimsPrincipal userClaims);
		PostgraduateDissertationGuidance GetById(Guid id);
		PostgraduateDissertationGuidance Get(Func<PostgraduateDissertationGuidance, bool> predicate);
		void CreateItem(PostgraduateDissertationGuidanceModel model);
		void UpdateItem(PostgraduateDissertationGuidanceEditModel model);
		void DeleteById(Guid id);
		bool Exists(Guid id);
	}
}
