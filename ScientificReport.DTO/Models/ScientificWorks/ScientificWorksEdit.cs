using System.Collections.Generic;
using ScientificReport.DAL.Entities;

namespace ScientificReport.DTO.Models.ScientificWorks
{
	public class ScientificWorksEdit
	{
		public ScientificWork ScientificWork { get; set; }
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Authors { get; set; }
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Users { get; set; }
	}
}
