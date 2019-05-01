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
	public class ScientificInternshipService : IScientificInternshipService
	{
		private readonly ScientificInternshipRepository _scientificInternshipRepository;

		public ScientificInternshipService(ScientificReportDbContext context)
		{
			_scientificInternshipRepository = new ScientificInternshipRepository(context);
		}

		public IEnumerable<ScientificInternship> GetAll()
		{
			return _scientificInternshipRepository.All();
		}

		public IEnumerable<ScientificInternship> GetAllWhere(Func<ScientificInternship, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public ScientificInternship GetById(Guid id)
		{
			return _scientificInternshipRepository.Get(id);
		}

		public ScientificInternship Get(Func<ScientificInternship, bool> predicate)
		{
			return _scientificInternshipRepository.Get(predicate);
		}

		public void CreateItem(ScientificInternship scientificInternship)
		{
			_scientificInternshipRepository.Create(scientificInternship);
		}

		public void UpdateItem(ScientificInternship scientificInternship)
		{
			_scientificInternshipRepository.Update(scientificInternship);
		}

		public void DeleteById(Guid id)
		{
			_scientificInternshipRepository.Delete(id);
		}

		public bool Exists(Guid id)
		{
			return _scientificInternshipRepository.Get(id) != null;
		}

		public IEnumerable<UserProfile> GetUsers(Guid id)
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
