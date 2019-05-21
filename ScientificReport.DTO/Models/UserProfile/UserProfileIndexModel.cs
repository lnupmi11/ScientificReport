using System;
using System.Collections.Generic;

namespace ScientificReport.DTO.Models.UserProfile
{
	public class UserProfileIndexModel : PageModel
	{
		public enum IsApprovedOption
		{
			All, Yes, No
		}

		public IsApprovedOption? IsApproved { get; set; }
		public IEnumerable<string> IsApprovedOptions { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Guid? DepartmentId { get; set; }
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> UserProfiles { get; set; }
		public IEnumerable<DAL.Entities.Department> Departments { get; set; }

		public UserProfileIndexModel()
		{
			Init();
		}

		public UserProfileIndexModel(IEnumerable<DAL.Entities.UserProfile.UserProfile> userProfiles, IEnumerable<DAL.Entities.Department> departments)
		{
			Init();
			Departments = departments;
			UserProfiles = userProfiles;
		}

		private void Init()
		{
			IsApprovedOptions = new[]
			{
				"All", "Yes", "No"
			};
		}
	}
}
