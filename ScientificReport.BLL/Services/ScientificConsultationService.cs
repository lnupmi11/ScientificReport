using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using ScientificReport.DTO.Models.ScientificConsultation;

namespace ScientificReport.BLL.Services
{
	public class ScientificConsultationService : IScientificConsultationService
	{
		private readonly ScientificConsultationRepository _scientificConsultationRepository;

		public ScientificConsultationService(ScientificReportDbContext context)
		{
			_scientificConsultationRepository = new ScientificConsultationRepository(context);
		}

		public virtual IEnumerable<ScientificConsultation> GetAll()
		{
			return _scientificConsultationRepository.All();
		}

		public virtual IEnumerable<ScientificConsultation> GetAllWhere(Func<ScientificConsultation, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual IEnumerable<ScientificConsultation> GetPage(int page, int count)
		{
			return _scientificConsultationRepository.All().Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCount()
		{
			return _scientificConsultationRepository.All().Count();
		}

		public virtual ScientificConsultation GetById(Guid id)
		{
			return _scientificConsultationRepository.Get(id);
		}

		public virtual ScientificConsultation Get(Func<ScientificConsultation, bool> predicate)
		{
			return _scientificConsultationRepository.Get(predicate);
		}

		public virtual void CreateItem(ScientificConsultationModel model)
		{
			_scientificConsultationRepository.Create(new ScientificConsultation
			{
				Guide = model.Guide,
				CandidateName = model.CandidateName,
				DissertationTitle = model.DissertationTitle
			});
		}

		public virtual void UpdateItem(ScientificConsultationEditModel model)
		{
			var scientificConsultation = GetById(model.Id);
			if (scientificConsultation == null)
			{
				return;
			}

			scientificConsultation.Guide = model.Guide;
			scientificConsultation.CandidateName = model.CandidateName;
			scientificConsultation.DissertationTitle = model.DissertationTitle;
			_scientificConsultationRepository.Update(scientificConsultation);
		}

		public virtual void DeleteById(Guid id)
		{
			_scientificConsultationRepository.Delete(id);
		}

		public virtual bool Exists(Guid id)
		{
			return _scientificConsultationRepository.Get(id) != null;
		}
	}
}
