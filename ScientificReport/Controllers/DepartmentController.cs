using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DTO.Models.Department;

namespace ScientificReport.Controllers
{
//	[Authorize(Roles = UserProfileRole.Administrator)]
	public class DepartmentController : Controller
	{
		private readonly IDepartmentService _departmentService;
		private readonly IUserProfileService _userProfileService;
		private readonly IScientificWorkService _scientificWorkService;
		
		public DepartmentController(IDepartmentService departmentService, IUserProfileService userProfileService, IScientificWorkService scientificWorkService)
		{
			_departmentService = departmentService;
			_userProfileService = userProfileService;
			_scientificWorkService = scientificWorkService;
		}

		// GET: Department/Index
		[HttpGet]
		public IActionResult Index()
		{
			return View(_departmentService.GetAll());
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

			return View(department);
		}

		// GET: Department/Create
		[HttpGet]
		public IActionResult Create()
		{
			var allUsers = _userProfileService.GetAll().ToList();
			var allDepartments = _departmentService.GetAll().ToList();

			var availableUsers = new List<SelectListItem>();
			foreach (var user in allUsers)
			{
				if (allDepartments.All(d => d.Head.Id != user.Id && !d.Staff.Contains(user)))
				{
					availableUsers.Add(new SelectListItem(user.FirstName + " " + user.MiddleName + " " + user.LastName, user.Id.ToString()));
				}
			}

			return View(new DepartmentCreateModel
			{
				UserSelection = availableUsers
			});
		}

		// POST: Department/Create
		[HttpPost]
		public IActionResult Create(DepartmentCreateModel model)
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
				_departmentService.CreateItem(new Department
				{
					Title = model.Title,
					Head = head,
					Staff = new List<UserProfile> {head}
				});
			}
			else
			{
				return NotFound();
			}

			return RedirectToAction("Index");
		}

		// GET: Department/Edit/{id}
		[HttpGet]
//		[Authorize(Roles = UserProfileRole.HeadOfDepartmentOrAdmin)]
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var department = _departmentService.GetById(id.Value);
			var departments = _departmentService.GetAllWhere(d => d.Id != department.Id).ToList();
			var allUsers = _userProfileService.GetAll();
			var availableUsers = new List<SelectListItem>();
			foreach (var user in allUsers)
			{
				if (departments.All(d => !d.Staff.Contains(user)))
				{
					availableUsers.Add(new SelectListItem(
						user.FirstName + " " + user.MiddleName + " " + user.LastName, user.Id.ToString(), user.Id.Equals(department.Head.Id)
					));
				}
			}

			var availableScientificWorks = _scientificWorkService
				.GetAllWhere(sw => departments.All(d => !d.ScientificWorks.Contains(sw)))
				.Select(scientificWork => new SelectListItem(scientificWork.Title, scientificWork.Id.ToString()))
				.ToList();

			if (department != null)
			{
				return View(new DepartmentEditModel
				{
					DepartmentId = department.Id,
					Title = department.Title,
					UserSelection = availableUsers,
					Head = department.Head,
					SelectedHeadId = department.Head.Id,
					Staff = department.Staff,
					ScientificWorkItems = availableScientificWorks,
					ScientificWorks = department.ScientificWorks,
					// TODO: uncomment User.IsInRole... after 'login' is fixed
					IsEditingByHead = true // User.IsInRole(UserProfileRole.Administrator)
				});
			}

			return RedirectToAction("Index");
		}

		// POST: Department/Edit/{id}
		[HttpPost]
//		[Authorize(Roles = UserProfileRole.HeadOfDepartmentOrAdmin)]
		public IActionResult Edit(Guid? id, [Bind("Title,SelectedHeadId")] DepartmentEditModel model)
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
			
			if (department != null)
			{
				department.Title = model.Title;
				var newHead = _userProfileService.GetById(model.SelectedHeadId);
				var departments = _departmentService.GetAllWhere(d => !d.Id.Equals(department.Id));
				if (newHead != null && departments.All(d => !d.Staff.Contains(newHead)))
				{
					department.Head = newHead;
				}
				
				_departmentService.UpdateItem(department);
			}
			else
			{
				return NotFound();
			}

			return RedirectToAction("Index");
		}

		// POST: Department/AddUserToStaff/{departmentId}
		[HttpPost]
		public IActionResult AddUserToStaff(Guid? id, [FromBody] DepartmentUpdateStaffRequest request)
		{
			if (id == null)
			{
				return NotFound();
			}
			
			var department = _departmentService.GetById(id.Value);
			if (department != null)
			{
				var user = _userProfileService.GetById(request.UserId);
				if (user != null && !_departmentService.UserIsHired(user))
				{
					department.Staff.Add(user);
					_departmentService.UpdateItem(department);
				}
				else
				{
					return Json(new
					{
						Success = false
					});
				}
			}

			return Json(new
			{
				Success = department != null
			});
		}
		
		// POST: Department/RemoveUserFromStaff/{departmentId}
		[HttpPost]
		public IActionResult RemoveUserFromStaff(Guid? id, [FromBody] DepartmentUpdateStaffRequest request)
		{
			if (id == null)
			{
				return NotFound();
			}
			
			var department = _departmentService.GetById(id.Value);
			if (department != null)
			{
				var user = _userProfileService.GetById(request.UserId);
				if (user != null && department.Staff.Contains(user) && !department.Head.Id.Equals(user.Id))
				{
					department.Staff.Remove(user);
					_departmentService.UpdateItem(department);
				}
				else
				{
					return Json(new
					{
						Success = false
					});
				}
			}

			return Json(new
			{
				Success = department != null
			});
		}
		
		// POST: Department/AddScientificWork/{departmentId}
		[HttpPost]
		public IActionResult AddScientificWork(Guid? id, [FromBody] DepartmentUpdateScientificWorksRequest request)
		{
			if (id == null)
			{
				return NotFound();
			}
			
			var department = _departmentService.GetById(id.Value);
			if (department != null)
			{
				var scientificWork = _scientificWorkService.GetById(request.ScientificWorkId);
				if (scientificWork != null && !_departmentService.GetAll().Any(d => d.ScientificWorks.Contains(scientificWork)))
				{
					department.ScientificWorks.Add(scientificWork);
					_departmentService.UpdateItem(department);
				}
				else
				{
					return Json(new
					{
						Success = false
					});
				}
			}

			return Json(new
			{
				Success = department != null
			});
		}
		
		// POST: Department/RemoveScientificWork/{departmentId}
		[HttpPost]
		public IActionResult RemoveScientificWork(Guid? id, [FromBody] DepartmentUpdateScientificWorksRequest request)
		{
			if (id == null)
			{
				return NotFound();
			}
			
			var department = _departmentService.GetById(id.Value);
			if (department != null)
			{
				var scientificWork = _scientificWorkService.GetById(request.ScientificWorkId);
				if (scientificWork != null && department.ScientificWorks.Contains(scientificWork))
				{
					department.ScientificWorks.Remove(scientificWork);
					_departmentService.UpdateItem(department);
				}
				else
				{
					return Json(new
					{
						Success = false
					});
				}
			}

			return Json(new
			{
				Success = department != null
			});
		}

		// POST: Department/Delete/{id}
		[HttpPost]
		public IActionResult Delete(Guid? id)
		{
			if (id == null || !_departmentService.DepartmentExists(id.Value))
			{
				return NotFound();
			}
	
			_departmentService.DeleteById(id.Value);
			return RedirectToAction("Index");
		}
	}
}
