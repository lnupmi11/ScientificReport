using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models;
using ScientificReport.DTO.Models.ReportThesis;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class ReportThesisController : Controller
	{
		private readonly IReportThesisService _reportThesisService;
		private readonly IUserProfileService _userProfileService;
		private readonly IDepartmentService _departmentService;
		private readonly IConferenceService _conferenceService;

		public ReportThesisController(
			IReportThesisService reportThesisService,
			IUserProfileService userProfileService,
			IDepartmentService departmentService,
			IConferenceService conferenceService
		)
		{
			_reportThesisService = reportThesisService;
			_userProfileService = userProfileService;
			_departmentService = departmentService;
			_conferenceService = conferenceService;
		}

		// GET: ReportThesis
		public IActionResult Index(ReportThesisIndexModel model)
		{
			model.ReportTheses = _reportThesisService.GetPageByRole(model.CurrentPage, model.PageSize, User);
			model.Count = _reportThesisService.GetCountByRole(User);
			return View(model);
		}

		// GET: ReportThesis/Details/{id}
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var reportThesis = _reportThesisService.GetById(id.Value);

			if (reportThesis == null)
			{
				return NotFound();
			}

			if (!UserHasPermission(reportThesis))
			{
				return Forbid();
			}

			var reportThesisDetails = new ReportThesisDetails
			{
				ReportThesis = reportThesis,
				Authors = _reportThesisService.GetAuthors(reportThesis.Id).ToList()
			};

			return View(reportThesisDetails);
		}

		// GET: ReportThesis/Create
		public IActionResult Create()
		{
			return View(new ReportThesisModel
			{
				Conferences = _conferenceService.GetAll()
			});
		}

		// POST: ReportThesis/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ReportThesisModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Conferences = _conferenceService.GetAll();
				return View(model);
			}

			model.Conference = _conferenceService.GetById(model.ConferenceId);
			_reportThesisService.CreateItem(model);
			_reportThesisService.AddAuthor(_reportThesisService.Get(r => r.Thesis == model.Thesis).Id,
				_userProfileService.Get(User).Id);
			return RedirectToAction(nameof(Index));
		}

		// GET: ReportThesis/Edit/{id}
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var reportThesis = _reportThesisService.GetById(id.Value);
			if (reportThesis == null)
			{
				return NotFound();
			}

			if (!UserHasPermission(reportThesis))
			{
				return Forbid();
			}

			return View(new ReportThesisEdit(reportThesis)
			{
				Authors = _reportThesisService.GetAuthors(reportThesis.Id),
				Users = _userProfileService.GetAll()
			});
		}

		// POST: ReportThesis/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid? id, ReportThesisEdit model)
		{
			var reportThesis = _reportThesisService.GetById(model.Id);
			if (id != reportThesis.Id)
			{
				return NotFound();
			}
			
			if (!UserHasPermission(reportThesis))
			{
				return Forbid();
			}

			if (!ModelState.IsValid)
			{
				model.Authors = _reportThesisService.GetAuthors(reportThesis.Id);
				model.Users = _userProfileService.GetAll();
				return View(model);
			}

			model.Conference = _conferenceService.GetById(model.ConferenceId);
			_reportThesisService.UpdateItem(model);

			return RedirectToAction(nameof(Index));
		}

		// GET: ReportThesis/Delete/{id}
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var reportThesis = _reportThesisService.GetById(id.Value);
			if (reportThesis == null)
			{
				return NotFound();
			}
			
			if (!UserHasPermission(reportThesis))
			{
				return Forbid();
			}

			return View(reportThesis);
		}

		// POST: ReportThesis/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			if (!_reportThesisService.Exists(id))
			{
				return NotFound();
			}
			
			if (!UserHasPermission(_reportThesisService.GetById(id)))
			{
				return Forbid();
			}
			
			_reportThesisService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}

		// POST: ReportThesis/AddAuthor/{id}
		[HttpPost]
		public IActionResult AddAuthor(Guid id, [FromBody] UpdateUserRequest request)
		{
			if (!_reportThesisService.Exists(id))
			{
				return NotFound();
			}
			
			if (!UserHasPermission(_reportThesisService.GetById(id)))
			{
				return Forbid();
			}
			
			_reportThesisService.AddAuthor(id, request.UserId);
			return Json(ApiResponse.Ok);
		}

		// POST: ReportThesis/DeleteAuthor/{id}
		[HttpPost]
		public IActionResult DeleteAuthor(Guid id, [FromBody] UpdateUserRequest request)
		{
			if (!_reportThesisService.Exists(id))
			{
				return NotFound();
			}
			
			if (!UserHasPermission(_reportThesisService.GetById(id)))
			{
				return Forbid();
			}
			
			_reportThesisService.RemoveAuthor(id, request.UserId);
			return Json(ApiResponse.Ok);
		}
		
		private bool UserHasPermission(ReportThesis reportThesis)
		{
			var user = _userProfileService.Get(User);
			var department = _departmentService.Get(d => d.Staff.Contains(user));
			return PageHelpers.IsAdmin(User) ||
			       PageHelpers.IsHeadOfDepartment(User) &&
			       _reportThesisService.GetAuthors(reportThesis.Id).Any(p => department.Staff.Contains(p)) ||
			       _reportThesisService.GetAuthors(reportThesis.Id).Contains(user);
		}
	}
}
