using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.ScientificWorks;

namespace ScientificReport.Controllers
{
	public class ScientificWorkController : Controller
	{
		private readonly ScientificWorkService _scientificWork;
		private readonly UserProfileService _userProfile;

		public ScientificWorkController(ScientificReportDbContext context)
		{
			_scientificWork = new ScientificWorkService(context);
			_userProfile = new UserProfileService(context);
		}

		// GET: ScientificWork
		public IActionResult Index()
		{
			return View(_scientificWork.GetAll());
		}

		// GET: ScientificWork/Details/5
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var scientificWork = _scientificWork.GetById(id.Value);
			if (scientificWork == null)
			{
				return NotFound();
			}

			var scientificWorksDetails = new ScientificWorksDetails
			{
				ScientificWork = scientificWork,
				Authors = _scientificWork.GetAuthors(scientificWork.Id).ToList()
			};

			return View(scientificWorksDetails);
		}

		// GET: ScientificWork/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: ScientificWork/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,Cypher,Category,Title,Contents")]
			ScientificWork scientificWork)
		{
			if (!ModelState.IsValid) return View(scientificWork);

			_scientificWork.CreateItem(scientificWork);
			return RedirectToAction(nameof(Index));
		}

		// GET: ScientificWork/Edit/5
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var scientificWork = _scientificWork.GetById(id.Value);
			if (scientificWork == null)
			{
				return NotFound();
			}

			
			var scientificWorksEdit = new ScientificWorksEdit
			{
				ScientificWork = scientificWork,
				Authors = _scientificWork.GetAuthors(scientificWork.Id),
				Users = _userProfile.GetAll()
			};

			return View(scientificWorksEdit);
		}

		// POST: ScientificWork/Edit/5
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
				scientificWorksEdit.Authors = _scientificWork.GetAuthors(scientificWork.Id);
				scientificWorksEdit.Users = _userProfile.GetAll();
				return View(scientificWorksEdit);
			}
			try
			{
				_scientificWork.UpdateItem(scientificWork);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_scientificWork.Exists(scientificWork.Id))
				{
					return NotFound();
				}
				throw;
			}

			return RedirectToAction(nameof(Index));
		}

		// GET: ScientificWork/Delete/5
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var scientificWork = _scientificWork.GetById(id.Value);
			if (scientificWork == null)
			{
				return NotFound();
			}

			return View(scientificWork);
		}

		// POST: ScientificWork/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			_scientificWork.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
		
		// POST: ScientificWork/AddAuthor/5
		[HttpPost]
//		[ValidateAntiForgeryToken]
		public IActionResult AddAuthor(Guid id, [FromBody] ScientificWorkAuthorRequest request)
		{
			_scientificWork.AddAuthor(id, request.AuthorId);
			return Json(new
			{
				Success = true
			});
		}
		// POST: ScientificWork/DeleteAuthor/5
		[HttpPost]
//		[ValidateAntiForgeryToken]
		public IActionResult DeleteAuthor(Guid id, [FromBody] ScientificWorkAuthorRequest request)
		{
			_scientificWork.RemoveAuthor(id, request.AuthorId);
			return Json(new
			{
				Success = true
			});
		}
	}
}