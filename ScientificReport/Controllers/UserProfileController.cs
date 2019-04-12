using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Interfaces;
using ScientificReport.Models.ViewModels;

namespace ScientificReport.Controllers
{
	// [Authorize]
	public class UserProfileController : Controller
	{
		private readonly UserManager<UserProfile> _userManager;
		private readonly SignInManager<UserProfile> _signInManager;

		private readonly IRepository<UserProfile, string> _userProfileRepository;
		
		private readonly ILogger _logger;

		public UserProfileController(
			UserManager<UserProfile> usrMgr,
			SignInManager<UserProfile> signInManager,
			IRepository<UserProfile, string> userProfileRepository,
			ILogger<UserProfileController> logger
		)
		{
			_userManager = usrMgr;
			_signInManager = signInManager;
			_userProfileRepository = userProfileRepository;
			_logger = logger;
		}

		// GET: UserProfile/Index
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _userManager.Users.ToListAsync());
		}

		// GET: UserProfile/Details/{id}
		[HttpGet]
		public IActionResult Details(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userProfile = _userProfileRepository.Get(id);
			if (userProfile == null)
			{
				return NotFound();
			}

			return View(userProfile);
		}

		// GET: UserProfile/Edit/{id}
		[HttpGet]
		public IActionResult Edit(string id) {
			var user = _userProfileRepository.Get(id);
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
			_logger.LogError(user.Id);
			
			_userProfileRepository.Update(user);
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
				ModelState.AddModelError(string.Empty, "User Not Found");
			}

			return View("Index", _userManager.Users);
		}
		
		// GET: UserProfile/Register
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register()
		{
			if (User.Identity.IsAuthenticated)
			{
				return Redirect("/UserProfile");
			}
			return View();
		}
		
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
			if (ModelState.IsValid) {
				var user = await _userManager.FindByNameAsync(model.UserName);
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
			return Redirect("/UserProfile/Login");
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
