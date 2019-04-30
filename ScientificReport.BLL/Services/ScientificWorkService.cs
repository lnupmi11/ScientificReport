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
	public class ScientificWorkService : IScientificWorkService
	{
		private readonly ScientificWorkRepository _scientificWorkRepository;
		private readonly UserProfileRepository _userProfileRepository;

		public ScientificWorkService(ScientificReportDbContext context)
		{
			_scientificWorkRepository = new ScientificWorkRepository(context);
			_userProfileRepository = new UserProfileRepository(context);
		}

		public IEnumerable<ScientificWork> GetAll()
		{
			return _scientificWorkRepository.All();
		}

		public IEnumerable<ScientificWork> GetAllWhere(Func<ScientificWork, bool> predicate)
		{
			return _scientificWorkRepository.AllWhere(predicate);
		}

		public ScientificWork GetById(Guid id)
		{
			return _scientificWorkRepository.Get(id);
		}

		public ScientificWork Get(Func<ScientificWork, bool> predicate)
		{
			return _scientificWorkRepository.Get(predicate);
		}

		public void CreateItem(ScientificWork item)
		{
			_scientificWorkRepository.Create(item);
		}

		public void UpdateItem(ScientificWork item)
		{
			_scientificWorkRepository.Update(item);
		}

		public void DeleteById(Guid id)
		{
			_scientificWorkRepository.Delete(id);
		}

		public bool Any(Func<ScientificWork, bool> predicate)
		{
			return _scientificWorkRepository.AllWhere(predicate).Any();
		}

		public bool Exists(Guid id)
		{
			return Any(r => r.Id == id);
		}

		public IEnumerable<UserProfile> GetAuthors(Guid id)
		{
			var scientificWork = _scientificWorkRepository.Get(id);
			IEnumerable<UserProfile> authors = null;
			if (scientificWork != null)
			{
				authors = scientificWork.UserProfilesScientificWorks.Select(u => u.UserProfile);
			}

			return authors;
		}

		public void AddAuthor(Guid id, Guid authorId)
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

		public void RemoveAuthor(Guid id, Guid authorId)
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
