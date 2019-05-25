using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Repositories;
using ScientificReport.DTO.Models.Publication;

namespace ScientificReport.BLL.Services
{
	public class PublicationService : IPublicationService
	{
		private readonly PublicationRepository _publicationRepository;
		private readonly UserProfileRepository _userProfileRepository;
		private readonly DepartmentRepository _departmentRepository;
		
		public PublicationService(ScientificReportDbContext context)
		{
			_publicationRepository = new PublicationRepository(context);
			_userProfileRepository = new UserProfileRepository(context);
			_departmentRepository = new DepartmentRepository(context);
		}
		
		public virtual IEnumerable<Publication> GetAll()
		{
			return _publicationRepository.All();
		}

		public virtual IEnumerable<Publication> GetAllWhere(Func<Publication, bool> predicate)
		{
			return _publicationRepository.AllWhere(predicate);
		}
		
		public virtual IEnumerable<Publication> Filter(PublicationIndexModel model, ClaimsPrincipal userPrincipal, bool userIsAdmin, bool userIsHead)
		{
			var publications = GetPage(model.CurrentPage, model.PageSize);
			if (model.SortBy != null)
			{
				publications = SortPublicationsBy(model.SortBy.Value, model.CurrentPage, model.PageSize);
			}
			else
			{
				var yearFrom = -1;
				if (model.YearFromFilter != null)
				{
					yearFrom = model.YearFromFilter.Value;
				}
			
				var yearTo = -1;
				if (model.YearToFilter != null)
				{
					yearTo = model.YearToFilter.Value;
				}

				var user = _userProfileRepository.Get(u => u.UserName == userPrincipal.Identity.Name);
			
				if (model.PublicationSetType == null)
				{
					if (userIsAdmin)
					{
						model.PublicationSetType = Publication.PublicationSetType.Faculty;	
					}
					else if (userIsHead)
					{
						model.PublicationSetType = Publication.PublicationSetType.Department;	
					}
					else
					{
						model.PublicationSetType = Publication.PublicationSetType.Personal;	
					}
				}
				
				switch (model.PublicationSetType.Value)
				{
					case Publication.PublicationSetType.Department:
						var department = _departmentRepository.Get(u => u.Staff.Contains(user));
						if (department != null)
						{
							publications = publications.Where(p =>
								p.UserProfilesPublications.Any(up => department.Staff.Contains(up.UserProfile)));	
						}
						else
						{
							publications = publications.Where(p =>
								p.UserProfilesPublications.Any(upp => upp.UserProfile.Id == user.Id));
						}
						break;
					case Publication.PublicationSetType.Faculty:
						break;
					default:
						publications = publications.Where(p =>
							p.UserProfilesPublications.Any(upp => upp.UserProfile.Id == user.Id));
						break;
				}

				if (yearFrom != -1)
				{	
					publications = publications.Where(p => p.PublishingYear >= yearFrom);
				}
			
				if (yearTo != -1)
				{
					publications = publications.Where(p => p.PublishingYear <= yearTo);
				}

				if (model.PrintStatus != null && model.PrintStatus != Publication.PrintStatuses.Any)
				{
					publications = publications.Where(p => p.PrintStatus == model.PrintStatus.Value);
				}
			}

			return publications;
		}
		
		public virtual IEnumerable<Publication> GetPage(int page, int count)
		{
			return _publicationRepository.All().Skip((page - 1) * count).Take(count).ToList();
		}
		
		public virtual int GetCount()
		{
			return _publicationRepository.All().Count();
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
		
		public virtual void RemoveAuthor(Publication publication, UserProfile user)
		{
			publication.UserProfilesPublications.Remove(publication.UserProfilesPublications.First(up => up.UserProfile.Id == user.Id));
			_publicationRepository.Update(publication);
		}

		public virtual IEnumerable<Publication> GetUserPublications(UserProfile user)
		{
			var allPublications = _publicationRepository.All().ToList();

			return allPublications.Where(t => t.UserProfilesPublications.Any(u => u.UserProfile.Id == user.Id)).ToList();
		}

		public virtual IEnumerable<Publication> SortPublicationsBy(Publication.SortByOptions option, int page, int count)
		{
			var publications = GetPage(page, count);
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
