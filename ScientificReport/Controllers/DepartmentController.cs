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
			var allDepartments = _departmentService.GetAll();
			if (department != null)
			{
				return View(new DepartmentEditModel
				{
					Title = department.Title,
					Head = department.Head,
					Users = _userProfileService.GetAll(),
					ScientificWorks = _scientificWorkService.GetAllWhere(
						sw => allDepartments.All(d => !d.ScientificWorks.Contains(sw))
					),
					// TODO: uncomment User.IsInRole... after 'login' is fixed
					IsEditingByHead = true // User.IsInRole(UserProfileRole.Administrator)
				});
			}

			return RedirectToAction("Index");
		}

		// POST: Department/Edit/{id}
		[HttpPost]
//		[Authorize(Roles = UserProfileRole.HeadOfDepartmentOrAdmin)]
		public IActionResult Edit(DepartmentEditModel model)
		{
			/*
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (_departmentService.DepartmentExists(model.Department.Id))
			{
				_departmentService.UpdateItem(model.Department);
			}
			else
			{
				return NotFound();
			}
			*/

			return RedirectToAction("Index");
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
