using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models;
using ScientificReport.DTO.Models.ScientificInternship;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class ScientificInternshipController : Controller
	{
		private readonly IScientificInternshipService _scientificInternshipService;
		private readonly IUserProfileService _userProfileService;
		private readonly IDepartmentService _departmentService;

		public ScientificInternshipController(
			IScientificInternshipService scientificInternshipService,
			IUserProfileService userProfileService,
			IDepartmentService departmentService
		)
		{
			_scientificInternshipService = scientificInternshipService;
			_userProfileService = userProfileService;
			_departmentService = departmentService;
		}

		// GET: ScientificInternship
		public IActionResult Index(ScientificInternshipIndexModel model)
		{
			model.ScientificInternships = _scientificInternshipService.GetPageByRole(model.CurrentPage, model.PageSize, User);
			model.Count = _scientificInternshipService.GetCountByRole(User);
			return View(model);
		}

		// GET: ScientificInternship/Details/{id}
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var scientificInternship = _scientificInternshipService.GetById(id.Value);
			if (scientificInternship == null)
			{
				return NotFound();
			}

			if (!UserHasPermission(scientificInternship))
			{
				return Forbid();
			}

			return View(scientificInternship);
		}

		// GET: ScientificInternship/Create
		public IActionResult Create() => View();

		// POST: ScientificInternship/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ScientificInternshipModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			
			_scientificInternshipService.CreateItem(model);
			_scientificInternshipService.AddUser(
				_scientificInternshipService.Get(si =>
					si.Contents == model.Contents && si.PlaceOfInternship == model.PlaceOfInternship),
				_userProfileService.Get(User));
			
			return RedirectToAction(nameof(Index));
		}

		// GET: ScientificInternship/Edit/{id}
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var scientificInternship = _scientificInternshipService.GetById(id.Value);
			if (scientificInternship == null)
			{
				return NotFound();
			}
			
			if (!UserHasPermission(scientificInternship))
			{
				return Forbid();
			}

			return View(new ScientificInternshipEditModel(scientificInternship)
			{
				Users = _scientificInternshipService.GetUsers(scientificInternship.Id),
				AllUsers = _userProfileService.GetAll()
			});
		}

		// POST: ScientificInternship/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, ScientificInternshipEditModel model)
		{
			if (id != model.Id || !_scientificInternshipService.Exists(id))
			{
				return NotFound();
			}

			var scientificInternship = _scientificInternshipService.GetById(id);
			if (!UserHasPermission(scientificInternship))
			{
				return Forbid();
			}

			if (!ModelState.IsValid)
			{
				model.Users = _scientificInternshipService.GetUsers(scientificInternship.Id);
				model.AllUsers = _userProfileService.GetAll();
				return View(model);
			}

			_scientificInternshipService.UpdateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: ScientificInternship/Delete/{id}
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var scientificInternship = _scientificInternshipService.GetById(id.Value);
			if (scientificInternship == null)
			{
				return NotFound();
			}
			
			if (!UserHasPermission(scientificInternship))
			{
				return Forbid();
			}

			return View(scientificInternship);
		}

		// POST: ScientificInternship/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			if (!_scientificInternshipService.Exists(id))
			{
				return NotFound();
			}
			
			if (!UserHasPermission(_scientificInternshipService.GetById(id)))
			{
				return Forbid();
			}

			_scientificInternshipService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}
		
		// POST: ScientificInternship/AddUser/{scientificInternshipId}
		[HttpPost]
		public IActionResult AddUser(Guid? id, [FromBody] UpdateUserRequest request)
		{
			if (id == null)
			{
				return NotFound();
			}
			
			var user = _userProfileService.GetById(request.UserId);
			if (user == null)
			{
				return Json(ApiResponse.Fail);
			}
			
			var scientificInternship = _scientificInternshipService.GetById(id.Value);
			if (scientificInternship == null)
			{
				return NotFound();
			}
			
			if (!UserHasPermission(scientificInternship))
			{
				return Json(ApiResponse.Fail);
			}
			
			if (!_scientificInternshipService.GetUsers(scientificInternship.Id).Contains(user))
			{
				_scientificInternshipService.AddUser(scientificInternship, user);
			}

			return Json(ApiResponse.Ok);
		}

		// POST: ScientificInternship/RemoveUser/{scientificInternshipId}
		[HttpPost]
		public IActionResult RemoveUser(Guid? id, [FromBody] UpdateUserRequest request)
		{
			if (id == null)
			{
				return NotFound();
			}
			
			var user = _userProfileService.GetById(request.UserId);
			if (user == null)
			{
				return Json(ApiResponse.Fail);
			}
			
			var publication = _scientificInternshipService.GetById(id.Value);
			if (publication == null)
			{
				return NotFound();
			}
			
			if (!UserHasPermission(publication))
			{
				return Json(ApiResponse.Fail);
			}
			
			if (_scientificInternshipService.GetUsers(publication.Id).Contains(user))
			{
				_scientificInternshipService.RemoveUser(publication, user);
			}

			return Json(ApiResponse.Ok);
		}
		
		private bool UserHasPermission(ScientificInternship scientificInternship)
		{
			var user = _userProfileService.Get(User);
			var department = _departmentService.Get(d => d.Staff.Contains(user));
			return PageHelpers.IsAdmin(User) ||
				   PageHelpers.IsHeadOfDepartment(User) &&
				   scientificInternship.UserProfilesScientificInternships.Any(p => department.Staff.Contains(p.UserProfile)) ||
				   scientificInternship.UserProfilesScientificInternships.Any(p => p.UserProfile.Id == user.Id);
		}
	}
}
