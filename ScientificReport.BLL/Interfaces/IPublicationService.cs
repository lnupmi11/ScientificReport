using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;

namespace ScientificReport.BLL.Interfaces
{
	public interface IPublicationService
	{
		IEnumerable<Publication> GetAll();
		IEnumerable<Publication> GetAllWhere(Func<Publication, bool> predicate);
		Publication GetById(Guid id);
		Publication Get(Func<Publication, bool> predicate);
		void CreateItem(Publication item);
		void UpdateItem(Publication item);
		void DeleteById(Guid id);
		bool PublicationExists(Guid id);
		ICollection<UserProfile> GetPublicationAuthors(Guid id);
	}
}
