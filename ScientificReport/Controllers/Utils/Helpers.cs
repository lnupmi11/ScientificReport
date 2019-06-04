using System;
using System.Linq;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.Publications;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.Controllers.Utils
{
	public class Helpers
	{
		public static bool CheckForPersonalPublication(PublicationBase publication, UserProfile user)
		{
			var result = false;
			var publicationType = publication.GetType();
			if (publicationType == typeof(Publication))
			{
				result = ((Publication) publication).UserProfilesPublications.Any(upp => upp.UserProfile.Id == user.Id);
			}
			else if (publicationType == typeof(Article))
			{
				result = ((Article) publication).UserProfilesArticles.Any(upp => upp.Author.Id == user.Id);
			}
			else if (publicationType == typeof(ScientificWork))
			{
				result = ((ScientificWork) publication).UserProfilesScientificWorks.Any(upp => upp.UserProfile.Id == user.Id);
			}

			return result;
		}
		
		public static bool CheckForDepartmentPublication(PublicationBase publication, Department department)
		{
			var result = false;
			var publicationType = publication.GetType();
			if (publicationType == typeof(Publication))
			{
				result = ((Publication) publication).UserProfilesPublications.Any(up => department.Staff.Contains(up.UserProfile));
			}
			else if (publicationType == typeof(Article))
			{
				result = ((Article) publication).UserProfilesArticles.Any(up => department.Staff.Contains(up.Author));
			}
			else if (publicationType == typeof(ScientificWork))
			{
				result = ((ScientificWork) publication).UserProfilesScientificWorks.Any(up => department.Staff.Contains(up.UserProfile));
			}

			return result;
		}
		
		public static bool CheckForPublication(PublicationBase publication, Func<UserProfile, bool> predicate)
		{
			var result = false;
			var publicationType = publication.GetType();
			if (publicationType == typeof(Publication))
			{
				result = ((Publication) publication).UserProfilesPublications.Any(up => predicate(up.UserProfile));
			}
			else if (publicationType == typeof(Article))
			{
				result = ((Article) publication).UserProfilesArticles.Any(up => predicate(up.Author));
			}
			else if (publicationType == typeof(ScientificWork))
			{
				result = ((ScientificWork) publication).UserProfilesScientificWorks.Any(up => predicate(up.UserProfile));
			}

			return result;
		}
	}
}
