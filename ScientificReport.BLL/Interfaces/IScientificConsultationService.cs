using System;
using System.Collections.Generic;
using System.Security.Claims;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.ScientificConsultation;

namespace ScientificReport.BLL.Interfaces
{
	public interface IScientificConsultationService
	{
		IEnumerable<ScientificConsultation> GetAll();
		IEnumerable<ScientificConsultation> GetAllWhere(Func<ScientificConsultation, bool> predicate);
		IEnumerable<ScientificConsultation> GetItemsByRole(ClaimsPrincipal userClaims);
		IEnumerable<ScientificConsultation> GetPageByRole(int page, int count, ClaimsPrincipal userClaims);
		int GetCountByRole(ClaimsPrincipal userClaims);
		ScientificConsultation GetById(Guid id);
		ScientificConsultation Get(Func<ScientificConsultation, bool> predicate);
		void CreateItem(ScientificConsultationModel model);
		void UpdateItem(ScientificConsultationEditModel model);
		void DeleteById(Guid id);
		bool Exists(Guid id);
	}
}
