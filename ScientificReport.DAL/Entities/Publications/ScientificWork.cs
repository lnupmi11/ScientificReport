using System.Collections.Generic;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.DAL.Entities.Publications
{
	public class ScientificWork : PublicationBase
	{
		public string Cypher { get; set; }
		
		public string Category { get; set; }

		public string Contents { get; set; }
		
		public ICollection<UserProfilesScientificWorks> UserProfilesScientificWorks { get; set; }
	}
}
