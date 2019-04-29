using System.Collections.Generic;
using ScientificReport.DAL.Entities;

namespace ScientificReport.DTO.Models.ScientificWorks
{
	public class ScientificWorksDetails
	{
		public ScientificWork ScientificWork { get; set; }
		public ICollection<DAL.Entities.UserProfile.UserProfile> Authors { get; set; }
	}
}
