using System;
using System.Collections.Generic;
using System.Security.Claims;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DTO.Models.Publication;

namespace ScientificReport.BLL.Interfaces
{
	public interface IPublicationService
	{
		IEnumerable<Publication> GetAll();
		IEnumerable<Publication> GetAllWhere(Func<Publication, bool> predicate);

		IEnumerable<Publication> Filter(PublicationIndexModel model, ClaimsPrincipal userPrincipal, bool userIsAdmin,
			bool userIsHead);
		Publication GetById(Guid id);
		Publication Get(Func<Publication, bool> predicate);
		void CreateItem(Publication item);
		void UpdateItem(Publication item);
		void DeleteById(Guid id);
		bool PublicationExists(Guid id);
		ICollection<UserProfile> GetPublicationAuthors(Guid id);
		void AddAuthor(Publication publication, UserProfile user);
		void RemoveAuthor(Publication publication, UserProfile user);
		IEnumerable<Publication> GetUserPublications(UserProfile user);
		IEnumerable<Publication> SortPublicationsBy(Publication.SortByOptions option, int page, int count);
	}
}
