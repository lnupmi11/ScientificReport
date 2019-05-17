using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.Publication;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class PublicationController : Controller
	{
		private readonly IPublicationService _publicationService;
		private readonly IUserProfileService _userProfileService;
		private readonly IDepartmentService _departmentService;
		private readonly IStringLocalizer<PublicationController> _localizer; 
		
		public PublicationController(
			IPublicationService publicationService,
			IUserProfileService userProfileService,
			IDepartmentService departmentService,
			IStringLocalizer<PublicationController> localizer
		)
		{
			_publicationService = publicationService;
			_userProfileService = userProfileService;
			_departmentService = departmentService;
			_localizer = localizer;
		}

		// GET: /Publication
		public IActionResult Index(PublicationIndexModel filters)
		{
			IEnumerable<Publication> publications;
			if (filters.SortBy != null)
			{
				publications = _publicationService.SortPublicationsBy(filters.SortBy.Value);
			}
			else
			{
				var yearFrom = -1;
				if (filters.YearFromFilter != null)
				{
					yearFrom = filters.YearFromFilter.Value;
				}
			
				var yearTo = -1;
				if (filters.YearToFilter != null)
				{
					yearTo = filters.YearToFilter.Value;
				}

				var user = _userProfileService.Get(User);
			
				if (filters.PublicationSetType == null)
				{
					if (PageHelpers.IsAdmin(User))
					{
						filters.PublicationSetType = Publication.PublicationSetType.Faculty;	
					}
					else if (PageHelpers.IsHeadOfDepartment(User))
					{
						filters.PublicationSetType = Publication.PublicationSetType.Department;	
					}
					else
					{
						filters.PublicationSetType = Publication.PublicationSetType.Personal;	
					}
				}
				
				switch (filters.PublicationSetType.Value)
				{
					case Publication.PublicationSetType.Department:
						var department = _departmentService.Get(u => u.Staff.Contains(user));
						if (department != null)
						{
							publications = _publicationService.GetAllWhere(p =>
								p.UserProfilesPublications.Any(up => department.Staff.Contains(up.UserProfile)));	
						}
						else
						{
							publications = _publicationService.GetAllWhere(p =>
								p.UserProfilesPublications.Any(upp => upp.UserProfile.Id == user.Id));
						}
						break;
					case Publication.PublicationSetType.Faculty:
						publications = _publicationService.GetAll();
						break;
					default:
						publications = _publicationService.GetAllWhere(p =>
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

				if (filters.PrintStatus != null && filters.PrintStatus != Publication.PrintStatuses.Any)
				{
					publications = publications.Where(p => p.PrintStatus == filters.PrintStatus.Value);
				}
			}

			return View(new PublicationIndexModel(publications));
		}

		// GET: /Publication/Details/{id}
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var publication = _publicationService.GetById(id.Value);
			if (publication == null)
			{
				return NotFound();
			}

			return View(publication);
		}

		// GET: /Publication/Create
		public IActionResult Create()
		{
			return View(new PublicationCreateModel());
		}

		// POST: /Publication/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		// TODO: add search for publication which match the string that user entered in 'Title' field
		public IActionResult Create(PublicationCreateModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (model.PublishingYear < 1900)
			{
				ModelState.AddModelError("", _localizer["InvalidPublishingYear"]);
				return View(model);
			}

			if (model.PagesAmount <= 0)
			{
				ModelState.AddModelError("", _localizer["InvalidPagesAmount"]);
				return View(model); 
			}

			var newPublication = model.ToPublication();
			_publicationService.CreateItem(newPublication);
			_publicationService.AddAuthor(
				_publicationService.Get(p => p.Title == newPublication.Title && p.PublishingYear == newPublication.PublishingYear && p.Specification == newPublication.Specification),
				_userProfileService.Get(User)
			);
			
			return RedirectToAction(nameof(Index));
		}

		// GET: /Publication/Edit/{id}
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var publication = _publicationService.GetById(id.Value);
			if (publication == null)
			{
				return NotFound();
			}

			var user = _userProfileService.Get(User);
			var department = _departmentService.Get(d => d.Staff.Contains(user));
			var isHeadOfDepartment = publication.UserProfilesPublications.Any(p => department.Staff.Contains(p.UserProfile));
			if (!(PageHelpers.IsAdmin(User) || isHeadOfDepartment ||
			    publication.UserProfilesPublications.Any(p => p.UserProfile.UserName == User.Identity.Name) &&
			    publication.PublishingYear == DateTime.Now.Year))
			{
				return Forbid();
			}

			return View(new PublicationCreateModel(publication));
		}

		// POST: /Publication/Edit/{id}
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		// TODO: fix this action
		public IActionResult Edit(Guid id, [Bind("Id,Type,Title,Specification,PublishingPlace,PublishingHouseName,PublishingYear,PagesAmount,IsPrintCanceled,IsRecommendedToPrint,CreatedAt,LastEditAt")] Publication publication)
		{
			if (id != publication.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
			//		_context.Update(publication);
			//		await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (_publicationService.PublicationExists(publication.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(new PublicationCreateModel(publication));
		}

		// GET: Publication/Delete/5
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var publication = _publicationService.GetById(id.Value);
			if (publication == null)
			{
				return NotFound();
			}
			
			if (!PageHelpers.IsAdmin(User) || publication.PublishingYear != DateTime.Now.Year)
			{
				return Forbid();
			}

			return View(publication);
		}

		// POST: Publication/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			var publication = _publicationService.GetById(id);
			if (publication == null)
			{
				return NotFound();
			}
			
			if (!PageHelpers.IsAdmin(User) || publication.PublishingYear != DateTime.Now.Year)
			{
				return Forbid();
			}
			
			_publicationService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
