using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.DTO.Models;

namespace ScientificReport.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index() => View();

		public IActionResult AccessDenied() => View();

		public IActionResult SetLanguage(string culture, string returnUrl)
		{
			Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
				new CookieOptions
				{
					Expires = DateTimeOffset.UtcNow.AddYears(1),
					IsEssential = true
				}
			);

			return Redirect(returnUrl);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
		}
	}
}
