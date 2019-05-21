using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
		public IActionResult Index(PublicationIndexModel model)
		{
			var publications = _publicationService.GetPage(model.CurrentPage, model.PageSize);
			if (model.SortBy != null)
			{
				publications = _publicationService.SortPublicationsBy(model.SortBy.Value, model.CurrentPage, model.PageSize);
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

				var user = _userProfileService.Get(User);
			
				if (model.PublicationSetType == null)
				{
					if (PageHelpers.IsAdmin(User))
					{
						model.PublicationSetType = Publication.PublicationSetType.Faculty;	
					}
					else if (PageHelpers.IsHeadOfDepartment(User))
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
						var department = _departmentService.Get(u => u.Staff.Contains(user));
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

			model.Publications = publications;
			model.Count = _publicationService.GetCount();
			return View(model);
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

			var model = new PublicationDetailsModel
			{
				Publication = publication, Authors = _publicationService.GetPublicationAuthors(id.Value)
			};
			return View(model);
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

			var editModel = new PublicationEditModel(publication)
			{
				Authors = _publicationService.GetPublicationAuthors(publication.Id),
				Users = _userProfileService.GetAll()
			};

			return View(editModel);
		}

		// POST: /Publication/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, PublicationEditModel model)
		{
			if (id != model.Id)
			{
				return NotFound();
			}
			
			if (!_publicationService.PublicationExists(id))
			{
				return NotFound();
			}

			var publication = _publicationService.GetById(id);
			if (ModelState.IsValid)
			{
				publication.Type = model.Type;
				publication.Title = model.Title;
				publication.Specification = model.Specification;
				publication.PublishingPlace = model.PublishingPlace;
				publication.PublishingHouseName = model.PublishingHouseName;
				publication.PublishingYear = model.PublishingYear;
				publication.PagesAmount = model.PagesAmount;
				publication.PrintStatus = model.PrintStatus;
				_publicationService.UpdateItem(publication);
				return RedirectToAction("Index");
			}
			
			var editModel = new PublicationEditModel(publication)
			{
				Authors = _publicationService.GetPublicationAuthors(publication.Id),
				Users = _userProfileService.GetAll()
			};
			return View(editModel);
		}

		// POST: Department/AddUserToStaff/{departmentId}
		[HttpPost]
		[Authorize(Roles = UserProfileRole.HeadOfDepartmentOrAdmin)]
		public IActionResult AddUserToAuthors(Guid? id, [FromBody] PublicationUpdateAuthorsRequest request)
		{
			if (id == null)
			{
				return NotFound();
			}
			
			var user = _userProfileService.GetById(request.UserId);
			if (user == null)
			{
				return Json(ApiResponse.Fail);
			}
			
			var department = _departmentService.Get(d => d.Head.Id == user.Id);
			if (department == null)
			{
				return Json(ApiResponse.Fail);
			}
			
			if (!IsValidCurrentUser(department))
			{
				return Json(ApiResponse.Fail);
			}

			var publication = _publicationService.GetById(id.Value);
			if (publication == null)
			{
				return NotFound();
			}
			
			if (!_publicationService.GetPublicationAuthors(publication.Id).Contains(user))
			{
				_publicationService.AddAuthor(publication, user);
			}

			return Json(ApiResponse.Ok);
		}

		// POST: Department/RemoveUserFromStaff/{departmentId}
		[HttpPost]
		[Authorize(Roles = UserProfileRole.HeadOfDepartmentOrAdmin)]
		public IActionResult RemoveUserFromAuthors(Guid? id, [FromBody] PublicationUpdateAuthorsRequest request)
		{
			if (id == null)
			{
				return NotFound();
			}
			
			var user = _userProfileService.GetById(request.UserId);
			if (user == null)
			{
				return Json(ApiResponse.Fail);
			}
			
			var department = _departmentService.Get(d => d.Head.Id == user.Id);
			if (department == null)
			{
				return Json(ApiResponse.Fail);
			}
			
			if (!IsValidCurrentUser(department))
			{
				return Json(ApiResponse.Fail);
			}

			var publication = _publicationService.GetById(id.Value);
			if (publication == null)
			{
				return NotFound();
			}
			
			if (_publicationService.GetPublicationAuthors(publication.Id).Contains(user))
			{
				_publicationService.RemoveAuthor(publication, user);
			}

			return Json(ApiResponse.Ok);
		}
		
		// GET: Publication/Delete/{id}
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

		// POST: Publication/Delete/{id}
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
		
		private bool IsValidCurrentUser(Department department)
		{
			var currentUser = _userProfileService.Get(User);
			if (!PageHelpers.IsAdmin(User))
			{
				return currentUser.Id == department.Head.Id;	
			}

			return true;
		}
	}
}
