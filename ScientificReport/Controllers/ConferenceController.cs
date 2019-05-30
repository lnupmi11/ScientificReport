using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.Conference;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class ConferenceController : Controller
	{
		private readonly IConferenceService _conferenceService;
		private readonly IDepartmentService _departmentService;
		private readonly IUserProfileService _userProfileService;

		public ConferenceController(
			IConferenceService conferenceService,
			IDepartmentService departmentService,
			IUserProfileService userProfileService
		)
		{
			_conferenceService = conferenceService;
			_departmentService = departmentService;
			_userProfileService = userProfileService;
		}

		// GET: Conference
		public IActionResult Index(ConferenceIndexModel model)
		{
			model.Conferences = _conferenceService.GetPageByRole(model.CurrentPage, model.PageSize, User);
			model.Count = _conferenceService.GetCountByRole(User);
			return View(model);
		}

		// GET: Conference/Details/{id}
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var conference = _conferenceService.GetById(id.Value);
			
			if (conference == null)
			{
				return NotFound();
			}

			if (!UserHasPermission(conference))
			{
				return Forbid();
			}

			var conferenceDetails = new ConferenceDetails
			{
				Conference = conference
			};

			return View(conferenceDetails);
		}

		// GET: Conference/Create
		public IActionResult Create() => View();

		// POST: Conference/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,Topic,Date")] Conference conference)
		{
			if (!ModelState.IsValid)
			{
				return View(conference);
			}

			_conferenceService.CreateItem(conference);
			return RedirectToAction(nameof(Index));
		}

		// GET: Conference/Edit/{id}
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var conference = _conferenceService.GetById(id.Value);
			if (conference == null)
			{
				return NotFound();
			}
			
			if (!UserHasPermission(conference))
			{
				return Forbid();
			}

			var conferenceEdit = new ConferenceEdit
			{
				Conference = conference
			};

			return View(conferenceEdit);
		}

		// POST: Conference/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, ConferenceEdit conferenceEdit)
		{
			var conference = conferenceEdit.Conference;
			if (id != conference.Id)
			{
				return NotFound();
			}

			if (!UserHasPermission(conference))
			{
				return Forbid();
			}
			
			if (!ModelState.IsValid)
			{
				return View(conferenceEdit);
			}
			try
			{
				_conferenceService.UpdateItem(conference);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_conferenceService.Exists(conference.Id))
				{
					return NotFound();
				}
				throw;
			}

			return RedirectToAction(nameof(Index));
		}

		// GET: Conference/Delete/{id}
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var conference = _conferenceService.GetById(id.Value);
			if (conference == null)
			{
				return NotFound();
			}
			
			if (!UserHasPermission(conference))
			{
				return Forbid();
			}

			return View(conference);
		}

		// POST: Conference/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			if (!UserHasPermission(_conferenceService.GetById(id)))
			{
				return Forbid();
			}
			
			_conferenceService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
		
		private bool UserHasPermission(Conference conference)
		{
			var user = _userProfileService.Get(User);
			var department = _departmentService.Get(d => d.Staff.Contains(user));
			return PageHelpers.IsAdmin(User) ||
				   PageHelpers.IsHeadOfDepartment(User) &&
				   _conferenceService.GetParticipators(conference.Id).Any(p => department.Staff.Contains(p)) ||
				   _conferenceService.GetParticipators(conference.Id).Contains(user);
		}
	}
}
