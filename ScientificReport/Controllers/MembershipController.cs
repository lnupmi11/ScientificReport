using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.Entities;

namespace ScientificReport.Controllers
{
	public class MembershipController : Controller
	{
		private readonly IMembershipService _membershipService;
		private readonly IUserProfileService _userProfileService;

		public MembershipController(IMembershipService membershipService, IUserProfileService userProfileService)
		{
			_membershipService = membershipService;
			_userProfileService = userProfileService;
		}
		
		// GET: Membership
		public IActionResult Index()
		{
			return View(_membershipService.GetAll());
		}
		
		// GET: Membership/Details/{id}
		[HttpGet]
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var membership = _membershipService.GetById(id.Value);
			if (membership == null)
			{
				return NotFound();
			}

			return View(membership);
		}
		
		// GET: Membership/Create
		public IActionResult Create()
		{
			return View();
		}
		
		// POST: Membership/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Membership model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_membershipService.CreateItem(model);
			return RedirectToAction(nameof(Index));
		}	
		
		// GET: Membership/Edit/5
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var membership = _membershipService.GetById(id.Value);
			if (membership == null)
			{
				return NotFound();
			}

			return View(membership);
		}
		
		// POST: Membership/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, [Bind("Id,MemberOf,MembershipInfo")] Membership membership)
		{
			if (id != membership.Id)
			{
				return NotFound();
			}

			if (!ModelState.IsValid) return View(membership);
			try
			{
				_membershipService.UpdateItem(membership);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_membershipService.Exists(membership.Id))
					return NotFound();

				throw;
			}

			return RedirectToAction(nameof(Index));
		}
		
		// GET: Membership/Delete/{id}
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var report = _membershipService.GetById(id.Value);
			if (report == null)
			{
				return NotFound();
			}

			return View(report);
		}

		// POST: Membership/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			_membershipService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
	}
}