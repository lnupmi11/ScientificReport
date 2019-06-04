using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
		private readonly IArticleService _articleService;
		private readonly IScientificWorkService _scientificWorkService;
		private readonly IConferenceService _conferenceService;
		private readonly IReportThesisService _reportThesisService;
		
		public PublicationController(
			IPublicationService publicationService,
			IUserProfileService userProfileService,
			IDepartmentService departmentService,
			IArticleService articleService,
			IScientificWorkService scientificWorkService,
			IConferenceService conferenceService,
			IReportThesisService reportThesisService
		)
		{
			_publicationService = publicationService;
			_userProfileService = userProfileService;
			_departmentService = departmentService;
			_articleService = articleService;
			_scientificWorkService = scientificWorkService;
			_conferenceService = conferenceService;
			_reportThesisService = reportThesisService;
		}

		// GET: /Publication
		public IActionResult Index(PublicationIndexModel model)
		{
			model.Publications = _publicationService.Filter(
				model, User, PageHelpers.IsAdmin(User), PageHelpers.IsHeadOfDepartment(User)
			);
			model.Articles = _articleService.GetPage(model.CurrentPage, model.PageSize);	
			model.Count = _publicationService.GetAll().Count() + _articleService.GetCount();
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
			return View(new PublicationCreateModel
			{
				ReportThesis =
				{
					Conferences = _conferenceService.GetAll()
				}
			});
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

			switch (model.PublicationType)
			{
				case PublicationCreateModel.PublicationTypes.Article:
					_articleService.CreateItem(model.ToArticle());
					_articleService.AddAuthor(
						_articleService.Get(a => a.Title == model.Article.Title),
						_userProfileService.Get(u => u.UserName == User.Identity.Name)
					);
					break;
				case PublicationCreateModel.PublicationTypes.ScientificWork:
					if (PageHelpers.IsHeadOfDepartment(User))
					{
						_scientificWorkService.CreateItem(model.ScientificWork);
					}

					break;
				case PublicationCreateModel.PublicationTypes.ReportThesis:
					model.ReportThesis.Conference = _conferenceService.GetById(model.ReportThesis.ConferenceId);
					_reportThesisService.CreateItem(model.ReportThesis);
					_reportThesisService.AddAuthor(
						_reportThesisService.Get(r => r.Thesis == model.ReportThesis.Thesis).Id,
						_userProfileService.Get(User).Id);
					break;
				case PublicationCreateModel.PublicationTypes.Other:
					var newPublication = model.ToPublication();
					_publicationService.CreateItem(newPublication);
					_publicationService.AddAuthor(
						_publicationService.Get(p => p.Title == newPublication.Title && p.PublishingYear == newPublication.PublishingYear && p.Specification == newPublication.Specification),
						_userProfileService.Get(User)
					);
					break;
				default:
					return BadRequest();
			}

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
				publication.PublicationType = model.Type;
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

		public IActionResult SearchPublications(string substring, Publication.PublicationTypes? type)
		{
			if (substring == null || type == null)
			{
				return Json(ApiResponse.Fail);
			}

			var publications = _publicationService.GetAllWhere(p => p.PublicationType == type.Value && p.Title.ToLower().Contains(substring.ToLower()));
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
