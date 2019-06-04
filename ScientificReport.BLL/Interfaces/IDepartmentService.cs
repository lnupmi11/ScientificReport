using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.Publications;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DTO.Models.Department;

namespace ScientificReport.BLL.Interfaces
{
	public interface IDepartmentService
	{
		int GetCount();
		IEnumerable<Department> GetPage(int page, int count);
		IEnumerable<Department> GetAll();
		IEnumerable<Department> Filter(DepartmentIndexModel model);
		IEnumerable<Department> GetAllWhere(Func<Department, bool> predicate);
		Department GetById(Guid id);
		Department Get(Func<Department, bool> predicate);
		void CreateItem(Department department);
		void UpdateItem(Department department);
		void DeleteById(Guid id);
		bool Exists(Guid id);
		void AddScientificWork(Guid id, ScientificWork scientificWork);
		void RemoveScientificWork(Guid id, ScientificWork scientificWork);
		void AddUser(Guid id, UserProfile user);
		void RemoveUser(Guid id, UserProfile user);
		bool UserIsHired(UserProfile user);
		bool UserWorksInDepartment(UserProfile headOfDepartment, Guid userId);
		IEnumerable<Department> SortDepartmentsBy(Department.SortByOption option, int page, int count);
	}
}
