using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.UserProfile
{
	public class ChangePasswordModel
	{
		[Required]
		public Guid? Id { get; set; }

		[Required]
		[UIHint("password")]
		public string OldPassword { get; set; }
		
		[Required]
		[UIHint("password")]
		public string NewPassword { get; set; }
		
		[Required]
		[UIHint("password")]
		public string NewPasswordRepeat { get; set; }
	}
}
