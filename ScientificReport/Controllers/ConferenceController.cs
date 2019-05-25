using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.Conference;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class ConferenceController : Controller
	{
		private readonly IConferenceService _conferenceService;

		public ConferenceController(IConferenceService conferenceService)
		{
			_conferenceService = conferenceService;
		}

		// GET: Conference
		public IActionResult Index(ConferenceIndexModel model)
		{
			model.Conferences = _conferenceService.GetPage(model.CurrentPage, model.PageSize);
			model.Count = _conferenceService.GetCount();
			return View(model);
		}

		// GET: Conference/Details/5
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

			var conferenceDetails = new ConferenceDetails
			{
				Conference = conference
			};

			return View(conferenceDetails);
		}

		// GET: Conference/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Conference/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id,Topic,Date")] Conference conference)
		{
			if (!ModelState.IsValid) return View(conference);

			_conferenceService.CreateItem(conference);
			return RedirectToAction(nameof(Index));
		}

		// GET: Conference/Edit/5
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

			
			var conferenceEdit = new ConferenceEdit
			{
				Conference = conference
			};

			return View(conferenceEdit);
		}

		// POST: Conference/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, ConferenceEdit conferenceEdit)
		{
			var conference = conferenceEdit.Conference;
			if (id != conference.Id)
			{
				return NotFound();
			}

			if (!ModelState.IsValid) return View(conferenceEdit);
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

		// GET: Conference/Delete/5
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

			return View(conference);
		}

		// POST: Conference/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			_conferenceService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
