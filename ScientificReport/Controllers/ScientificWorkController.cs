using System;
using System.Collections.Generic;
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

		public ScientificWorkController(ScientificReportDbContext context)
		{
			_scientificWork = new ScientificWorkService(context);
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

			var scientificWorksDetails = new ScientificWorksDetails
			{
				ScientificWork = scientificWork,
				Authors = _scientificWork.GetAuthors(scientificWork.Id).ToList()
			};

			return View(scientificWorksDetails);
		}

		// POST: ScientificWork/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, ScientificWorksDetails scientificWorksDetails)
		{
			var scientificWork = scientificWorksDetails.ScientificWork;
			if (id != scientificWork.Id)
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				scientificWorksDetails.Authors = _scientificWork.GetAuthors(scientificWork.Id).ToList();
				return View(scientificWorksDetails);
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
	}
}