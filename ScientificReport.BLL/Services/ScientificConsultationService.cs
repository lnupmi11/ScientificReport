using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ScientificReport.BLL.Interfaces;
using ScientificReport.BLL.Utils;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using ScientificReport.DTO.Models.ScientificConsultation;

namespace ScientificReport.BLL.Services
{
	public class ScientificConsultationService : IScientificConsultationService
	{
		private readonly ScientificConsultationRepository _scientificConsultationRepository;
		private readonly DepartmentRepository _departmentRepository;
		private readonly UserProfileRepository _userProfileRepository;

		public ScientificConsultationService(ScientificReportDbContext context)
		{
			_scientificConsultationRepository = new ScientificConsultationRepository(context);
			_departmentRepository = new DepartmentRepository(context);
			_userProfileRepository = new UserProfileRepository(context);
		}

		public virtual IEnumerable<ScientificConsultation> GetAll()
		{
			return _scientificConsultationRepository.All();
		}

		public virtual IEnumerable<ScientificConsultation> GetAllWhere(Func<ScientificConsultation, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual IEnumerable<ScientificConsultation> GetItemsByRole(ClaimsPrincipal userClaims)
		{
			IEnumerable<ScientificConsultation> items;
			if (UserHelpers.IsAdmin(userClaims))
			{
				items = _scientificConsultationRepository.All();
			}
			else if (UserHelpers.IsHeadOfDepartment(userClaims))
			{
				var department = _departmentRepository.Get(r => r.Head.UserName == userClaims.Identity.Name);
				items = _scientificConsultationRepository.AllWhere(m => department.Staff.Contains(m.Guide));
			}
			else
			{
				var user = _userProfileRepository.Get(u => u.UserName == userClaims.Identity.Name);
				items = _scientificConsultationRepository.AllWhere(m => m.Guide.Id == user.Id);
			}

			return items;
		}

		public virtual IEnumerable<ScientificConsultation> GetPageByRole(int page, int count, ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCountByRole(ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Count();
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
