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
	public class PublicationService : IPublicationService
	{
		private readonly PublicationRepository _publicationRepository;
		
		public PublicationService(ScientificReportDbContext context)
		{
			_publicationRepository = new PublicationRepository(context);
		}
		
		public virtual IEnumerable<Publication> GetAll()
		{
			return _publicationRepository.All();
		}

		public virtual IEnumerable<Publication> GetAllWhere(Func<Publication, bool> predicate)
		{
			return _publicationRepository.AllWhere(predicate);
		}

		public virtual Publication GetById(Guid id)
		{
			return _publicationRepository.Get(id);
		}

		public virtual Publication Get(Func<Publication, bool> predicate)
		{
			return _publicationRepository.Get(predicate);
		}

		public virtual void CreateItem(Publication item)
		{
			_publicationRepository.Create(item);
		}

		public virtual void UpdateItem(Publication item)
		{
			_publicationRepository.Update(item);
		}

		public virtual void DeleteById(Guid id)
		{
			_publicationRepository.Delete(id);
		}

		public virtual bool PublicationExists(Guid id)
		{
			return _publicationRepository.Get(id) != null;
		}

		public virtual ICollection<UserProfile> GetPublicationAuthors(Guid id)
		{
			var publication = _publicationRepository.Get(id);
			ICollection<UserProfile> result = null;
			if (publication != null)
			{
				result = publication.UserProfilesPublications.Select(item => item.UserProfile).ToList();
			}

			return result;
		}

		public virtual void AddAuthor(Publication publication, UserProfile user)
		{
			publication.UserProfilesPublications.Add(new UserProfilesPublications
			{
				Publication = publication,
				UserProfile = user,
				PublicationId = publication.Id,
				UserProfileId = user.Id
			});
			_publicationRepository.Update(publication);
		}

		public virtual IEnumerable<Publication> GetUserPublications(UserProfile user)
		{
			var allPublications = _publicationRepository.All().ToList();

			return allPublications.Where(t => t.UserProfilesPublications.Any(u => u.UserProfile.Id == user.Id)).ToList();
		}

		public virtual IEnumerable<Publication> SortPublicationsBy(Publication.SortByOptions option)
		{
			var publications = GetAll();
			switch (option)
			{
				case Publication.SortByOptions.Type:
					publications = publications.OrderBy(p => p.Type);
					break;
				case Publication.SortByOptions.Title:
					publications = publications.OrderBy(p => p.Title);
					break;
				case Publication.SortByOptions.PublishingHouse:
					publications = publications.OrderBy(p => p.PublishingHouseName);
					break;
				case Publication.SortByOptions.PublishingYear:
					publications = publications.OrderByDescending(p => p.PublishingYear);
					break;
				default:
					return new List<Publication>();
			}

			return publications;
		}
	}
}
