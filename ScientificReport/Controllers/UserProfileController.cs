using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
		
		private readonly ILogger _logger;

		public UserProfileController(
			UserManager<UserProfile> usrMgr,
			SignInManager<UserProfile> signInManager,
			RoleManager<UserProfileRole> roleManager,
			IUserProfileService userProfileService,
			ILogger<UserProfileController> logger
		)
		{
			_userManager = usrMgr;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_userProfileService = userProfileService;
			_logger = logger;
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

			return View(userProfile);
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
					UserProfile = user,
					AllRoles = _roleManager.Roles.ToList(),
					UserRoles = await _userManager.GetRolesAsync(user)
				});
			}

			return RedirectToAction("Index");
		}

		// POST: UserProfile/Edit/{id}
		[HttpPost]
		public IActionResult Edit(UserProfileEditModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_logger.LogError(model.UserProfile.Id.ToString());

			if (_userProfileService.UserExists(model.UserProfile.Id))
			{
				_userProfileService.UpdateItem(model.UserProfile);
			}
			else
			{
				return NotFound();
			}
			
			return RedirectToAction("Index");
		}

		// POST: UserProfile/AddUserToRole/{userId}
		[HttpPost]
		public async Task<IActionResult> AddUserToRole(Guid? id, [FromBody] UserProfileUpdateRolesRequest request)
		{
			if (id == null)
			{
				return NotFound();
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
			
			return Json(new
			{
				Success = userExists
			});
		}
		
		// POST: UserProfile/RemoveUserFromRole/{userId}
		[HttpPost]
		public async Task<IActionResult> RemoveUserFromRole(Guid? id, [FromBody] UserProfileUpdateRolesRequest request)
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
				Position = UserProfileRole.Teacher,
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
				if (user != null) {
					var result = await _signInManager.PasswordSignInAsync(
						user.UserName, model.Password, model.RememberMe, false
					);
					if (result.Succeeded) {
						return Redirect(model.ReturnUrl);
					}
				}
			}
			ModelState.AddModelError(string.Empty, "Invalid login or password");
			return View(model);
		}

		// GET: UserProfile/Logout
		[HttpGet]
		[Authorize(Roles = UserProfileRole.Any)]
		public async Task<IActionResult> Logout() {
			await _signInManager.SignOutAsync();
			return RedirectToAction("Login");
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
