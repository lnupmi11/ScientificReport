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

		public virtual IEnumerable<ScientificInternship> GetAll()
		{
			return _scientificInternshipRepository.All();
		}

		public virtual IEnumerable<ScientificInternship> GetAllWhere(Func<ScientificInternship, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual ScientificInternship GetById(Guid id)
		{
			return _scientificInternshipRepository.Get(id);
		}

		public virtual ScientificInternship Get(Func<ScientificInternship, bool> predicate)
		{
			return _scientificInternshipRepository.Get(predicate);
		}

		public virtual void CreateItem(ScientificInternship scientificInternship)
		{
			_scientificInternshipRepository.Create(scientificInternship);
		}

		public virtual void UpdateItem(ScientificInternship scientificInternship)
		{
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
