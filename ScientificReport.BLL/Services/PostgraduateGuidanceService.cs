using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ScientificReport.BLL.Interfaces;
using ScientificReport.BLL.Utils;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using ScientificReport.DTO.Models.PostgraduateGuidance;

namespace ScientificReport.BLL.Services
{
	public class PostgraduateGuidanceService : IPostgraduateGuidanceService
	{
		private readonly PostgraduateGuidanceRepository _postgraduateGuidanceRepository;
		private readonly DepartmentRepository _departmentRepository;
		private readonly UserProfileRepository _userProfileRepository;

		public PostgraduateGuidanceService(ScientificReportDbContext context)
		{
			_postgraduateGuidanceRepository = new PostgraduateGuidanceRepository(context);
			_departmentRepository = new DepartmentRepository(context);
			_userProfileRepository = new UserProfileRepository(context);
		}

		public virtual IEnumerable<PostgraduateGuidance> GetAll()
		{
			return _postgraduateGuidanceRepository.All();
		}

		public virtual IEnumerable<PostgraduateGuidance> GetAllWhere(Func<PostgraduateGuidance, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual IEnumerable<PostgraduateGuidance> GetItemsByRole(ClaimsPrincipal userClaims)
		{
			IEnumerable<PostgraduateGuidance> items;
			if (UserHelpers.IsAdmin(userClaims))
			{
				items = _postgraduateGuidanceRepository.All();
			}
			else if (UserHelpers.IsHeadOfDepartment(userClaims))
			{
				var department = _departmentRepository.Get(r => r.Head.UserName == userClaims.Identity.Name);
				items = _postgraduateGuidanceRepository.AllWhere(m => department.Staff.Contains(m.Guide));
			}
			else
			{
				var user = _userProfileRepository.Get(u => u.UserName == userClaims.Identity.Name);
				items = _postgraduateGuidanceRepository.AllWhere(m => m.Guide.Id == user.Id);
			}

			return items;
		}

		public virtual IEnumerable<PostgraduateGuidance> GetPageByRole(int page, int count, ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCountByRole(ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Count();
		}

		public virtual PostgraduateGuidance GetById(Guid id)
		{
			return _postgraduateGuidanceRepository.Get(id);
		}

		public virtual PostgraduateGuidance Get(Func<PostgraduateGuidance, bool> predicate)
		{
			return _postgraduateGuidanceRepository.Get(predicate);
		}

		public virtual void CreateItem(PostgraduateGuidanceModel model)
		{
			_postgraduateGuidanceRepository.Create(new PostgraduateGuidance
			{
				Guide = model.Guide,
				PostgraduateInfo = model.PostgraduateInfo,
				PostgraduateName = model.PostgraduateName
			});
		}

		public virtual void UpdateItem(PostgraduateGuidanceEditModel model)
		{
			var postgraduateGuidance = GetById(model.Id);
			if (postgraduateGuidance == null)
			{
				return;
			}

			postgraduateGuidance.Guide = model.Guide;
			postgraduateGuidance.PostgraduateInfo = model.PostgraduateInfo;
			postgraduateGuidance.PostgraduateName = model.PostgraduateName;
			_postgraduateGuidanceRepository.Update(postgraduateGuidance);
		}

		public virtual void DeleteById(Guid id)
		{
			_postgraduateGuidanceRepository.Delete(id);
		}

		public virtual bool Exists(Guid id)
		{
			return _postgraduateGuidanceRepository.Get(id) != null;
		}
	}
}
