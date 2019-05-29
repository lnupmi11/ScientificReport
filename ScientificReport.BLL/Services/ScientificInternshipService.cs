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
using ScientificReport.DTO.Models.ScientificInternship;

namespace ScientificReport.BLL.Services
{
	public class ScientificInternshipService : IScientificInternshipService
	{
		private readonly ScientificInternshipRepository _scientificInternshipRepository;
		private readonly DepartmentRepository _departmentRepository;
		private readonly UserProfileRepository _userProfileRepository;

		public ScientificInternshipService(ScientificReportDbContext context)
		{
			_scientificInternshipRepository = new ScientificInternshipRepository(context);
			_departmentRepository = new DepartmentRepository(context);
			_userProfileRepository = new UserProfileRepository(context);
		}

		public virtual IEnumerable<ScientificInternship> GetAll()
		{
			return _scientificInternshipRepository.All();
		}

		public virtual IEnumerable<ScientificInternship> GetAllWhere(Func<ScientificInternship, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual IEnumerable<ScientificInternship> GetItemsByRole(ClaimsPrincipal userClaims)
		{
			IEnumerable<ScientificInternship> items;
			if (UserHelpers.IsAdmin(userClaims))
			{
				items = _scientificInternshipRepository.All();
			}
			else if (UserHelpers.IsHeadOfDepartment(userClaims))
			{
				var department = _departmentRepository.Get(r => r.Head.UserName == userClaims.Identity.Name);
				items = _scientificInternshipRepository.AllWhere(m => m.UserProfilesScientificInternships.Any(p => department.Staff.Contains(p.UserProfile)));
			}
			else
			{
				var user = _userProfileRepository.Get(u => u.UserName == userClaims.Identity.Name);
				items = _scientificInternshipRepository.AllWhere(m => m.UserProfilesScientificInternships.Any(p => p.UserProfile.Id == user.Id));
			}

			return items;
		}

		public virtual IEnumerable<ScientificInternship> GetPageByRole(int page, int count, ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCountByRole(ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Count();
		}

		public virtual ScientificInternship GetById(Guid id)
		{
			return _scientificInternshipRepository.Get(id);
		}

		public virtual ScientificInternship Get(Func<ScientificInternship, bool> predicate)
		{
			return _scientificInternshipRepository.Get(predicate);
		}

		public virtual void CreateItem(ScientificInternshipModel model)
		{
			_scientificInternshipRepository.Create(new ScientificInternship
			{
				PlaceOfInternship = model.PlaceOfInternship,
				Contents = model.Contents,
				Started = model.Started,
				Ended = model.Ended
			});
		}

		public virtual void UpdateItem(ScientificInternshipEditModel model)
		{
			var scientificInternship = GetById(model.Id);
			if (scientificInternship == null)
			{
				return;
			}

			scientificInternship.PlaceOfInternship = model.PlaceOfInternship;
			scientificInternship.Contents = model.Contents;
			scientificInternship.Started = model.Started;
			scientificInternship.Ended = model.Ended;
			_scientificInternshipRepository.Update(scientificInternship);
		}

		public virtual void DeleteById(Guid id)
		{
			_scientificInternshipRepository.Delete(id);
		}

		public virtual bool Exists(Guid id)
		{
			return _scientificInternshipRepository.Get(id) != null;
		}

		public void AddUser(ScientificInternship scientificInternship, UserProfile user)
		{
			if (scientificInternship == null || user == null)
			{
				return;
			}
			
			scientificInternship.UserProfilesScientificInternships.Add(new UserProfilesScientificInternships
			{
				UserProfile = user,
				UserProfileId = user.Id,
				ScientificInternship = scientificInternship,
				ScientificInternshipId = scientificInternship.Id
			});
			_scientificInternshipRepository.Update(scientificInternship);
		}

		public void RemoveUser(ScientificInternship scientificInternship, UserProfile user)
		{
			if (scientificInternship == null || user == null)
			{
				return;
			}

			scientificInternship.UserProfilesScientificInternships.Remove(
				scientificInternship.UserProfilesScientificInternships.First(p => p.UserProfile.Id == user.Id));
			_scientificInternshipRepository.Update(scientificInternship);
		}

		public virtual IEnumerable<UserProfile> GetUsers(Guid id)
		{
			var scientificInternship = _scientificInternshipRepository.Get(id);
			IEnumerable<UserProfile> users = null;
			if (scientificInternship != null)
			{
				users = scientificInternship.UserProfilesScientificInternships.Select(u => u.UserProfile);
			}

			return users;
		}
	}
}
