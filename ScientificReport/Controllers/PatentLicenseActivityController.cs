using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.PatentLicenseActivity;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class PatentLicenseActivityController : Controller
	{
		private readonly IPatentLicenseActivityService _patentLicenseActivityService;
		private readonly IDepartmentService _departmentService;
		private readonly IUserProfileService _userProfileService;

		public PatentLicenseActivityController(
			IPatentLicenseActivityService patentLicenseActivityService,
			IDepartmentService departmentService,
			IUserProfileService userProfileService
		)
		{
			_patentLicenseActivityService = patentLicenseActivityService;
			_departmentService = departmentService;
			_userProfileService = userProfileService;
		}

		// GET: PatentLicenseActivity
		public IActionResult Index(PatentLicenseActivityIndexModel model)
		{
			model.PatentLicenseActivities = _patentLicenseActivityService.GetPageByRole(model.CurrentPage, model.PageSize, User);
			model.Count = _patentLicenseActivityService.GetCountByRole(User);
			return View(model);
		}

		// GET: PatentLicenseActivity/Details/{id}
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var patentLicenseActivity = _patentLicenseActivityService.GetById(id.Value);
			if (patentLicenseActivity == null)
			{
				return NotFound();
			}

			if (!UserHasPermission(patentLicenseActivity))
			{
				return Forbid();
			}

			return View(patentLicenseActivity);
		}

		// GET: PatentLicenseActivity/Create
		public IActionResult Create() => View();

		// POST: PatentLicenseActivity/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(PatentLicenseActivityModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_patentLicenseActivityService.CreateItem(model);
			var patentLicenseActivity = _patentLicenseActivityService.Get(pla => pla.Name == model.Name);
			var user = _userProfileService.Get(User);
			if (model.Type == PatentLicenseActivity.Types.Patent)
			{
				_patentLicenseActivityService.AddAuthor(patentLicenseActivity, user);
			}
			else
			{
				_patentLicenseActivityService.AddApplicant(patentLicenseActivity, user);
			}

			return RedirectToAction(nameof(Index));
		}

		// GET: PatentLicenseActivity/Edit/{id}
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var patentLicenseActivity = _patentLicenseActivityService.GetById(id.Value);
			if (patentLicenseActivity == null)
			{
				return NotFound();
			}
			
			if (!UserHasPermission(patentLicenseActivity))
			{
				return Forbid();
			}

			return View(new PatentLicenseActivityEditModel(patentLicenseActivity)
			{
				Users = _userProfileService.GetAll(),
				AuthorsOrApplicants = patentLicenseActivity.Type == PatentLicenseActivity.Types.Patent
					? _patentLicenseActivityService.GetAuthors(patentLicenseActivity.Id)
					: _patentLicenseActivityService.GetApplicants(patentLicenseActivity.Id)
			});
		}

		// POST: PatentLicenseActivity/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid id, PatentLicenseActivityEditModel model)
		{
			if (id != model.Id || !_patentLicenseActivityService.Exists(id))
			{
				return NotFound();
			}

			var patentLicenseActivity = _patentLicenseActivityService.GetById(id);
			if (!UserHasPermission(patentLicenseActivity))
			{
				return Forbid();
			}
			
			if (!ModelState.IsValid)
			{
				model.Users = _userProfileService.GetAll();
				model.AuthorsOrApplicants = patentLicenseActivity.Type == PatentLicenseActivity.Types.Patent
					? _patentLicenseActivityService.GetAuthors(patentLicenseActivity.Id)
					: _patentLicenseActivityService.GetApplicants(patentLicenseActivity.Id);
				return View(model);
			}

			_patentLicenseActivityService.UpdateItem(model);
			return RedirectToAction(nameof(Index));
		}

		// GET: PatentLicenseActivity/Delete/{id}
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var patentLicenseActivity = _patentLicenseActivityService.GetById(id.Value);
			if (patentLicenseActivity == null)
			{
				return NotFound();
			}
			
			if (!UserHasPermission(patentLicenseActivity))
			{
				return Forbid();
			}

			return View(patentLicenseActivity);
		}

		// POST: PatentLicenseActivity/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			if (!_patentLicenseActivityService.Exists(id))
			{
				return NotFound();
			}
			
			if (!UserHasPermission(_patentLicenseActivityService.GetById(id)))
			{
				return Forbid();
			}

			_patentLicenseActivityService.DeleteById(id);
			return RedirectToAction(nameof(Index));
		}

		// POST: PatentLicenseActivity/AddUser/{patentLicenseActivityId}
		[HttpPost]
		[Authorize(Roles = UserProfileRole.HeadOfDepartmentOrAdmin)]
		public IActionResult AddUser(Guid? id, [FromBody] PatentLicenseActivityUpdUserRequest request)
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
			
			var patentLicenseActivity = _patentLicenseActivityService.GetById(id.Value);
			if (patentLicenseActivity == null)
			{
				return NotFound();
			}
			
			if (!UserHasPermission(patentLicenseActivity))
			{
				return Json(ApiResponse.Fail);
			}

			if (request.ActivityType == PatentLicenseActivity.Types.Patent)
			{
				if (!_patentLicenseActivityService.GetAuthors(patentLicenseActivity.Id).Contains(user))
				{
					_patentLicenseActivityService.AddAuthor(patentLicenseActivity, user);
				}
			}
			else
			{
				if (!_patentLicenseActivityService.GetApplicants(patentLicenseActivity.Id).Contains(user))
				{
					_patentLicenseActivityService.AddApplicant(patentLicenseActivity, user);
				}
			}

			return Json(ApiResponse.Ok);
		}

		// POST: PatentLicenseActivity/RemoveUser/{patentLicenseActivityId}
		[HttpPost]
		[Authorize(Roles = UserProfileRole.HeadOfDepartmentOrAdmin)]
		public IActionResult RemoveUser(Guid? id, [FromBody] PatentLicenseActivityUpdUserRequest request)
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
			
			var patentLicenseActivity = _patentLicenseActivityService.GetById(id.Value);
			if (patentLicenseActivity == null)
			{
				return NotFound();
			}
			
			if (!UserHasPermission(patentLicenseActivity))
			{
				return Json(ApiResponse.Fail);
			}

			if (request.ActivityType == PatentLicenseActivity.Types.Patent)
			{
				if (_patentLicenseActivityService.GetAuthors(patentLicenseActivity.Id).Contains(user))
				{
					_patentLicenseActivityService.RemoveAuthor(patentLicenseActivity.Id, user);
				}
			}
			else
			{
				if (_patentLicenseActivityService.GetApplicants(patentLicenseActivity.Id).Contains(user))
				{
					_patentLicenseActivityService.RemoveApplicant(patentLicenseActivity.Id, user);
				}
			}

			return Json(ApiResponse.Ok);
		}
		
		private bool UserHasPermission(PatentLicenseActivity patentLicenseActivity)
		{
			var user = _userProfileService.Get(User);
			var department = _departmentService.Get(d => d.Staff.Contains(user));
			return PageHelpers.IsAdmin(User) ||
				   PageHelpers.IsHeadOfDepartment(User) &&
				   (patentLicenseActivity.AuthorsPatentLicenseActivities.Any(p => department.Staff.Contains(p.Author)) ||
					patentLicenseActivity.ApplicantsPatentLicenseActivities.Any(p => department.Staff.Contains(p.Applicant))) ||
				   patentLicenseActivity.AuthorsPatentLicenseActivities.Any(a => a.Author.Id == user.Id) ||
				   patentLicenseActivity.ApplicantsPatentLicenseActivities.Any(a => a.Applicant.Id == user.Id);
		}
	}
}
