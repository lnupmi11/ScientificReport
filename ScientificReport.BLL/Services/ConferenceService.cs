using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ScientificReport.BLL.Interfaces;
using ScientificReport.BLL.Utils;
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
		private readonly DepartmentRepository _departmentRepository;
		private readonly UserProfileRepository _userProfileRepository;

		public ConferenceService(ScientificReportDbContext context)
		{
			_conferenceRepository = new ConferenceRepository(context);
			_reportThesisRepository = new ReportThesisRepository(context);
			_departmentRepository = new DepartmentRepository(context);
			_userProfileRepository = new UserProfileRepository(context);
		}

		public virtual IEnumerable<Conference> GetAll()
		{
			return _conferenceRepository.All();
		}

		public virtual IEnumerable<Conference> GetAllWhere(Func<Conference, bool> predicate)
		{
			return GetAll().Where(predicate);
		}
		
		public virtual IEnumerable<Conference> GetItemsByRole(ClaimsPrincipal userClaims)
		{
			IEnumerable<Conference> items;
			if (UserHelpers.IsAdmin(userClaims))
			{
				items = _conferenceRepository.All();
			}
			else if (UserHelpers.IsHeadOfDepartment(userClaims))
			{
				var department = _departmentRepository.Get(r => r.Head.UserName == userClaims.Identity.Name);
				items = _conferenceRepository.AllWhere(a => GetParticipators(a.Id).Any(u => department.Staff.Contains(u)));
			}
			else
			{
				var user = _userProfileRepository.Get(u => u.UserName == userClaims.Identity.Name);
				items = _conferenceRepository.AllWhere(a => GetParticipators(a.Id).Any(u => u.Id == user.Id));
			}

			return items;
		}
		
		public virtual IEnumerable<Conference> GetPageByRole(int page, int count, ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCountByRole(ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Count();
		}

		public virtual Conference GetById(Guid id)
		{
			return _conferenceRepository.Get(id);
		}

		public virtual Conference Get(Func<Conference, bool> predicate)
		{
			return _conferenceRepository.Get(predicate);
		}

		public virtual void CreateItem(Conference conference)
		{
			_conferenceRepository.Create(conference);
		}

		public virtual void UpdateItem(Conference conference)
		{
			_conferenceRepository.Update(conference);
		}

		public virtual void DeleteById(Guid id)
		{
			_conferenceRepository.Delete(id);
		}

		public virtual bool Exists(Guid id)
		{
			return _conferenceRepository.Get(id) != null;
		}

		public virtual IEnumerable<ReportThesis> GetReportTheses(Guid id)
		{
			return _reportThesisRepository.AllWhere(t => t.Conference.Id.Equals(id));
		}

		public virtual IEnumerable<UserProfile> GetParticipators(Guid id)
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
