using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ScientificReport.BLL.Interfaces;
using ScientificReport.BLL.Utils;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using ScientificReport.DTO.Models.Membership;

namespace ScientificReport.BLL.Services
{
	public class MembershipService : IMembershipService
	{
		private readonly MembershipRepository _membershipRepository;
		private readonly UserProfileRepository _userProfileRepository;
		private readonly DepartmentRepository _departmentRepository;

		public MembershipService(ScientificReportDbContext context)
		{
			_membershipRepository = new MembershipRepository(context);
			_userProfileRepository = new UserProfileRepository(context);
			_departmentRepository = new DepartmentRepository(context);
		}

		public virtual IEnumerable<Membership> GetAll()
		{
			return _membershipRepository.All();
		}

		public virtual IEnumerable<Membership> GetAllWhere(Func<Membership, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual IEnumerable<Membership> GetItemsByRole(ClaimsPrincipal userClaims)
		{
			IEnumerable<Membership> items;
			if (UserHelpers.IsAdmin(userClaims))
			{
				items = _membershipRepository.All();
			}
			else if (UserHelpers.IsHeadOfDepartment(userClaims))
			{
				var department = _departmentRepository.Get(r => r.Head.UserName == userClaims.Identity.Name);
				items = _membershipRepository.AllWhere(m => department.Staff.Contains(m.User));
			}
			else
			{
				var user = _userProfileRepository.Get(u => u.UserName == userClaims.Identity.Name);
				items = _membershipRepository.AllWhere(m => m.User.Id == user.Id);
			}

			return items;
		}

		public virtual IEnumerable<Membership> GetPageByRole(int page, int count, ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCountByRole(ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Count();
		}

		public virtual Membership GetById(Guid id)
		{
			return _membershipRepository.Get(id);
		}

		public virtual Membership Get(Func<Membership, bool> predicate)
		{
			return _membershipRepository.Get(predicate);
		}

		public virtual void CreateItem(MembershipModel model)
		{
			_membershipRepository.Create(new Membership
			{
				MemberOf = model.MemberOf,
				MembershipInfo = model.MembershipInfo,
				User = model.User
			});
		}

		public virtual void UpdateItem(MembershipEditModel model)
		{
			var membership = GetById(model.Id);
			if (membership == null)
			{
				return;
			}
			
			membership.MemberOf = model.MemberOf;
			membership.MembershipInfo = model.MembershipInfo;
			_membershipRepository.Update(membership);
		}

		public virtual void DeleteById(Guid id)
		{
			_membershipRepository.Delete(id);
		}

		public virtual bool Exists(Guid id)
		{
			return _membershipRepository.Get(id) != null;
		}
	}
}
