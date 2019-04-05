using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScientificReport.Models;
using ScientificReport.Models.ViewModels;

namespace ScientificReport.Controllers
{
	//[Authorize]
	public class UserProfileController : Controller
	{
		private readonly UserManager<UserProfile> _userManager;
		private readonly SignInManager<UserProfile> _signInManager;
		private readonly IUserValidator<UserProfile> _userValidator;
		private readonly IPasswordValidator<UserProfile> _passwordValidator;
		private readonly IPasswordHasher<UserProfile> _passwordHasher;

		public UserProfileController(UserManager<UserProfile> usrMgr,
			IUserValidator<UserProfile> userValid,
			IPasswordValidator<UserProfile> passValid,
			IPasswordHasher<UserProfile> passwordHash,
			SignInManager<UserProfile> signInManager)
		{
			_userManager = usrMgr;
			_userValidator = userValid;
			_passwordValidator = passValid;
			_passwordHasher = passwordHash;
			_signInManager = signInManager;
		}

		// GET: UserProfile/Index
		public async Task<IActionResult> Index()
		{
			var items = await _userManager.Users.ToListAsync();
			return View(items);
		}

		// GET: UserProfile/Details/{id}
		public async Task<IActionResult> Details(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userProfile = await _userManager.Users.FirstOrDefaultAsync(m => m.Id == id);
			if (userProfile == null)
			{
				return NotFound();
			}

			return View(userProfile);
		}

		// GET: UserProfile/Edit/{id}
		public async Task<IActionResult> Edit(string id) {
			var user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				return View(user);
			}

			return RedirectToAction("Index");
		}

		// POST: UserProfile/Edit/{id}
		[HttpPost]
		public async Task<IActionResult> Edit(UserProfile user)
		{
			if (!ModelState.IsValid)
			{
				return View(user);
			}

			// TODO: save user with IUserProfileRepository
			
			Console.Out.WriteLine(user.UserName);
			return RedirectToAction("Index");
		}

		// POST: UserProfile/Delete/{id}
		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				var result = await _userManager.DeleteAsync(user);
				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}

				AddErrorsFromResult(result);
			}
			else
			{
				ModelState.AddModelError("", "User Not Found");
			}

			return View("Index", _userManager.Users);
		}
		
		// GET: UserProfile/Register
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
				ModelState.AddModelError("", "Password confirmation failed");
			}
			
			return View(model);
		}

		// GET: UserProfile/Login
		[AllowAnonymous]
		public IActionResult Login() => View();

		// POST: UserProfile/Login
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginModel model)
		{
			if (ModelState.IsValid) {
				var user = await _userManager.FindByNameAsync(model.UserName);
				if (user != null) {
					await _signInManager.SignOutAsync();
					if ((await _signInManager.PasswordSignInAsync(
							user, model.Password, false, false
						)).Succeeded) {
						return Redirect(model.ReturnUrl);
					}
				}
			}
			ModelState.AddModelError("", "Invalid login or password");
			return View(model);
		}

		// GET: UserProfile/Logout
		public async Task<IActionResult> Logout() {
			await _signInManager.SignOutAsync();
			return Redirect("/UserProfile/Login");
		}
		
		private void AddErrorsFromResult(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error.Description);
			}
		}
	}
}
