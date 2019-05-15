using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.Department;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class DepartmentController : Controller
	{
		private readonly UserManager<UserProfile> _userManager;

		private readonly IDepartmentService _departmentService;
		private readonly IUserProfileService _userProfileService;
		private readonly IScientificWorkService _scientificWorkService;

		public DepartmentController(IDepartmentService departmentService, IUserProfileService userProfileService,
			IScientificWorkService scientificWorkService, UserManager<UserProfile> userManager)
		{
			_userManager = userManager;
			_departmentService = departmentService;
			_userProfileService = userProfileService;
			_scientificWorkService = scientificWorkService;
		}

		// GET: Department/Index
		[HttpGet]
		public IActionResult Index()
		{
			if (PageHelpers.IsAdmin(User))
			{
				return View(_departmentService.GetAll());
			}

			var department = _departmentService.Get(
				d => d.Staff.Contains(_userProfileService.Get(User))
			);

			if (department == null)
			{
				return NotFound();
			}

			return RedirectToAction("Details", new { id = department.Id });
		}

		// GET: Department/Details/{id}
		[HttpGet]
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var department = _departmentService.GetById(id.Value);
			if (department == null)
			{
				return NotFound();
			}

			if (!PageHelpers.IsAdmin(User) && !department.Staff.Contains(_userProfileService.Get(User)))
			{
				return Forbid();
			}

			return View(department);
		}

		// GET: Department/Create
		[HttpGet]
		[Authorize(Roles = UserProfileRole.Administrator)]
		public async Task<IActionResult> Create()
		{
			var allUsers = _userProfileService.GetAll();
			var allDepartments = _departmentService.GetAll();

			var usersToCheck = allUsers.Where(user => allDepartments.All(d => d.Head.Id != user.Id && !d.Staff.Contains(user))).ToList();
			var availableUsers = new List<UserProfile>();
			foreach (var user in usersToCheck)
			{
				if (await _userProfileService.IsTeacherOnlyAsync(user, _userManager))
				{
					availableUsers.Add(user);
				}
			}
			
			return View(new DepartmentCreateModel
			{
				UserSelection = availableUsers
			});
		}

		// POST: Department/Create
		[HttpPost]
		[Authorize(Roles = UserProfileRole.Administrator)]
		public async Task<IActionResult> Create(DepartmentCreateModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (_departmentService.Get(d => d.Title == model.Title) != null)
			{
				return RedirectToAction("Index");
			}

			if (_userProfileService.UserExists(model.SelectedHeadId))
			{
				var head = _userProfileService.GetById(model.SelectedHeadId);
				var isAvailable =
					!(await _userProfileService.IsInRoleAsync(head, UserProfileRole.Administrator, _userManager)
					&& await _userProfileService.IsInRoleAsync(head, UserProfileRole.HeadOfDepartment, _userManager));

				if (isAvailable)
				{
					_departmentService.CreateItem(new Department
					{
						Title = model.Title,
						Head = head,
						Staff = new List<UserProfile> {head}
					});
					head.Position = "HeadOfDepartment";
					_userProfileService.UpdateItem(head);
					await _userProfileService.AddToRoleAsync(head, UserProfileRole.HeadOfDepartment, _userManager);	
				}
				else
				{
					return BadRequest();
				}
			}
			else
			{
				return NotFound();
			}

			return RedirectToAction("Index");
		}

		// GET: Department/Edit/{id}
		[HttpGet]
		[Authorize(Roles = UserProfileRole.HeadOfDepartmentOrAdmin)]
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var department = _departmentService.GetById(id.Value);
			if (department == null)
			{
				return RedirectToAction("Index");
			}

			if (!IsValidCurrentUser(department))
			{
				return Forbid();
			}

			var departments = _departmentService.GetAllWhere(d => d.Id != department.Id).ToList();
			var allUsers = _userProfileService.GetAll();

			var availableUsers = allUsers.Where(user => departments.All(d => !d.Staff.Contains(user)));

			var availableScientificWorks = _scientificWorkService.GetAllWhere(sw => departments.All(d => !d.ScientificWorks.Contains(sw)));

			return View(new DepartmentEditModel
			{
				Department = department,
				DepartmentId = department.Id,
				Title = department.Title,
				UserSelection = availableUsers,
				Head = department.Head,
				SelectedHeadId = department.Head.Id,
				Staff = department.Staff,
				AvailableScientificWork = availableScientificWorks,
				ScientificWorks = department.ScientificWorks,
				IsEditingByHead = department.Head.Id == _userProfileService.Get(u => u.UserName == User.Identity.Name).Id
			});
		}

		// POST: Department/Edit/{id}
		[HttpPost]
		[Authorize(Roles = UserProfileRole.HeadOfDepartmentOrAdmin)]
		public async Task<IActionResult> Edit(Guid? id, [Bind("Title,SelectedHeadId")] DepartmentEditModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (id == null)
			{
				return NotFound();
			}

			var department = _departmentService.GetById(id.Value);
			if (department == null)
			{
				return NotFound();
			}

			if (!IsValidCurrentUser(department))
			{
				return Forbid();
			}

			department.Title = model.Title;
			var newHead = _userProfileService.GetById(model.SelectedHeadId);
			var departments = _departmentService.GetAllWhere(d => !d.Id.Equals(department.Id));
			if (newHead != null && departments.All(d => !d.Staff.Contains(newHead)))
			{
				var oldHead = department.Head;
				oldHead.Position = "Teacher";
				_userProfileService.UpdateItem(oldHead);
				await _userProfileService.RemoveFromRoleAsync(oldHead, UserProfileRole.HeadOfDepartment,
					_userManager);

				department.Head = newHead;

				newHead.Position = "HeadOfDepartment";
				_userProfileService.UpdateItem(newHead);
				await _userProfileService.AddToRoleAsync(newHead, UserProfileRole.HeadOfDepartment, _userManager);

				if (!department.Staff.Contains(newHead))
				{
					department.Staff.Add(newHead);
					_departmentService.UpdateItem(department);
				}
			}

			_departmentService.UpdateItem(department);

			return RedirectToAction("Index");
		}

		// POST: Department/AddUserToStaff/{departmentId}
		[HttpPost]
		[Authorize(Roles = UserProfileRole.HeadOfDepartmentOrAdmin)]
		public async Task<IActionResult> AddUserToStaff(Guid? id, [FromBody] DepartmentUpdateStaffRequest request)
		{
			if (id == null)
			{
				return NotFound();
			}
			
			var department = _departmentService.GetById(id.Value);
			if (department == null)
			{
				return Json(ApiResponse.Fail);
			}
			
			if (!IsValidCurrentUser(department))
			{
				return Json(ApiResponse.Fail);
			}

			var user = _userProfileService.GetById(request.UserId);
			var isNotAvailable = await _userProfileService.IsInRoleAsync(user, UserProfileRole.HeadOfDepartment, _userManager);
			
			if (user == null || _departmentService.UserIsHired(user) || isNotAvailable)
			{
				return Json(ApiResponse.Fail);
			}

			department.Staff.Add(user);
			_departmentService.UpdateItem(department);

			return Json(ApiResponse.Ok);
		}

		// POST: Department/RemoveUserFromStaff/{departmentId}
		[HttpPost]
		[Authorize(Roles = UserProfileRole.HeadOfDepartmentOrAdmin)]
		public IActionResult RemoveUserFromStaff(Guid? id, [FromBody] DepartmentUpdateStaffRequest request)
		{
			if (id == null)
			{
				return NotFound();
			}

			var department = _departmentService.GetById(id.Value);
			if (department == null)
			{
				return Json(ApiResponse.Fail);
			}

			if (!IsValidCurrentUser(department))
			{
				return Json(ApiResponse.Fail);
			}
			
			var user = _userProfileService.GetById(request.UserId);
			if (user == null || !department.Staff.Contains(user) || department.Head.Id.Equals(user.Id))
			{
				return Json(ApiResponse.Fail);
			}

			department.Staff.Remove(user);
			_departmentService.UpdateItem(department);

			return Json(ApiResponse.Ok);
		}

		// POST: Department/AddScientificWork/{departmentId}
		[HttpPost]
		[Authorize(Roles = UserProfileRole.HeadOfDepartment)]
		public IActionResult AddScientificWork(Guid? id, [FromBody] DepartmentUpdateScientificWorksRequest request)
		{
			if (id == null)
			{
				return NotFound();
			}

			var department = _departmentService.GetById(id.Value);
			if (department == null)
			{
				return Json(new
				{
					Success = false
				});
			}

			if (!IsValidCurrentUser(department))
			{
				return Json(new
				{
					Success = false
				});
			}
			
			var scientificWork = _scientificWorkService.GetById(request.ScientificWorkId);
			if (scientificWork == null ||
			    _departmentService.GetAll().Any(d => d.ScientificWorks.Contains(scientificWork)))
			{
				return Json(new
				{
					Success = false
				});
			}

			department.ScientificWorks.Add(scientificWork);
			_departmentService.UpdateItem(department);

			return Json(new
			{
				Success = true
			});
		}

		// POST: Department/RemoveScientificWork/{departmentId}
		[HttpPost]
		[Authorize(Roles = UserProfileRole.HeadOfDepartment)]
		public IActionResult RemoveScientificWork(Guid? id, [FromBody] DepartmentUpdateScientificWorksRequest request)
		{
			if (id == null)
			{
				return NotFound();
			}

			var department = _departmentService.GetById(id.Value);
			if (department == null)
			{
				return Json(ApiResponse.Fail);
			}

			if (!IsValidCurrentUser(department))
			{
				return Json(ApiResponse.Fail);
			}
			
			var scientificWork = _scientificWorkService.GetById(request.ScientificWorkId);
			if (scientificWork == null || !department.ScientificWorks.Contains(scientificWork))
			{
				return Json(ApiResponse.Fail);
			}

			department.ScientificWorks.Remove(scientificWork);
			_departmentService.UpdateItem(department);

			return Json(ApiResponse.Ok);
		}

		// POST: Department/Delete/{id}
		[HttpPost]
		[Authorize(Roles = UserProfileRole.Administrator)]
		public async Task<IActionResult> Delete(Guid? id)
		{
			if (id == null || !_departmentService.Exists(id.Value))
			{
				return NotFound();
			}

			var departmentToDelete = _departmentService.GetById(id.Value);
			if (!IsValidCurrentUser(departmentToDelete))
			{
				return Forbid();
			}

			if (departmentToDelete.Staff.Count == 1)
			{
				await _userProfileService.RemoveFromRoleAsync(departmentToDelete.Head, UserProfileRole.HeadOfDepartment, _userManager);
				departmentToDelete.Head.Position = UserProfileRole.Teacher;
				_userProfileService.UpdateItem(departmentToDelete.Head);
				_departmentService.DeleteById(id.Value);
			}

			return RedirectToAction("Index");
		}

		private bool IsValidCurrentUser(Department department)
		{
			var currentUser = _userProfileService.Get(User);
			if (!PageHelpers.IsAdmin(User))
			{
				return currentUser.Id == department.Head.Id;	
			}

			return true;
		}
	}
}
