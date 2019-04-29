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

		public virtual void CreateItem(UserProfile user, Publication item)
		{
			item.CreatedBy = user;
			item.LastEditBy = user;
			item.CreatedAt = DateTime.Now;
			item.LastEditAt = DateTime.Now;
			_publicationRepository.Create(item);
		}

		public virtual void UpdateItem(UserProfile user, Publication item)
		{
			item.LastEditBy = user;
			item.LastEditAt = DateTime.Now;
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
	}
}
