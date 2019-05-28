using System.Collections.Generic;

namespace ScientificReport.DTO.Models.Department
{
	public class DepartmentIndexModel : PageModel
	{
		public string TitleFilter { get; set; }
		
		public DAL.Entities.Department.SortByOption? SortBy { get; set; }
		
		public IEnumerable<ScientificReport.DAL.Entities.Department> Departments { get; set; }
		
		public DepartmentIndexModel() {}

		public DepartmentIndexModel(IEnumerable<DAL.Entities.Department> departments)
		{
			Departments = departments;
		}
	}
}
