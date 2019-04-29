using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class PostgraduateGuidance
	{
		[Key]
		public Guid Id { get; set; }
		
		public virtual UserProfile.UserProfile Guide { get; set; }

		public string PostgraduateName { get; set; }
		
		public string PostgraduateInfo { get; set; }
	}
}
