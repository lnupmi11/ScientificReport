using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.Publications;

namespace ScientificReport.DTO.Models.ScientificWorks
{
	public class ScientificWorksEdit
	{
		public ScientificWork ScientificWork { get; set; }
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Authors { get; set; }
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Users { get; set; }
	}
}
