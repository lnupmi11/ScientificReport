using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.Membership;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class MembershipController : Controller
	{
		private readonly IMembershipService _membershipService;

		public MembershipController(IMembershipService membershipService)
		{
			_membershipService = membershipService;
		}

		// GET: Membership
		public IActionResult Index(MembershipIndexModel model)
		{
			model.Memberships = _membershipService.GetPage(model.CurrentPage, model.PageSize);
			model.Count = _membershipService.GetCount();
			return View(model);
		}

		// GET: Membership/Details/{id}
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
		public IActionResult Create() => View();

		// POST: Membership/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(MembershipModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			_membershipService.CreateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: Membership/Edit/{id}
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

			return View(new MembershipEditModel(membership));
		}

		// POST: Membership/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, MembershipEditModel model)
		{
			if (id != model.Id || !_membershipService.Exists(id))
			{
				return NotFound();
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_membershipService.UpdateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: Membership/Delete/{id}
		public IActionResult Delete(Guid? id)
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

		// POST: Membership/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			if (!_membershipService.Exists(id))
			{
				return NotFound();
			}

			_membershipService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
