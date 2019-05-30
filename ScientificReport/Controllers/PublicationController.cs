using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models;
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
			model.Publications = _publicationService.Filter(
				model, User, PageHelpers.IsAdmin(User), PageHelpers.IsHeadOfDepartment(User)
			);
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

			var authors = _publicationService.GetPublicationAuthors(id.Value);
			var model = new PublicationDetailsModel
			{
				Publication = publication,
				Authors = authors,
				UserIsAuthor = authors.Contains(_userProfileService.Get(User))
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
				ModelState.AddModelError("", _localizer["Publishing year is incorrect"]);
				return View(model);
			}

			if (model.PagesAmount <= 0)
			{
				ModelState.AddModelError("", _localizer["Pages amount must be greater than 0"]);
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

			if (!AllowUserToEditPublication(publication))
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

		// POST: Publication/AddSelfToAuthors/{id}
		[HttpPost]
		public IActionResult AddSelfToAuthors(Guid? id)
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
			if (!_publicationService.GetPublicationAuthors(publication.Id).Contains(user))
			{
				_publicationService.AddAuthor(publication, user);
			}

			return RedirectToAction("Details", new { id = id.Value });
		}

		// POST: Publication/AddUserToAuthors/{publicationId}
		[HttpPost]
		[Authorize(Roles = UserProfileRole.HeadOfDepartmentOrAdmin)]
		public IActionResult AddUserToAuthors(Guid? id, [FromBody] UpdateUserRequest request)
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
			
			var publication = _publicationService.GetById(id.Value);
			if (publication == null)
			{
				return NotFound();
			}
			
			if (!AllowUserToEditPublication(publication))
			{
				return Json(ApiResponse.Fail);
			}
			
			if (!_publicationService.GetPublicationAuthors(publication.Id).Contains(user))
			{
				_publicationService.AddAuthor(publication, user);
			}

			return Json(ApiResponse.Ok);
		}

		// POST: Publication/RemoveUserFromAuthors/{publicationId}
		[HttpPost]
		[Authorize(Roles = UserProfileRole.HeadOfDepartmentOrAdmin)]
		public IActionResult RemoveUserFromAuthors(Guid? id, [FromBody] UpdateUserRequest request)
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
			
			var publication = _publicationService.GetById(id.Value);
			if (publication == null)
			{
				return NotFound();
			}
			
			if (!AllowUserToEditPublication(publication))
			{
				return Json(ApiResponse.Fail);
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
			
			if (!PageHelpers.IsAdmin(User))
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
			
			if (!PageHelpers.IsAdmin(User))
			{
				return Forbid();
			}
			
			_publicationService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}

		public IActionResult SearchPublications(string substring, Publication.Types? type)
		{
			if (substring == null || type == null)
			{
				return Json(ApiResponse.Fail);
			}

			var publications = _publicationService.GetAllWhere(p => p.Type == type.Value && p.Title.ToLower().Contains(substring.ToLower()));
			return Json(new PublicationSearchApiResponse
			{
				Publications = publications.Select(p => new PublicationApiResponse
				{
					Id = p.Id,
					Title = p.Title
				}),
				Success = true
			});
		}

		
		private bool AllowUserToEditPublication(Publication publication)
		{
			var user = _userProfileService.Get(User);
			var department = _departmentService.Get(d => d.Staff.Contains(user));
			var isHeadOfDepartment = PageHelpers.IsHeadOfDepartment(User) && publication.UserProfilesPublications.Any(p => department.Staff.Contains(p.UserProfile));
			return PageHelpers.IsAdmin(User) || isHeadOfDepartment ||
			       publication.UserProfilesPublications.Any(p => p.UserProfile.UserName == User.Identity.Name);
		}
	}
}
