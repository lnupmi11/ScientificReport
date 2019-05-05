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
		private readonly UserProfileRepository _userProfileRepository;

		public ReportThesisService(ScientificReportDbContext context)
		{
			_reportThesisRepository = new ReportThesisRepository(context);
			_userProfileRepository = new UserProfileRepository(context);
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
		
		public void AddAuthor(Guid id, Guid authorId)
		{
			var reportThesis = _reportThesisRepository.Get(id);
			var author = _userProfileRepository.Get(authorId);
			if (reportThesis.UserProfilesReportTheses.Any(u => u.UserProfile.Id == authorId))
			{
				return;
			}

			reportThesis.UserProfilesReportTheses.Add(new UserProfilesReportThesis
			{
				ReportThesis = reportThesis,
				UserProfile = author
			});
			_reportThesisRepository.Update(reportThesis);
		}

		public void RemoveAuthor(Guid id, Guid authorId)
		{
			var reportThesis = _reportThesisRepository.Get(id);
			if (reportThesis.UserProfilesReportTheses.All(u => u.UserProfile.Id != authorId))
			{
				return;
			}

			reportThesis.UserProfilesReportTheses = reportThesis.UserProfilesReportTheses
				.Where(u => u.UserProfile.Id != authorId).ToList();
			_reportThesisRepository.Update(reportThesis);
		}
	}
}
