using System;
using Microsoft.AspNetCore.Identity;

namespace ScientificReport.DAL.Roles
{
	public sealed class UserProfileRole : IdentityRole<Guid>
	{
		public const string Teacher = "Teacher";
		public const string HeadOfDepartment = "HeadOfDepartment";
		public const string Administrator = "Administrator";

		// Allow all roles except anonymous users
		public const string Any = Teacher + "," + HeadOfDepartment + "," + Administrator;

		public static readonly string[] Roles =
		{
			Teacher, HeadOfDepartment, Administrator
		};

		public UserProfileRole(string name)
		{
			Name = name;
		}
	}
}
