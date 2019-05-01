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
	public class ConferenceService : IConferenceService
	{
		private readonly ConferenceRepository _conferenceRepository;
		private readonly ReportThesisRepository _reportThesisRepository;

		public ConferenceService(ScientificReportDbContext context)
		{
			_conferenceRepository = new ConferenceRepository(context);
			_reportThesisRepository = new ReportThesisRepository(context);
		}

		public IEnumerable<Conference> GetAll()
		{
			return _conferenceRepository.All();
		}

		public IEnumerable<Conference> GetAllWhere(Func<Conference, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public Conference GetById(Guid id)
		{
			return _conferenceRepository.Get(id);
		}

		public Conference Get(Func<Conference, bool> predicate)
		{
			return _conferenceRepository.Get(predicate);
		}

		public void CreateItem(Conference conference)
		{
			_conferenceRepository.Create(conference);
		}

		public void UpdateItem(Conference conference)
		{
			_conferenceRepository.Update(conference);
		}

		public void DeleteById(Guid id)
		{
			_conferenceRepository.Delete(id);
		}

		public bool Exists(Guid id)
		{
			return _conferenceRepository.Get(id) != null;
		}

		public IEnumerable<ReportThesis> GetReportTheses(Guid id)
		{
			return _reportThesisRepository.AllWhere(t => t.Conference.Id.Equals(id));
		}

		public IEnumerable<UserProfile> GetParticipators(Guid id)
		{
			var theses = GetReportTheses(id).ToList();
			IList<UserProfile> participators = new List<UserProfile>();
			foreach (var thesis in theses)
			{
				participators = participators.Concat(thesis.UserProfilesReportTheses.Select(t => t.UserProfile)).ToList();
			}
			return participators;
		}
	}
}
