using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Repositories;

namespace ScientificReport.BLL.Services
{
	public class ReportThesisService : IReportThesisService
	{
		private readonly ReportThesisRepository _reportThesisRepository;

		public ReportThesisService(ScientificReportDbContext context)
		{
			_reportThesisRepository = new ReportThesisRepository(context);
		}

		public virtual IEnumerable<ReportThesis> GetAll()
		{
			return _reportThesisRepository.All();
		}

		public virtual IEnumerable<ReportThesis> GetAllWhere(Func<ReportThesis, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual ReportThesis GetById(Guid id)
		{
			return _reportThesisRepository.Get(id);
		}

		public virtual ReportThesis Get(Func<ReportThesis, bool> predicate)
		{
			return _reportThesisRepository.Get(predicate);
		}

		public virtual void CreateItem(ReportThesis reportThesis)
		{
			_reportThesisRepository.Create(reportThesis);
		}

		public virtual void UpdateItem(ReportThesis reportThesis)
		{
			_reportThesisRepository.Update(reportThesis);
		}

		public virtual void DeleteById(Guid id)
		{
			_reportThesisRepository.Delete(id);
		}

		public virtual bool Exists(Guid id)
		{
			return _reportThesisRepository.Get(id) != null;
		}

		public virtual IEnumerable<UserProfile> GetAuthors(Guid id)
		{
			var reportThesis = _reportThesisRepository.Get(id);
			IEnumerable<UserProfile> authors = null;
			if (reportThesis != null)
			{
				authors = reportThesis.UserProfilesReportTheses.Select(u => u.UserProfile);
			}

			return authors;
		}
	}
}
