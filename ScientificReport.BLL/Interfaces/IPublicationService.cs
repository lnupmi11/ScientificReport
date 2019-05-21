using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.BLL.Interfaces
{
	public interface IPublicationService
	{
		IEnumerable<Publication> GetAll();
		IEnumerable<Publication> GetAllWhere(Func<Publication, bool> predicate);
		IEnumerable<Publication> GetPage(int page, int count);
		int GetCount();
		Publication GetById(Guid id);
		Publication Get(Func<Publication, bool> predicate);
		void CreateItem(Publication item);
		void UpdateItem(Publication item);
		void DeleteById(Guid id);
		bool PublicationExists(Guid id);
		ICollection<UserProfile> GetPublicationAuthors(Guid id);
		void AddAuthor(Publication publication, UserProfile user);
		IEnumerable<Publication> GetUserPublications(UserProfile user);
		IEnumerable<Publication> SortPublicationsBy(Publication.SortByOptions option, int page, int count);
	}
}
