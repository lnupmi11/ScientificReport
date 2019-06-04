using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ScientificReport.DAL.Entities.Publications;

namespace ScientificReport.DAL.Entities
{
	public class Department
	{
		public enum SortByOption
		{
			Title, StaffCount, TotalScientificWorksCount
		}
		
		[Key]
		public Guid Id { get; set; }
		
		public string Title { get; set; } 
 
		public virtual UserProfile.UserProfile Head { get; set; }
		
		public virtual ICollection<ScientificWork> ScientificWorks { get; set; }
		
		public virtual ICollection<UserProfile.UserProfile> Staff { get; set; }
	}
}
