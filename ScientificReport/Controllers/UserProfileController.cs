using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ScientificReport.Models;
using ScientificReport.Forms;

namespace ScientificReport.Controllers
{
	public class UserProfileController : Controller
	{
		private UserManager<UserProfile> userManager;
		private IUserValidator<UserProfile> userValidator;
		private IPasswordValidator<UserProfile> passwordValidator;
		private IPasswordHasher<UserProfile> passwordHasher;

		public UserProfileController(UserManager<UserProfile> usrMgr,
			IUserValidator<UserProfile> userValid,
			IPasswordValidator<UserProfile> passValid,
			IPasswordHasher<UserProfile> passwordHash)
		{
			userManager = usrMgr;
			userValidator = userValid;
			passwordValidator = passValid;
			passwordHasher = passwordHash;
		}

		// GET: UserProfile/Index
		public async Task<IActionResult> Index()
		{
			var items = await userManager.Users.ToListAsync();
			if (!items.Any()) return View(items);
			
			// debug stuff
			var output = JsonConvert.SerializeObject(items.First());
			Console.WriteLine();
			Console.WriteLine(output);
			Console.WriteLine();
			var users = await userManager.Users.ToListAsync();
			output = JsonConvert.SerializeObject(users);
			Console.WriteLine();
			Console.WriteLine(output);
			Console.WriteLine();
			return View(items);
		}

		// GET: UserProfile/Details/{id}
		public async Task<IActionResult> Details(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var userProfile = await userManager.Users.FirstOrDefaultAsync(m => m.Id == id);
			if (userProfile == null)
			{
				return NotFound();
			}

			return View(userProfile);
		}

		// GET: UserProfile/Register
		public IActionResult Register() => View();
		
		// POST: UserProfile/Register
		[HttpPost]
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
			var result = await userManager.CreateAsync(user, model.Password);
			if (result.Succeeded)
			{
				return RedirectToAction("Index");
			}

			AddErrorsFromResult(result);
			return View(model);
		}

		// GET: UserProfile/Edit/{id}
		public async Task<IActionResult> Edit(string id) {
			var user = await userManager.FindByIdAsync(id);
			if (user != null)
			{
				return View(user);
			}

			return RedirectToAction("Index");
		}

		// POST: UserProfile/Edit/{id}
		[HttpPost]
		public async Task<IActionResult> Edit(string id, string email, string password)
		{
			var user = await userManager.FindByIdAsync(id);
			if (user != null)
			{
				user.Email = email;
				var validEmail = await userValidator.ValidateAsync(userManager, user);
				if (!validEmail.Succeeded)
				{
					AddErrorsFromResult(validEmail);
				}

				IdentityResult validPass = null;
				if (!string.IsNullOrEmpty(password))
				{
					validPass = await passwordValidator.ValidateAsync(userManager,
						user, password);
					if (validPass.Succeeded)
					{
						user.PasswordHash = passwordHasher.HashPassword(user,
							password);
					}
					else
					{
						AddErrorsFromResult(validPass);
					}
				}

				if (
					(!validEmail.Succeeded || validPass != null) &&
					(!validEmail.Succeeded || password == string.Empty || !validPass.Succeeded)
				)
				{
					return View(user);
				}
				var result = await userManager.UpdateAsync(user);
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

			return View(user);
		}

		// POST: UserProfile/Delete/{id}
		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			var user = await userManager.FindByIdAsync(id);
			if (user != null)
			{
				var result = await userManager.DeleteAsync(user);
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

			return View("Index", userManager.Users);
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
