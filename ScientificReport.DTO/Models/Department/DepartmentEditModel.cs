using System.Collections.Generic;

namespace ScientificReport.DTO.Models.Department
{
	public class DepartmentEditModel
	{
		public DAL.Entities.Department Department { get; set; }
		
		public DAL.Entities.UserProfile Head { get; set; }
		
		public IEnumerable<DAL.Entities.UserProfile> Users { get; set; }
		
		public IEnumerable<DAL.Entities.ScientificWork> ScientificWorks { get; set; }
		
		public bool IsEditingByHead { get; set; }
	}
}
