using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Repositories;

namespace ScientificReport.BLL.Services
{
	public class PatentLicenseActivityService : IPatentLicenseActivityService
	{
		private readonly PatentLicenseActivityRepository _patentLicenseActivityRepository;

		public PatentLicenseActivityService(ScientificReportDbContext context)
		{
			_patentLicenseActivityRepository = new PatentLicenseActivityRepository(context);
		}

		public IEnumerable<PatentLicenseActivity> GetAll()
		{
			return _patentLicenseActivityRepository.All();
		}

		public IEnumerable<PatentLicenseActivity> GetAllWhere(Func<PatentLicenseActivity, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public PatentLicenseActivity GetById(Guid id)
		{
			return _patentLicenseActivityRepository.Get(id);
		}

		public PatentLicenseActivity Get(Func<PatentLicenseActivity, bool> predicate)
		{
			return _patentLicenseActivityRepository.Get(predicate);
		}

		public void CreateItem(PatentLicenseActivity patentLicenseActivity)
		{
			_patentLicenseActivityRepository.Create(patentLicenseActivity);
		}

		public void UpdateItem(PatentLicenseActivity patentLicenseActivity)
		{
			_patentLicenseActivityRepository.Update(patentLicenseActivity);
		}

		public void DeleteById(Guid id)
		{
			_patentLicenseActivityRepository.Delete(id);
		}

		public bool Exists(Guid id)
		{
			return _patentLicenseActivityRepository.Get(id) != null;
		}

		public IEnumerable<UserProfile> GetAuthors(Guid id)
		{
			var patentLicenseActivity = _patentLicenseActivityRepository.Get(id);
			IEnumerable<UserProfile> authors = null;
			if (patentLicenseActivity != null)
			{
				authors = patentLicenseActivity.AuthorsPatentLicenseActivities.Select(u => u.Author);
			}

			return authors;
		}

		public IEnumerable<UserProfile> GetApplicants(Guid id)
		{
			var patentLicenseActivity = _patentLicenseActivityRepository.Get(id);
			IEnumerable<UserProfile> applicants = null;
			if (patentLicenseActivity != null)
			{
				applicants = patentLicenseActivity.ApplicantsPatentLicenseActivities.Select(u => u.Applicant);
			}

			return applicants;
		}
	}
}
