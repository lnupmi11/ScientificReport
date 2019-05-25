using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.ScientificConsultation;

namespace ScientificReport.BLL.Interfaces
{
	public interface IScientificConsultationService
	{
		IEnumerable<ScientificConsultation> GetAll();
		IEnumerable<ScientificConsultation> GetAllWhere(Func<ScientificConsultation, bool> predicate);
		IEnumerable<ScientificConsultation> GetPage(int page, int count);
		int GetCount();
		ScientificConsultation GetById(Guid id);
		ScientificConsultation Get(Func<ScientificConsultation, bool> predicate);
		void CreateItem(ScientificConsultationModel model);
		void UpdateItem(ScientificConsultationEditModel model);
		void DeleteById(Guid id);
		bool Exists(Guid id);
	}
}
