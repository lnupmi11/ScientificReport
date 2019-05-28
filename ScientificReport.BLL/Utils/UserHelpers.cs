using System.Security.Claims;
using ScientificReport.DAL.Roles;

namespace ScientificReport.BLL.Utils
{
	public class UserHelpers
	{
		public static bool IsAdmin(ClaimsPrincipal user)
		{
			return user.IsInRole(UserProfileRole.Administrator);
		}
		
		public static bool IsHeadOfDepartment(ClaimsPrincipal user)
		{
			return user.IsInRole(UserProfileRole.HeadOfDepartment);
		}
		
		public static bool IsAdminOrHead(ClaimsPrincipal user)
		{
			return IsAdmin(user) || IsHeadOfDepartment(user);
		}

		public static bool IsTeacher(ClaimsPrincipal user)
		{
			return user.IsInRole(UserProfileRole.Teacher);
		}
	}
}
