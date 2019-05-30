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
using ScientificReport.DTO.Models.Grant;

namespace ScientificReport.BLL.Services
{
	public class GrantService : IGrantService
	{
		private readonly GrantRepository _grantRepository;
		private readonly DepartmentRepository _departmentRepository;
		private readonly UserProfileRepository _userProfileRepository;

		public GrantService(ScientificReportDbContext context)
		{
			_grantRepository = new GrantRepository(context);
			_departmentRepository = new DepartmentRepository(context);
			_userProfileRepository = new UserProfileRepository(context);
		}

		public virtual IEnumerable<Grant> GetAll()
		{
			return _grantRepository.All();
		}

		public virtual IEnumerable<Grant> GetAllWhere(Func<Grant, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual IEnumerable<Grant> GetItemsByRole(ClaimsPrincipal userClaims)
		{
			IEnumerable<Grant> items;
			if (UserHelpers.IsAdmin(userClaims))
			{
				items = _grantRepository.All();
			}
			else if (UserHelpers.IsHeadOfDepartment(userClaims))
			{
				var department = _departmentRepository.Get(r => r.Head.UserName == userClaims.Identity.Name);
				items = _grantRepository.AllWhere(
					a => a.UserProfilesGrants.Any(u => department.Staff.Contains(u.UserProfile))
				);
			}
			else
			{
				var user = _userProfileRepository.Get(u => u.UserName == userClaims.Identity.Name);
				items = _grantRepository.AllWhere(
					a => a.UserProfilesGrants.Any(u => u.UserProfile.Id == user.Id)
				);
			}

			return items;
		}
		
		public virtual IEnumerable<Grant> GetPageByRole(int page, int count, ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCountByRole(ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Count();
		}

		public virtual Grant GetById(Guid id)
		{
			return _grantRepository.Get(id);
		}

		public virtual Grant Get(Func<Grant, bool> predicate)
		{
			return _grantRepository.Get(predicate);
		}

		public virtual void CreateItem(GrantModel model)
		{
			_grantRepository.Create(new Grant()
			{
				Info = model.Info
			});
		}

		public virtual void UpdateItem(GrantEditModel model)
		{
			var grant = GetById(model.Id);
			if (grant == null)
			{
				return;
			}

			grant.Info = model.Info;
			_grantRepository.Update(grant);
		}

		public virtual void DeleteById(Guid id)
		{
			_grantRepository.Delete(id);
		}

		public virtual bool Exists(Guid id)
		{
			return _grantRepository.Get(id) != null;
		}

		public void AddUser(Grant grant, UserProfile userProfile)
		{
			if (grant == null || userProfile == null)
			{
				return;
			}
			
			grant.UserProfilesGrants.Add(new UserProfilesGrants
			{
				Grant = grant,
				GrantId = grant.Id,
				UserProfile = userProfile,
				UserProfileId = userProfile.Id
			});
			_grantRepository.Update(grant);
		}
		
		public void RemoveUser(Grant grant, UserProfile userProfile)
		{
			if (grant == null || userProfile == null)
			{
				return;
			}
			
			grant.UserProfilesGrants.Remove(grant.UserProfilesGrants.First(u => u.UserProfile.Id == userProfile.Id));
			_grantRepository.Update(grant);
		}

		public virtual IEnumerable<UserProfile> GetUsers(Guid id)
		{
			var grant = _grantRepository.Get(id);
			IEnumerable<UserProfile> users = null;
			if (grant != null)
			{
				users = grant.UserProfilesGrants.Select(u => u.UserProfile);
			}

			return users;
		}
	}
}
