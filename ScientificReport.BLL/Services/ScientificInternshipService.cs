using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
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

		public virtual IEnumerable<ScientificInternship> GetPage(int page, int count)
		{
			return _scientificInternshipRepository.All().Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCount()
		{
			return _scientificInternshipRepository.All().Count();
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
