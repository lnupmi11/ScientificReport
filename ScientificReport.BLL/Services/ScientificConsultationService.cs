using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;

namespace ScientificReport.BLL.Services
{
	public class ScientificConsultationService : IScientificConsultationService
	{
		private readonly ScientificConsultationRepository _scientificConsultationRepository;

		public ScientificConsultationService(ScientificReportDbContext context)
		{
			_scientificConsultationRepository = new ScientificConsultationRepository(context);
		}

		public IEnumerable<ScientificConsultation> GetAll()
		{
			return _scientificConsultationRepository.All();
		}

		public IEnumerable<ScientificConsultation> GetAllWhere(Func<ScientificConsultation, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public ScientificConsultation GetById(Guid id)
		{
			return _scientificConsultationRepository.Get(id);
		}

		public ScientificConsultation Get(Func<ScientificConsultation, bool> predicate)
		{
			return _scientificConsultationRepository.Get(predicate);
		}

		public void CreateItem(ScientificConsultation scientificConsultation)
		{
			_scientificConsultationRepository.Create(scientificConsultation);
		}

		public void UpdateItem(ScientificConsultation scientificConsultation)
		{
			_scientificConsultationRepository.Update(scientificConsultation);
		}

		public void DeleteById(Guid id)
		{
			_scientificConsultationRepository.Delete(id);
		}

		public bool Exists(Guid id)
		{
			return _scientificConsultationRepository.Get(id) != null;
		}
	}
}
