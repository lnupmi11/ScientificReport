using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.ReportThesis;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class ReportThesisController : Controller
	{
		private readonly IReportThesisService _reportThesisService;
		private readonly IUserProfileService _userProfileService;

		public ReportThesisController(IReportThesisService reportThesisService, IUserProfileService userProfileService)
		{
			_reportThesisService = reportThesisService;
			_userProfileService = userProfileService;
		}

		// GET: ReportThesis
		public IActionResult Index(ReportThesisIndexModel model)
		{
			model.ReportTheses = _reportThesisService.GetPage(model.CurrentPage, model.PageSize);
			model.Count = _reportThesisService.GetCount();
			return View(model);
		}

		// GET: ReportThesis/Details/5
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
			return View();
		}

		// POST: ReportThesis/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,Thesis")] ReportThesis reportThesis)
		{
			if (!ModelState.IsValid) return View(reportThesis);

			_reportThesisService.CreateItem(reportThesis);
			return RedirectToAction(nameof(Index));
		}

		// GET: ReportThesis/Edit/5
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

			var reportThesisEdt = new ReportThesisEdit
			{
				ReportThesis = reportThesis,
				Authors = _reportThesisService.GetAuthors(reportThesis.Id),
				Users = _userProfileService.GetAll()
			};
			return View(reportThesisEdt);
		}

		// POST: ReportThesis/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid? id, ReportThesisEdit reportThesisEdit)
		{
			var reportThesis = reportThesisEdit.ReportThesis;
			if (id != reportThesis.Id)
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				reportThesisEdit.Authors = _reportThesisService.GetAuthors(reportThesis.Id);
				reportThesisEdit.Users = _userProfileService.GetAll();
				return View(reportThesisEdit);
			}

			try
			{
				_reportThesisService.UpdateItem(reportThesis);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_reportThesisService.Exists(reportThesis.Id))
				{
					return NotFound();
				}

				throw;
			}

			return RedirectToAction(nameof(Index));
		}

		// GET: ReportThesis/Delete/5
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

			return View(reportThesis);
		}

		// POST: ReportThesis/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			_reportThesisService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}

		private bool ReportThesisExists(Guid id)
		{
			return _reportThesisService.GetAll().Any(e => e.Id == id);
		}

		// POST: ScientificWork/AddAuthor/5
		[HttpPost]
//		[ValidateAntiForgeryToken]
		public IActionResult AddAuthor(Guid id, [FromBody] ReportThesisAuthorRequest request)
		{
			_reportThesisService.AddAuthor(id, request.AuthorId);
			return Json(ApiResponse.Ok);
		}

		// POST: ScientificWork/DeleteAuthor/5
		[HttpPost]
//		[ValidateAntiForgeryToken]
		public IActionResult DeleteAuthor(Guid id, [FromBody] ReportThesisAuthorRequest request)
		{
			_reportThesisService.RemoveAuthor(id, request.AuthorId);
			return Json(ApiResponse.Ok);
		}
	}
}