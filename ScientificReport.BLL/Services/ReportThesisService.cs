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
using ScientificReport.DTO.Models.ReportThesis;

namespace ScientificReport.BLL.Services
{
	public class ReportThesisService : IReportThesisService
	{
		private readonly ReportThesisRepository _reportThesisRepository;
		private readonly UserProfileRepository _userProfileRepository;
		private readonly DepartmentRepository _departmentRepository;

		public ReportThesisService(ScientificReportDbContext context)
		{
			_reportThesisRepository = new ReportThesisRepository(context);
			_userProfileRepository = new UserProfileRepository(context);
			_departmentRepository = new DepartmentRepository(context);
		}

		public virtual IEnumerable<ReportThesis> GetAll()
		{
			return _reportThesisRepository.All();
		}

		public virtual IEnumerable<ReportThesis> GetAllWhere(Func<ReportThesis, bool> predicate)
		{
			return GetAll().Where(predicate);
		}
		
		public virtual IEnumerable<ReportThesis> GetItemsByRole(ClaimsPrincipal userClaims)
		{
			IEnumerable<ReportThesis> items;
			if (UserHelpers.IsAdmin(userClaims))
			{
				items = _reportThesisRepository.All();
			}
			else if (UserHelpers.IsHeadOfDepartment(userClaims))
			{
				var department = _departmentRepository.Get(r => r.Head.UserName == userClaims.Identity.Name);
				items = _reportThesisRepository.AllWhere(a => GetAuthors(a.Id).Any(u => department.Staff.Contains(u)));
			}
			else
			{
				var user = _userProfileRepository.Get(u => u.UserName == userClaims.Identity.Name);
				items = _reportThesisRepository.AllWhere(a => GetAuthors(a.Id).Any(u => u.Id == user.Id));
			}

			return items;
		}
		
		public virtual IEnumerable<ReportThesis> GetPageByRole(int page, int count, ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCountByRole(ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Count();
		}

		public virtual ReportThesis GetById(Guid id)
		{
			return _reportThesisRepository.Get(id);
		}

		public virtual ReportThesis Get(Func<ReportThesis, bool> predicate)
		{
			return _reportThesisRepository.Get(predicate);
		}

		public virtual void CreateItem(ReportThesisModel model)
		{
			_reportThesisRepository.Create(new ReportThesis
			{
				Thesis = model.Thesis,
				Conference = model.Conference
			});
		}

		public virtual void UpdateItem(ReportThesisEdit model)
		{
			var reportThesis = _reportThesisRepository.Get(model.Id);
			if (reportThesis == null)
			{
				return;
			}

			reportThesis.Thesis = model.Thesis;
			reportThesis.Conference = model.Conference;
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
