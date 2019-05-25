using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.ScientificWorks;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Teacher)]
	public class ScientificWorkController : Controller
	{
		private readonly IScientificWorkService _scientificWorkService;
		private readonly IUserProfileService _userProfileService;

		public ScientificWorkController(
			IScientificWorkService scientificWorkService,
			IUserProfileService userProfileService
		)
		{
			_scientificWorkService = scientificWorkService;
			_userProfileService = userProfileService;
		}

		// GET: ScientificWork
		public IActionResult Index(ScientificWorkIndexModel model)
		{
			model.ScientificWorks = _scientificWorkService.GetPage(model.CurrentPage, model.PageSize);
			model.Count = _scientificWorkService.GetCount();
			return View(model);
		}

		// GET: ScientificWork/Details/{id}
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var scientificWork = _scientificWorkService.GetById(id.Value);
			if (scientificWork == null)
			{
				return NotFound();
			}

			var scientificWorksDetails = new ScientificWorksDetails
			{
				ScientificWork = scientificWork,
				Authors = _scientificWorkService.GetAuthors(scientificWork.Id).ToList()
			};

			return View(scientificWorksDetails);
		}

		// GET: ScientificWork/Create
		public IActionResult Create() => View();

		// POST: ScientificWork/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,Cypher,Category,Title,Contents")]
			ScientificWork scientificWork)
		{
			if (!ModelState.IsValid)
			{
				return View(scientificWork);
			}

			_scientificWorkService.CreateItem(scientificWork);
			return RedirectToAction(nameof(Index));
		}

		// GET: ScientificWork/Edit/{id}
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var scientificWork = _scientificWorkService.GetById(id.Value);
			if (scientificWork == null)
			{
				return NotFound();
			}
			
			var scientificWorksEdit = new ScientificWorksEdit
			{
				ScientificWork = scientificWork,
				Authors = _scientificWorkService.GetAuthors(scientificWork.Id),
				Users = _userProfileService.GetAll()
			};

			return View(scientificWorksEdit);
		}

		// POST: ScientificWork/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, ScientificWorksEdit scientificWorksEdit)
		{
			var scientificWork = scientificWorksEdit.ScientificWork;
			if (id != scientificWork.Id)
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				scientificWorksEdit.Authors = _scientificWorkService.GetAuthors(scientificWork.Id);
				scientificWorksEdit.Users = _userProfileService.GetAll();
				return View(scientificWorksEdit);
			}
			try
			{
				_scientificWorkService.UpdateItem(scientificWork);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_scientificWorkService.Exists(scientificWork.Id))
				{
					return NotFound();
				}
				throw;
			}

			return RedirectToAction(nameof(Index));
		}

		// GET: ScientificWork/Delete/{id}
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var scientificWork = _scientificWorkService.GetById(id.Value);
			if (scientificWork == null)
			{
				return NotFound();
			}

			return View(scientificWork);
		}

		// POST: ScientificWork/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			_scientificWorkService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
		
		// POST: ScientificWork/AddAuthor/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddAuthor(Guid id, [FromBody] ScientificWorkAuthorRequest request)
		{
			_scientificWorkService.AddAuthor(id, request.AuthorId);
			return Json(ApiResponse.Ok);
		}
		
		// POST: ScientificWork/DeleteAuthor/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteAuthor(Guid id, [FromBody] ScientificWorkAuthorRequest request)
		{
			_scientificWorkService.RemoveAuthor(id, request.AuthorId);
			return Json(ApiResponse.Ok);
		}
	}
}
