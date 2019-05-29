using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities.Reports;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.TeacherReport;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Teacher)]
	public class TeacherReportController : Controller
	{
		private readonly ITeacherReportService _teacherReportService;
		private readonly IUserProfileService _userProfileService;
		private readonly IPublicationService _publicationService;
		private readonly IArticleService _articleService;
		private readonly IScientificWorkService _scientificWorkService;
		private readonly IReportThesisService _reportThesisService;

		public TeacherReportController(ITeacherReportService teacherReportService,
			IUserProfileService userProfileService,
			IArticleService articleService,
			IScientificWorkService scientificWorkService,
			IReportThesisService reportThesisService,
			IPublicationService publicationService)
		{
			_teacherReportService = teacherReportService;
			_userProfileService = userProfileService;
			_publicationService = publicationService;
			_articleService = articleService;
			_scientificWorkService = scientificWorkService;
			_reportThesisService = reportThesisService;
		}

		// GET: Report
		public IActionResult Index()
		{
			return View(_teacherReportService.GetAll());
		}

		// GET: Report/Details/{id}
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var report = _teacherReportService.GetById(id.Value);
			if (report == null)
			{
				return NotFound();
			}

			return View(report);
		}

		// GET: Report/Print/{id}
		public IActionResult Print(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

//			ViewData["Message"] = "Your application description page.";
			var report = _teacherReportService.GetById(id.Value);

			var filename = $"{report.Teacher.UserName}-{report.Created}_report.pdf";

			// TODO: delete the next row, when the development is finished
			filename = null;

			return new ViewAsPdf(report, ViewData)
			{
				FileName = filename
			};
		}

		// GET: Report/Create
		public IActionResult Create()
		{
			var viewModel = new TeacherReportCreateViewModel
			{
				Users = _userProfileService.GetAll()
			};

			return View(viewModel);
		}

		// POST: Report/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Guid userId)
		{
			if (!ModelState.IsValid)
			{
				var viewModel = new TeacherReportCreateViewModel
				{
					UserId = _userProfileService.Get(u => u.UserName == HttpContext.User.Identity.Name).Id,
					Users = _userProfileService.GetAll()
				};

				ModelState.AddModelError("key", "Failed to create");
				return View(viewModel);
			}

			var report = new TeacherReport
			{
				Teacher = _userProfileService.GetById(userId)
			};

			_teacherReportService.CreateItem(report);
			return RedirectToAction(nameof(Index));
		}

		// GET: Report/Edit/{id}
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var report = _teacherReportService.GetById(id.Value);
			if (report == null)
			{
				return NotFound();
			}

			var user = _userProfileService.Get(u => u.UserName == HttpContext.User.Identity.Name);

			var data = new TeacherReportEditViewModel
			{
				TeacherReport = report,
				Users = _userProfileService.GetAll(),
				Publications = _publicationService.GetAll()
					.OrderByDescending(p => report.TeacherReportsPublications.Any(tp => tp.Publication.Id == p.Id))
					.ThenByDescending(p => p.UserProfilesPublications.Any(u => u.UserProfile.Id == user.Id))
					.ThenBy(p => p.Title),
				Articles = _articleService.GetAll(),
				ScientificWorks = _scientificWorkService.GetAll(),
				ReportTheses = _reportThesisService.GetAll()
			};
			return View(data);
		}

		// POST: Report/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, TeacherReportEditViewModel data)
		{
			if (id != data.TeacherReport.Id)
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				data.Publications = _publicationService.GetAll();
				data.Users = _userProfileService.GetAll();

				return View(data);
			}

			data.TeacherReport.Teacher = _userProfileService.GetById(data.TeacherReport.Teacher.Id);

			try
			{
				_teacherReportService.UpdateItem(data.TeacherReport);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_teacherReportService.Exists(data.TeacherReport.Id))
					return NotFound();
				Console.WriteLine();
				throw;
			}

			return RedirectToAction(nameof(Index));
		}

		// GET: Report/Delete/{id}
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var report = _teacherReportService.GetById(id.Value);
			if (report == null)
			{
				return NotFound();
			}

			return View(report);
		}

		// POST: TeacherReport/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			_teacherReportService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}

		// POST: TeacherReport/AddAuthor/5
		[HttpPost]
//		[ValidateAntiForgeryToken]
		public IActionResult AddPublication(Guid id, [FromBody] TeacherReportToggleEntityRequest request)
		{
			_teacherReportService.AddPublication(id, request.EntityId);
			return Json(ApiResponse.Ok);
		}

		// POST: TeacherReport/DeleteAuthor/5
		[HttpPost]
//		[ValidateAntiForgeryToken]
		public IActionResult DeletePublication(Guid id, [FromBody] TeacherReportToggleEntityRequest request)
		{
			_teacherReportService.RemovePublication(id, request.EntityId);
			return Json(ApiResponse.Ok);
		}

		[HttpPost]
		public IActionResult AddArticle(Guid id, [FromBody] TeacherReportToggleEntityRequest request)
		{
			_teacherReportService.AddArticle(id, request.EntityId);
			return Json(ApiResponse.Ok);
		}

		[HttpPost]
		public IActionResult DeleteArticle(Guid id, [FromBody] TeacherReportToggleEntityRequest request)
		{
			_teacherReportService.RemoveArticle(id, request.EntityId);
			return Json(ApiResponse.Ok);
		}

		[HttpPost]
		public IActionResult AddScientificWork(Guid id, [FromBody] TeacherReportToggleEntityRequest request)
		{
			_teacherReportService.AddScientificWork(id, request.EntityId);
			return Json(ApiResponse.Ok);
		}

		[HttpPost]
		public IActionResult DeleteScientificWork(Guid id, [FromBody] TeacherReportToggleEntityRequest request)
		{
			_teacherReportService.RemoveScientificWork(id, request.EntityId);
			return Json(ApiResponse.Ok);
		}

		[HttpPost]
		public IActionResult AddReportThesis(Guid id, [FromBody] TeacherReportToggleEntityRequest request)
		{
			_teacherReportService.AddReportThesis(id, request.EntityId);
			return Json(ApiResponse.Ok);
		}

		[HttpPost]
		public IActionResult DeleteReportThesis(Guid id, [FromBody] TeacherReportToggleEntityRequest request)
		{
			_teacherReportService.RemoveReportThesis(id, request.EntityId);
			return Json(ApiResponse.Ok);
		}
	}
}