using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ScientificReport.BLL.Interfaces;
using ScientificReport.BLL.Utils;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using ScientificReport.DTO.Models.PostgraduateDissertationGuidance;

namespace ScientificReport.BLL.Services
{
	public class PostgraduateDissertationGuidanceService : IPostgraduateDissertationGuidanceService
	{
		private readonly PostgraduateDissertationGuidanceRepository _postgraduateDissertationGuidanceRepository;
		private readonly DepartmentRepository _departmentRepository;
		private readonly UserProfileRepository _userProfileRepository;

		public PostgraduateDissertationGuidanceService(ScientificReportDbContext context)
		{
			_postgraduateDissertationGuidanceRepository = new PostgraduateDissertationGuidanceRepository(context);
			_departmentRepository = new DepartmentRepository(context);
			_userProfileRepository = new UserProfileRepository(context);
		}

		public virtual IEnumerable<PostgraduateDissertationGuidance> GetAll()
		{
			return _postgraduateDissertationGuidanceRepository.All();
		}

		public virtual IEnumerable<PostgraduateDissertationGuidance> GetAllWhere(Func<PostgraduateDissertationGuidance, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual IEnumerable<PostgraduateDissertationGuidance> GetItemsByRole(ClaimsPrincipal userClaims)
		{
			IEnumerable<PostgraduateDissertationGuidance> items;
			if (UserHelpers.IsAdmin(userClaims))
			{
				items = _postgraduateDissertationGuidanceRepository.All();
			}
			else if (UserHelpers.IsHeadOfDepartment(userClaims))
			{
				var department = _departmentRepository.Get(r => r.Head.UserName == userClaims.Identity.Name);
				items = _postgraduateDissertationGuidanceRepository.AllWhere(m => department.Staff.Contains(m.Guide));
			}
			else
			{
				var user = _userProfileRepository.Get(u => u.UserName == userClaims.Identity.Name);
				items = _postgraduateDissertationGuidanceRepository.AllWhere(m => m.Guide.Id == user.Id);
			}

			return items;
		}

		public virtual IEnumerable<PostgraduateDissertationGuidance> GetPageByRole(int page, int count, ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCountByRole(ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Count();
		}

		public virtual PostgraduateDissertationGuidance GetById(Guid id)
		{
			return _postgraduateDissertationGuidanceRepository.Get(id);
		}

		public virtual PostgraduateDissertationGuidance Get(Func<PostgraduateDissertationGuidance, bool> predicate)
		{
			return _postgraduateDissertationGuidanceRepository.Get(predicate);
		}

		public virtual void CreateItem(PostgraduateDissertationGuidanceModel model)
		{
			_postgraduateDissertationGuidanceRepository.Create(new PostgraduateDissertationGuidance
			{
				Guide = model.Guide,
				Speciality = model.Speciality,
				Dissertation = model.Dissertation,
				GraduationYear = model.GraduationYear,
				PostgraduateName = model.PostgraduateName,
				DateDegreeGained = model.DateDegreeGained
			});
		}

		public virtual void UpdateItem(PostgraduateDissertationGuidanceEditModel model)
		{
			var postgraduateDissertationGuidance = GetById(model.Id);
			if (postgraduateDissertationGuidance == null)
			{
				return;
			}

			postgraduateDissertationGuidance.Speciality = model.Speciality;
			postgraduateDissertationGuidance.Dissertation = model.Dissertation;
			postgraduateDissertationGuidance.GraduationYear = model.GraduationYear;
			postgraduateDissertationGuidance.PostgraduateName = model.PostgraduateName;
			postgraduateDissertationGuidance.DateDegreeGained = model.DateDegreeGained;
			_postgraduateDissertationGuidanceRepository.Update(postgraduateDissertationGuidance);
		}

		public virtual void DeleteById(Guid id)
		{
			_postgraduateDissertationGuidanceRepository.Delete(id);
		}

		public virtual bool Exists(Guid id)
		{
			return _postgraduateDissertationGuidanceRepository.Get(id) != null;
		}
	}
}
