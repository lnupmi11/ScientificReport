using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;

namespace ScientificReport.BLL.Services
{
	public class DepartmentService : IDepartmentService
	{
		private readonly DepartmentRepository _departmentRepository;
		
		public DepartmentService(ScientificReportDbContext context)
		{
			_departmentRepository = new DepartmentRepository(context);
		}

		public IEnumerable<Department> GetAll()
		{
			return _departmentRepository.All();
		}

		public IEnumerable<Department> GetAllWhere(Func<Department, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public Department GetById(Guid id)
		{
			return _departmentRepository.Get(id);
		}

		public Department Get(Func<Department, bool> predicate)
		{
			return _departmentRepository.Get(predicate);
		}

		public void CreateItem(Department department)
		{
			_departmentRepository.Create(department);
		}

		public void UpdateItem(Department department)
		{
			_departmentRepository.Update(department);
		}

		public void DeleteById(Guid id)
		{
			_departmentRepository.Delete(id);
		}

		public bool DepartmentExists(Guid id)
		{
			return _departmentRepository.Get(id) != null;
		}

		public void AddScientificWork(Guid id, ScientificWork scientificWork)
		{
			var department = _departmentRepository.Get(id);
			if (department.ScientificWorks.Any(sw => sw.Id == scientificWork.Id))
			{
				return;
			}
			
			department.ScientificWorks.Add(scientificWork);
			_departmentRepository.Update(department);
		}

		public void RemoveScientificWork(Guid id, ScientificWork scientificWork)
		{
			var department = _departmentRepository.Get(id);
			if (department.ScientificWorks.All(sw => sw.Id != scientificWork.Id))
			{
				return;
			}
			
			department.ScientificWorks.Remove(scientificWork);
			_departmentRepository.Update(department);
		}

		public void AddUser(Guid id, UserProfile user)
		{
			var department = _departmentRepository.Get(id);
			if (department.Staff.Any(u => u.Id == user.Id))
			{
				return;
			}
			
			department.Staff.Add(user);
			_departmentRepository.Update(department);
		}

		public void RemoveUser(Guid id, UserProfile user)
		{
			var department = _departmentRepository.Get(id);
			if (department.Staff.All(u => u.Id != user.Id))
			{
				return;
			}
			
			department.Staff.Remove(user);
			_departmentRepository.Update(department);
		}
	}
}
