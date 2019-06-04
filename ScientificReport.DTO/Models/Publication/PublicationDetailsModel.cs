using System.Collections.Generic;

namespace ScientificReport.DTO.Models.Publication
{
	public class PublicationDetailsModel
	{
		public DAL.Entities.Publications.Publication Publication { get; set; }
		
		public bool UserIsAuthor { get; set; }

		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Authors { get; set; }
	}
}
