using System;
using System.Collections.Generic;
using System.Security.Claims;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.PostgraduateGuidance;

namespace ScientificReport.BLL.Interfaces
{
	public interface IPostgraduateGuidanceService
	{
		IEnumerable<PostgraduateGuidance> GetAll();
		IEnumerable<PostgraduateGuidance> GetAllWhere(Func<PostgraduateGuidance, bool> predicate);
		IEnumerable<PostgraduateGuidance> GetItemsByRole(ClaimsPrincipal userClaims);
		IEnumerable<PostgraduateGuidance> GetPageByRole(int page, int count, ClaimsPrincipal userClaims);
		int GetCountByRole(ClaimsPrincipal userClaims);
		PostgraduateGuidance GetById(Guid id);
		PostgraduateGuidance Get(Func<PostgraduateGuidance, bool> predicate);
		void CreateItem(PostgraduateGuidanceModel model);
		void UpdateItem(PostgraduateGuidanceEditModel model);
		void DeleteById(Guid id);
		bool Exists(Guid id);
	}
}
