using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;

namespace ScientificReport.BLL.Interfaces
{
	public interface IScientificConsultationService
	{
		IEnumerable<ScientificConsultation> GetAll();
		IEnumerable<ScientificConsultation> GetAllWhere(Func<ScientificConsultation, bool> predicate);
		ScientificConsultation GetById(Guid id);
		ScientificConsultation Get(Func<ScientificConsultation, bool> predicate);
		void CreateItem(ScientificConsultation scientificconsultation);
		void UpdateItem(ScientificConsultation scientificconsultation);
		void DeleteById(Guid id);
		bool Exists(Guid id);
	}
}
