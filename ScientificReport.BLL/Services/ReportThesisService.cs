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

		public IEnumerable<ReportThesis> GetAll()
		{
			return _reportThesisRepository.All();
		}

		public IEnumerable<ReportThesis> GetAllWhere(Func<ReportThesis, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public ReportThesis GetById(Guid id)
		{
			return _reportThesisRepository.Get(id);
		}

		public ReportThesis Get(Func<ReportThesis, bool> predicate)
		{
			return _reportThesisRepository.Get(predicate);
		}

		public void CreateItem(ReportThesis reportThesis)
		{
			_reportThesisRepository.Create(reportThesis);
		}

		public void UpdateItem(ReportThesis reportThesis)
		{
			_reportThesisRepository.Update(reportThesis);
		}

		public void DeleteById(Guid id)
		{
			_reportThesisRepository.Delete(id);
		}

		public bool Exists(Guid id)
		{
			return _reportThesisRepository.Get(id) != null;
		}

		public IEnumerable<UserProfile> GetAuthors(Guid id)
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
