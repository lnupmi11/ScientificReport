using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ScientificReport.DTO.Models.UserProfile
{
	[SuppressMessage("ReSharper", "Mvc.TemplateNotResolved")]
	public class LoginModel
	{
		[Required]
		public string UserName { get; set; }
		
		[Required]
		[UIHint("password")]
		public string Password { get; set; }
		
		public bool RememberMe { get; set; }
		
		public string ReturnUrl { get; set; } = "/";
	}
}
