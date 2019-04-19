using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.UserProfile;

namespace ScientificReport.Controllers
{
	// [Authorize]
	public class UserProfileController : Controller
	{
		private readonly UserManager<UserProfile> _userManager;
		private readonly SignInManager<UserProfile> _signInManager;

		private readonly IUserProfileService _userProfileService;
		
		private readonly ILogger _logger;

		public UserProfileController(
			UserManager<UserProfile> usrMgr,
			SignInManager<UserProfile> signInManager,
			IUserProfileService userProfileService,
			ILogger<UserProfileController> logger
		)
		{
			_userManager = usrMgr;
			_signInManager = signInManager;
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
		public IActionResult Details(Guid id)
		{
			if (id.Equals(null))
			{
				return NotFound();
			}

			var userProfile = _userProfileService.GetById(id);
			if (userProfile == null)
			{
				return NotFound();
			}

			return View(userProfile);
		}

		// GET: UserProfile/Edit/{id}
		[HttpGet]
		public IActionResult Edit(Guid id) {
			var user = _userProfileService.GetById(id);
			if (user != null)
			{
				return View(user);
			}

			return RedirectToAction("Index");
		}

		// POST: UserProfile/Edit/{id}
		[HttpPost]
		public IActionResult Edit(UserProfile user)
		{
			if (!ModelState.IsValid)
			{
				return View(user);
			}
			_logger.LogError(user.Id.ToString());

			if (_userProfileService.UserExists(user.Id))
			{
				_userProfileService.UpdateItem(user);
			}
			return RedirectToAction("Index");
		}

		// POST: UserProfile/Delete/{id}
		[HttpPost]
		public IActionResult Delete(Guid id)
		{
			if (!_userProfileService.UserExists(id))
			{
				return NotFound();
			}
			
			_userProfileService.DeleteById(id);
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
				Position = "Teacher",
				IsApproved = false,
				PhoneNumber = model.PhoneNumber
			};
			if (model.Password.Equals(model.PasswordRepeat))
			{
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					return RedirectToAction("Index");
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
					await _signInManager.SignOutAsync();
					var result = await _signInManager.PasswordSignInAsync(
						user.UserName, model.Password, true, false
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
