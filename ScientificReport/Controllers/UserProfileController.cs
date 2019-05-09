using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.UserProfile;

namespace ScientificReport.Controllers
{
//	[Authorize(Roles = UserProfileRole.Administrator)]
	public class UserProfileController : Controller
	{
		private readonly UserManager<UserProfile> _userManager;
		private readonly SignInManager<UserProfile> _signInManager;
		private readonly RoleManager<UserProfileRole> _roleManager;
		
		private readonly IUserProfileService _userProfileService;
		private readonly IDepartmentService _departmentService;

		public UserProfileController(
			UserManager<UserProfile> usrMgr,
			SignInManager<UserProfile> signInManager,
			RoleManager<UserProfileRole> roleManager,
			IUserProfileService userProfileService,
			IDepartmentService departmentService
		)
		{
			_userManager = usrMgr;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_userProfileService = userProfileService;
			_departmentService = departmentService;
		}

		// GET: UserProfile/Index
		[HttpGet]
		public IActionResult Index()
		{
			return View(_userProfileService.GetAll());
		}

		// GET: UserProfile/Details/{id}
		[HttpGet]
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userProfile = _userProfileService.GetById(id.Value);
			if (userProfile == null)
			{
				return NotFound();
			}

			var department = _departmentService.Get(d => d.Staff.Contains(userProfile));
			
			var detailsModel = new UserDetailsModel
			{
				User = userProfile,
				DepartmentName = department != null ? "кафедри " + department.Title : ""
			};

			return View(detailsModel);
		}

		// GET: UserProfile/Edit/{id}
		[HttpGet]
		public async Task<IActionResult> Edit(Guid? id) {
			if (id == null)
			{
				return NotFound();
			}
			var user = _userProfileService.GetById(id.Value);
			if (user != null)
			{
				return View(new UserProfileEditModel
				{
					UserId = user.Id,
					FirstName = user.FirstName,
					MiddleName = user.MiddleName,
					LastName = user.LastName,
					BirthYear = user.BirthYear,
					GraduationYear = user.GraduationYear,
					ScientificDegree = user.ScientificDegree,
					YearDegreeGained = user.YearDegreeGained,
					AcademicStatus = user.AcademicStatus,
					YearDegreeAssigned = user.YearDegreeAssigned,
					PhoneNumber = user.PhoneNumber,
					IsApproved = user.IsApproved,
					UserName = user.UserName,
					Email = user.Email,
					AllRoles = _roleManager.Roles.ToList(),
					UserRoles = await _userManager.GetRolesAsync(user)
				});
			}

			return RedirectToAction("Index");
		}

		// POST: UserProfile/Edit/{id}
		[HttpPost]
		public IActionResult Edit(Guid? id, UserProfileEditModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (id == null)
			{
				return NotFound();
			}

			if (_userProfileService.UserExists(id.Value))
			{
				var user = _userProfileService.GetById(id.Value);

				user.FirstName = model.FirstName;
				user.MiddleName = model.MiddleName;
				user.LastName = model.LastName;
				user.BirthYear = model.BirthYear;
				user.GraduationYear = model.GraduationYear;
				user.ScientificDegree = model.ScientificDegree;
				user.YearDegreeGained = model.YearDegreeGained;
				user.AcademicStatus = model.AcademicStatus;
				user.YearDegreeAssigned = model.YearDegreeAssigned;
				user.PhoneNumber = model.PhoneNumber;
				user.IsApproved = model.IsApproved;
				user.UserName = model.UserName;
				user.Email = model.Email;
				
				_userProfileService.UpdateItem(user);
			}
			else
			{
				return NotFound();
			}
			
			return RedirectToAction("Index");
		}

		// POST: UserProfile/AddUserToAdministration/{userId}
		[HttpPost]
		public async Task<IActionResult> AddUserToAdministration(Guid? id, [FromBody] UserProfileUpdateRolesRequest request)
		{
			if (id == null)
			{
				return NotFound();
			}

			if (request.RoleName.Equals(UserProfileRole.Teacher) ||
			    request.RoleName.Equals(UserProfileRole.HeadOfDepartment))
			{
				return Json(new {Success = false});
			}
			
			var userExists = _userProfileService.UserExists(id.Value);
			if (userExists)
			{
				var user = _userProfileService.GetById(id.Value);
				if (!await _userProfileService.IsInRoleAsync(user, request.RoleName, _userManager))
				{
					await _userProfileService.AddToRoleAsync(user, request.RoleName, _userManager);
				}	
			}

			return Json(new {Success = userExists});
		}
		
		// POST: UserProfile/RemoveUserFromAdministration/{userId}
		[HttpPost]
		public async Task<IActionResult> RemoveUserFromAdministration(Guid? id, [FromBody] UserProfileUpdateRolesRequest request)
		{
			if (id == null)
			{
				return NotFound();
			}
			
			var userExists = _userProfileService.UserExists(id.Value);
			if (userExists)
			{
				var user = _userProfileService.GetById(id.Value);
				if (await _userProfileService.IsInRoleAsync(user, request.RoleName, _userManager))
				{
					await _userProfileService.RemoveFromRoleAsync(user, request.RoleName, _userManager);
					if (request.RoleName == UserProfileRole.HeadOfDepartment)
					{
						user.Position = "Викладач";
						_userProfileService.UpdateItem(user);
					}
				}
			}
			
			return Json(new
			{
				Success = userExists
			});
		}

		// POST: UserProfile/Delete/{id}
		[HttpPost]
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			
			if (!_userProfileService.UserExists(id.Value))
			{
				return NotFound();
			}
			
			_userProfileService.DeleteById(id.Value);
			return RedirectToAction("Index");
		}

		// GET: UserProfile/Register
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register() => View();
		
		// POST: UserProfile/Register
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterModel model) {
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (_userProfileService.Get(usr => usr.UserName == model.UserName) != null)
			{
				ModelState.AddModelError(string.Empty, "User already exists");
				return BadRequest();
			}
			
			var user = new UserProfile {
				UserName = model.UserName,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				MiddleName = model.MiddleName,
				BirthYear = model.BirthYear,
				AcademicStatus = model.AcademicStatus,
				GraduationYear = model.GraduationYear,
				ScientificDegree = model.ScientificDegree,
				YearDegreeGained = model.YearDegreeGained,
				YearDegreeAssigned = model.YearDegreeAssigned,
				Position = "Викладач",
				IsApproved = false,
				PhoneNumber = model.PhoneNumber
			};
			if (model.Password.Equals(model.PasswordRepeat))
			{
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					var createdUser = _userProfileService.Get(u => u.UserName == user.UserName);
					var addUserToRoleResult = await _userProfileService.AddToRoleAsync(
						createdUser, UserProfileRole.Teacher, _userManager
					);
					if (addUserToRoleResult.Succeeded)
					{
						return RedirectToAction("Index");	
					}
					
					AddErrorsFromResult(addUserToRoleResult);
				}
				
				AddErrorsFromResult(result);
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Password confirmation failed");
			}
			
			return View(model);
		}

		// GET: UserProfile/Login
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login()
		{
			if (User.Identity.IsAuthenticated)
			{
				return Redirect("/");
			}

			return View();
		}

		// POST: UserProfile/Login
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				var user = _userProfileService.Get(usr => usr.UserName == model.UserName);
				if (user != null)
				{
					if (user.IsApproved)
					{
						var result = await _signInManager.PasswordSignInAsync(
							user.UserName, model.Password, model.RememberMe, false
						);
						if (result.Succeeded)
						{
							return Redirect(model.ReturnUrl);
						}	
					}
					else
					{
						ModelState.AddModelError(string.Empty, "Account is not approved yet");
						return View(model);
					}
				}
			}
			ModelState.AddModelError(string.Empty, "Invalid login or password");
			return View(model);
		}

		// GET: UserProfile/Logout
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Redirect("/");
		}

		[HttpGet]
		[Authorize(Roles = UserProfileRole.Any)]
		public IActionResult ChangePassword()
		{
			var currentUser = _userManager.GetUserAsync(HttpContext.User);
			if (currentUser == null)
			{
				return NotFound();
			}

			return View(new ChangePasswordModel
			{
				Id = currentUser.Result.Id
			});
		}

		[HttpPost]
		[Authorize(Roles = UserProfileRole.Any)]
		public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
		{
			if (model.Id == null)
			{
				return NotFound();
			}

			var user = _userProfileService.GetById(model.Id.Value);
			if (user == null)
			{
				return NotFound();
			}

			var error = await _userProfileService.ChangePassword(
				user, model.OldPassword, model.NewPassword, model.NewPasswordRepeat, _userManager
			);

			if (error == null)
			{
				return Redirect("/");
			}
			
			ModelState.AddModelError(string.Empty, error);
			return View(model);
		}

		private void AddErrorsFromResult(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
		}
	}
}
