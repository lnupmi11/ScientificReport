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
using ScientificReport.DTO.Models.PatentLicenseActivity;

namespace ScientificReport.BLL.Services
{
	public class PatentLicenseActivityService : IPatentLicenseActivityService
	{
		private readonly PatentLicenseActivityRepository _patentLicenseActivityRepository;
		private readonly UserProfileRepository _userProfileRepository;
		private readonly DepartmentRepository _departmentRepository;

		public PatentLicenseActivityService(ScientificReportDbContext context)
		{
			_patentLicenseActivityRepository = new PatentLicenseActivityRepository(context);
			_userProfileRepository = new UserProfileRepository(context);
			_departmentRepository = new DepartmentRepository(context);
		}

		public virtual IEnumerable<PatentLicenseActivity> GetAll()
		{
			return _patentLicenseActivityRepository.All();
		}

		public virtual IEnumerable<PatentLicenseActivity> GetAllWhere(Func<PatentLicenseActivity, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual IEnumerable<PatentLicenseActivity> GetItemsByRole(ClaimsPrincipal userClaims)
		{
			IEnumerable<PatentLicenseActivity> items;
			if (UserHelpers.IsAdmin(userClaims))
			{
				items = _patentLicenseActivityRepository.All();
			}
			else if (UserHelpers.IsHeadOfDepartment(userClaims))
			{
				var department = _departmentRepository.Get(r => r.Head.UserName == userClaims.Identity.Name);
				items = _patentLicenseActivityRepository.AllWhere(
					a => a.AuthorsPatentLicenseActivities.Any(u => department.Staff.Contains(u.Author)) ||
					a.ApplicantsPatentLicenseActivities.Any(u => department.Staff.Contains(u.Applicant))
				);
			}
			else
			{
				var user = _userProfileRepository.Get(u => u.UserName == userClaims.Identity.Name);
				items = _patentLicenseActivityRepository.AllWhere(
					a => a.AuthorsPatentLicenseActivities.Any(u => u.Author.Id == user.Id) ||
					a.ApplicantsPatentLicenseActivities.Any(u => u.Applicant.Id == user.Id)
				);
			}

			return items;
		}
		
		public virtual IEnumerable<PatentLicenseActivity> GetPageByRole(int page, int count, ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCountByRole(ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Count();
		}

		public virtual PatentLicenseActivity GetById(Guid id)
		{
			return _patentLicenseActivityRepository.Get(id);
		}

		public virtual PatentLicenseActivity Get(Func<PatentLicenseActivity, bool> predicate)
		{
			return _patentLicenseActivityRepository.Get(predicate);
		}

		public virtual void CreateItem(PatentLicenseActivityModel model)
		{
			_patentLicenseActivityRepository.Create(new PatentLicenseActivity
			{
				Name = model.Name,
				Type = model.Type,
				Number = model.Number,
				Date = model.DateTime
			});
		}

		public virtual void UpdateItem(PatentLicenseActivityEditModel model)
		{
			var patentLicenseActivity = GetById(model.Id);
			if (patentLicenseActivity == null)
			{
				return;
			}

			patentLicenseActivity.Name = model.Name;
			if (patentLicenseActivity.Type != model.Type)
			{
				if (model.Type == PatentLicenseActivity.Types.Patent)
				{
					var users = patentLicenseActivity.ApplicantsPatentLicenseActivities.Select(u => u.Applicant).ToList();
					for (var i = 0; i < users.Count; i++)
					{
						RemoveApplicant(patentLicenseActivity.Id, users[i]);
						AddAuthor(patentLicenseActivity, users[i]);
					}
				}
				else
				{
					var users = patentLicenseActivity.AuthorsPatentLicenseActivities.Select(u => u.Author).ToList();
					for (var i = 0; i < users.Count; i++)
					{
						RemoveAuthor(patentLicenseActivity.Id, users[i]);
						AddApplicant(patentLicenseActivity, users[i]);
					}
				}
				
				patentLicenseActivity.Type = model.Type;	
			}
			patentLicenseActivity.Number = model.Number;
			patentLicenseActivity.Date = model.DateTime;
			_patentLicenseActivityRepository.Update(patentLicenseActivity);
		}

		public virtual void DeleteById(Guid id)
		{
			_patentLicenseActivityRepository.Delete(id);
		}

		public virtual bool Exists(Guid id)
		{
			return _patentLicenseActivityRepository.Get(id) != null;
		}

		public virtual void AddAuthor(PatentLicenseActivity patentLicenseActivity, UserProfile user)
		{
			if (patentLicenseActivity == null)
			{
				return;
			}
			
			patentLicenseActivity.AuthorsPatentLicenseActivities.Add(new AuthorsPatentLicenseActivities
			{
				Author = user,
				AuthorId = user.Id,
				PatentLicenseActivity = patentLicenseActivity,
				PatentLicenseActivityId = patentLicenseActivity.Id
			});
			_patentLicenseActivityRepository.Update(patentLicenseActivity);
		}
		
		public virtual void RemoveAuthor(Guid id, UserProfile user)
		{
			var patentLicenseActivity = _patentLicenseActivityRepository.Get(id);
			if (patentLicenseActivity == null)
			{
				return;
			}
			
			patentLicenseActivity.AuthorsPatentLicenseActivities.Remove(patentLicenseActivity.AuthorsPatentLicenseActivities.First(u => u.Author.Id == user.Id));
			_patentLicenseActivityRepository.Update(patentLicenseActivity);
		}

		public virtual void AddApplicant(PatentLicenseActivity patentLicenseActivity, UserProfile user)
		{
			if (patentLicenseActivity == null)
			{
				return;
			}
			
			patentLicenseActivity.ApplicantsPatentLicenseActivities.Add(new ApplicantsPatentLicenseActivities
			{
				Applicant = user,
				ApplicantId = user.Id,
				PatentLicenseActivity = patentLicenseActivity,
				PatentLicenseActivityId = patentLicenseActivity.Id
			});
			_patentLicenseActivityRepository.Update(patentLicenseActivity);
		}

		public virtual void RemoveApplicant(Guid id, UserProfile user)
		{
			var patentLicenseActivity = _patentLicenseActivityRepository.Get(id);
			if (patentLicenseActivity == null)
			{
				return;
			}
			
			patentLicenseActivity.ApplicantsPatentLicenseActivities.Remove(patentLicenseActivity.ApplicantsPatentLicenseActivities.First(u => u.Applicant.Id == user.Id));
			_patentLicenseActivityRepository.Update(patentLicenseActivity);
		}
		
		public virtual void AddCoauthor(Guid id, string coauthor)
		{
			var patentLicenseActivity = _patentLicenseActivityRepository.Get(id);
			if (patentLicenseActivity == null)
			{
				return;
			}
			
			patentLicenseActivity.CoauthorsPatentLicenseActivities.Add(new CoauthorsPatentLicenseActivities
			{
				Coauthor = coauthor,
				PatentLicenseActivity = patentLicenseActivity,
				PatentLicenseActivityId = patentLicenseActivity.Id
			});
			_patentLicenseActivityRepository.Update(patentLicenseActivity);
		}
		
		public virtual void RemoveCoauthor(Guid id, string coauthor)
		{
			var patentLicenseActivity = _patentLicenseActivityRepository.Get(id);
			if (patentLicenseActivity == null)
			{
				return;
			}
			
			patentLicenseActivity.CoauthorsPatentLicenseActivities.Remove(patentLicenseActivity.CoauthorsPatentLicenseActivities.First(u => u.Coauthor == coauthor));
			_patentLicenseActivityRepository.Update(patentLicenseActivity);
		}

		public virtual void AddCoApplicant(Guid id, string coApplicant)
		{
			var patentLicenseActivity = _patentLicenseActivityRepository.Get(id);
			if (patentLicenseActivity == null)
			{
				return;
			}
			
			patentLicenseActivity.CoApplicantsPatentLicenseActivities.Add(new CoApplicantsPatentLicenseActivities
			{
				CoApplicant = coApplicant,
				PatentLicenseActivity = patentLicenseActivity,
				PatentLicenseActivityId = patentLicenseActivity.Id
			});
			_patentLicenseActivityRepository.Update(patentLicenseActivity);
		}

		public virtual void RemoveCoApplicant(Guid id, string coApplicant)
		{
			var patentLicenseActivity = _patentLicenseActivityRepository.Get(id);
			if (patentLicenseActivity == null)
			{
				return;
			}
			
			patentLicenseActivity.CoApplicantsPatentLicenseActivities.Remove(patentLicenseActivity.CoApplicantsPatentLicenseActivities.First(u => u.CoApplicant == coApplicant));
			_patentLicenseActivityRepository.Update(patentLicenseActivity);
		}

		public virtual IEnumerable<UserProfile> GetAuthors(Guid id)
		{
			var patentLicenseActivity = _patentLicenseActivityRepository.Get(id);
			IEnumerable<UserProfile> authors = null;
			if (patentLicenseActivity != null)
			{
				authors = patentLicenseActivity.AuthorsPatentLicenseActivities.Select(u => u.Author);
			}

			return authors;
		}

		public virtual IEnumerable<UserProfile> GetApplicants(Guid id)
		{
			var patentLicenseActivity = _patentLicenseActivityRepository.Get(id);
			IEnumerable<UserProfile> applicants = null;
			if (patentLicenseActivity != null)
			{
				applicants = patentLicenseActivity.ApplicantsPatentLicenseActivities.Select(u => u.Applicant);
			}

			return applicants;
		}
		
		public virtual IEnumerable<string> GetCoauthors(Guid id)
		{
			var patentLicenseActivity = _patentLicenseActivityRepository.Get(id);
			IEnumerable<string> coauthors = null;
			if (patentLicenseActivity != null)
			{
				coauthors = patentLicenseActivity.CoauthorsPatentLicenseActivities.Select(u => u.Coauthor);
			}

			return coauthors;
		}

		public virtual IEnumerable<string> GetCoApplicants(Guid id)
		{
			var patentLicenseActivity = _patentLicenseActivityRepository.Get(id);
			IEnumerable<string> coApplicants = null;
			if (patentLicenseActivity != null)
			{
				coApplicants = patentLicenseActivity.CoApplicantsPatentLicenseActivities.Select(u => u.CoApplicant);
			}

			return coApplicants;
		}
	}
}
