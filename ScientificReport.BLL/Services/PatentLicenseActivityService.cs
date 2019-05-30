using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
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

		public PatentLicenseActivityService(ScientificReportDbContext context)
		{
			_patentLicenseActivityRepository = new PatentLicenseActivityRepository(context);
		}

		public virtual IEnumerable<PatentLicenseActivity> GetAll()
		{
			return _patentLicenseActivityRepository.All();
		}

		public virtual IEnumerable<PatentLicenseActivity> GetAllWhere(Func<PatentLicenseActivity, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual IEnumerable<PatentLicenseActivity> GetPage(int page, int count)
		{
			return _patentLicenseActivityRepository.All().Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCount()
		{
			return _patentLicenseActivityRepository.All().Count();
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
			patentLicenseActivity.Type = model.Type;
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
