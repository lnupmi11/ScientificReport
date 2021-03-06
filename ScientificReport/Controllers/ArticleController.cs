using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScientificReport.BLL.Interfaces;
using ScientificReport.Controllers.Utils;
using ScientificReport.DAL.Entities.Publications;
using ScientificReport.DAL.Roles;
using ScientificReport.DTO.Models.Article;

namespace ScientificReport.Controllers
{
	[Authorize(Roles = UserProfileRole.Any)]
	public class ArticleController : Controller
	{
		private readonly IArticleService _articleService;
		private readonly IUserProfileService _userProfileService;
		private readonly IDepartmentService _departmentService;

		public ArticleController(
			IArticleService articleService,
			IUserProfileService userProfileService,
			IDepartmentService departmentService
		)
		{
			_articleService = articleService;
			_userProfileService = userProfileService;
			_departmentService = departmentService;
		}

		// GET: Article/Details/{id}
		public IActionResult Details(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var article = _articleService.GetById(id.Value);
			if (article == null)
			{
				return NotFound();
			}

			return View(new ArticleDetailsModel
			{
				Article = article,
				Authors = _articleService.GetAuthors(article.Id)
			});
		}

		// GET: Article/Edit/{id}
		public IActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var article = _articleService.GetById(id.Value);
			if (article == null)
			{
				return NotFound();
			}

			if (!AllowUserToEditArticle(article))
			{
				return Forbid();
			}

			var model = new ArticleEditModel(article)
			{
				Users = _userProfileService.GetAll(),
				Authors = _articleService.GetAuthors(article.Id)
			};

			return View(model);
		}

		// POST: Article/Edit/{id}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Guid? id, ArticleEditModel model)
		{
			if (id == null || id != model.Id)
			{
				return NotFound();
			}

			var article = _articleService.GetById(id.Value);

			if (!AllowUserToEditArticle(article))
			{
				return Forbid();
			}
			
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			
			_articleService.UpdateItem(model.Modify(article));
			return RedirectToAction("Index", "Publication");
		}
		
		// POST: Article/AddUserToAuthors/{articleId}
		[HttpPost]
		[Authorize(Roles = UserProfileRole.HeadOfDepartmentOrAdmin)]
		public IActionResult AddUserToAuthors(Guid? id, [FromBody] ArticleUpdateAuthorsRequest request)
		{
			if (id == null)
			{
				return NotFound();
			}
			
			var user = _userProfileService.GetById(request.UserId);
			if (user == null)
			{
				return Json(ApiResponse.Fail);
			}
			
			var article = _articleService.GetById(id.Value);
			if (article == null)
			{
				return NotFound();
			}
			
			if (!AllowUserToEditArticle(article))
			{
				return Json(ApiResponse.Fail);
			}
			
			if (!_articleService.GetAuthors(article.Id).Contains(user))
			{
				_articleService.AddAuthor(article, user);
			}

			return Json(ApiResponse.Ok);
		}

		// POST: Article/RemoveUserFromAuthors/{articleId}
		[HttpPost]
		[Authorize(Roles = UserProfileRole.HeadOfDepartmentOrAdmin)]
		public IActionResult RemoveUserFromAuthors(Guid? id, [FromBody] ArticleUpdateAuthorsRequest request)
		{
			if (id == null)
			{
				return NotFound();
			}
			
			var user = _userProfileService.GetById(request.UserId);
			if (user == null)
			{
				return Json(ApiResponse.Fail);
			}
			
			var article = _articleService.GetById(id.Value);
			if (article == null)
			{
				return NotFound();
			}
			
			if (!AllowUserToEditArticle(article))
			{
				return Json(ApiResponse.Fail);
			}
			
			if (_articleService.GetAuthors(article.Id).Contains(user))
			{
				_articleService.RemoveAuthor(article, user);
			}

			return Json(ApiResponse.Ok);
		}

		// GET: Article/Delete/{id}
		public IActionResult Delete(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var article = _articleService.GetById(id.Value);
			if (article == null)
			{
				return NotFound();
			}

			if (!AllowToDeleteArticle())
			{
				return Forbid();
			}

			return View(article);
		}

		// POST: Article/Delete/{id}
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(Guid id)
		{
			var article = _articleService.GetById(id);
			if (article == null)
			{
				return NotFound();
			}

			if (!AllowToDeleteArticle())
			{
				return Forbid();
			}
			
			_articleService.DeleteById(id);
			
			return RedirectToAction("Index", "Publication");
		}

		private bool AllowUserToEditArticle(Article article)
		{
			var user = _userProfileService.Get(User);
			var department = _departmentService.Get(d => d.Staff.Contains(user));
			var isHeadOfDepartment = PageHelpers.IsHeadOfDepartment(User) && article.UserProfilesArticles.Any(p => department.Staff.Contains(p.Author));
			return PageHelpers.IsAdmin(User) || isHeadOfDepartment ||
			       article.UserProfilesArticles.Any(p => p.Author.UserName == User.Identity.Name) &&
			       article.PublishingYear == DateTime.Now.Year;
		}

		private bool AllowToDeleteArticle()
		{
			return PageHelpers.IsAdmin(User);
		}
	}
}
