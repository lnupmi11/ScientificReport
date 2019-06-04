using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ScientificReport.BLL.Interfaces;
using ScientificReport.BLL.Utils;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.Publications;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Repositories;

namespace ScientificReport.BLL.Services
{
	public class ScientificWorkService : IScientificWorkService
	{
		private readonly ScientificWorkRepository _scientificWorkRepository;
		private readonly UserProfileRepository _userProfileRepository;
		private readonly DepartmentRepository _departmentRepository;

		public ScientificWorkService(ScientificReportDbContext context)
		{
			_scientificWorkRepository = new ScientificWorkRepository(context);
			_userProfileRepository = new UserProfileRepository(context);
			_departmentRepository = new DepartmentRepository(context);
		}

		public virtual IEnumerable<ScientificWork> GetAll()
		{
			return _scientificWorkRepository.All();
		}

		public virtual IEnumerable<ScientificWork> GetAllWhere(Func<ScientificWork, bool> predicate)
		{
			return _scientificWorkRepository.AllWhere(predicate);
		}
		
		public virtual IEnumerable<ScientificWork> GetItemsByRole(ClaimsPrincipal userClaims)
		{
			IEnumerable<ScientificWork> items;
			if (UserHelpers.IsAdmin(userClaims))
			{
				items = _scientificWorkRepository.All();
			}
			else if (UserHelpers.IsHeadOfDepartment(userClaims))
			{
				var department = _departmentRepository.Get(r => r.Head.UserName == userClaims.Identity.Name);
				items = _scientificWorkRepository.AllWhere(a => a.UserProfilesScientificWorks.Any(u => department.Staff.Contains(u.UserProfile)));
			}
			else
			{
				var user = _userProfileRepository.Get(u => u.UserName == userClaims.Identity.Name);
				items = _scientificWorkRepository.AllWhere(a => a.UserProfilesScientificWorks.Any(u => u.UserProfile.Id == user.Id));
			}

			return items;
		}
		
		public virtual IEnumerable<ScientificWork> GetPageByRole(int page, int count, ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCountByRole(ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Count();
		}

		public virtual ScientificWork GetById(Guid id)
		{
			return _scientificWorkRepository.Get(id);
		}

		public virtual ScientificWork Get(Func<ScientificWork, bool> predicate)
		{
			return _scientificWorkRepository.Get(predicate);
		}

		public virtual void CreateItem(ScientificWork item)
		{
			_scientificWorkRepository.Create(item);
		}

		public virtual void UpdateItem(ScientificWork item)
		{
			_scientificWorkRepository.Update(item);
		}

		public virtual void DeleteById(Guid id)
		{
			_scientificWorkRepository.Delete(id);
		}

		public virtual bool Any(Func<ScientificWork, bool> predicate)
		{
			return _scientificWorkRepository.AllWhere(predicate).Any();
		}

		public virtual bool Exists(Guid id)
		{
			return Any(r => r.Id == id);
		}

		public virtual IEnumerable<UserProfile> GetAuthors(Guid id)
		{
			var scientificWork = _scientificWorkRepository.Get(id);
			IEnumerable<UserProfile> authors = null;
			if (scientificWork != null)
			{
				authors = scientificWork.UserProfilesScientificWorks.Select(u => u.UserProfile);
			}

			return authors;
		}

		public virtual void AddAuthor(Guid id, Guid authorId)
		{
			var scientificWork = _scientificWorkRepository.Get(id);
			var author = _userProfileRepository.Get(authorId);
			if (scientificWork.UserProfilesScientificWorks.Any(u => u.UserProfile.Id == authorId))
			{
				return;
			}

			scientificWork.UserProfilesScientificWorks.Add(new UserProfilesScientificWorks
			{
				ScientificWork = scientificWork,
				UserProfile = author
			});
			_scientificWorkRepository.Update(scientificWork);
		}

		public virtual void RemoveAuthor(Guid id, Guid authorId)
		{
			var scientificWork = _scientificWorkRepository.Get(id);
			if (scientificWork.UserProfilesScientificWorks.All(u => u.UserProfile.Id != authorId))
			{
				return;
			}

			scientificWork.UserProfilesScientificWorks = scientificWork.UserProfilesScientificWorks
				.Where(u => u.UserProfile.Id != authorId).ToList();
			_scientificWorkRepository.Update(scientificWork);
		}
	}
}
