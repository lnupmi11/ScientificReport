using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities.Publications;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models;
using ScientificReport.DTO.Models.ScientificWorks;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Teacher)]
	public class ScientificWorkController : Controller
	{
		private readonly IScientificWorkService _scientificWorkService;
		private readonly IUserProfileService _userProfileService;
		private readonly IDepartmentService _departmentService;

		public ScientificWorkController(
			IScientificWorkService scientificWorkService,
			IUserProfileService userProfileService,
			IDepartmentService departmentService
		)
		{
			_scientificWorkService = scientificWorkService;
			_userProfileService = userProfileService;
			_departmentService = departmentService;
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

			if (!UserHasPermission(scientificWork))
			{
				return Forbid();
			}
			
			var scientificWorksDetails = new ScientificWorksDetails
			{
				ScientificWork = scientificWork,
				Authors = _scientificWorkService.GetAuthors(scientificWork.Id).ToList()
			};

			return View(scientificWorksDetails);
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

			if (!UserHasPermission(scientificWork))
			{
				return Forbid();
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
			
			if (!UserHasPermission(scientificWork))
			{
				return Forbid();
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

			return RedirectToAction("Index", "Publication");
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
			
			if (!UserHasPermission(scientificWork))
			{
				return Forbid();
			}

			return View(scientificWork);
		}

		// POST: ScientificWork/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			if (!_scientificWorkService.Exists(id))
			{
				return NotFound();
			}
			
			if (!UserHasPermission(_scientificWorkService.GetById(id)))
			{
				return Forbid();
			}
			
			_scientificWorkService.DeleteById(id);
			return RedirectToAction("Index", "Publication");
		}
		
		// POST: ScientificWork/AddAuthor/{id}
		[HttpPost]
		public IActionResult AddAuthor(Guid id, [FromBody] UpdateUserRequest request)
		{
			if (!_scientificWorkService.Exists(id))
			{
				return NotFound();
			}
			
			if (!UserHasPermission(_scientificWorkService.GetById(id)))
			{
				return Forbid();
			}
			
			_scientificWorkService.AddAuthor(id, request.UserId);
			return Json(ApiResponse.Ok);
		}
		
		// POST: ScientificWork/DeleteAuthor/{id}
		[HttpPost]
		public IActionResult DeleteAuthor(Guid id, [FromBody] UpdateUserRequest request)
		{
			if (!_scientificWorkService.Exists(id))
			{
				return NotFound();
			}
			
			if (!UserHasPermission(_scientificWorkService.GetById(id)))
			{
				return Forbid();
			}
			
			_scientificWorkService.RemoveAuthor(id, request.UserId);
			return Json(ApiResponse.Ok);
		}
		
		private bool UserHasPermission(ScientificWork scientificWork)
		{
			var user = _userProfileService.Get(User);
			var department = _departmentService.Get(d => d.Staff.Contains(user));
			var isHeadOfDepartment = PageHelpers.IsHeadOfDepartment(User) && scientificWork.UserProfilesScientificWorks.Any(p => department.Staff.Contains(p.UserProfile));
			return PageHelpers.IsAdmin(User) || isHeadOfDepartment || scientificWork.UserProfilesScientificWorks.Any(p => p.UserProfile.UserName == User.Identity.Name);
		}
	}
}
