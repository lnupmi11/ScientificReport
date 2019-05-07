using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
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

		public virtual IEnumerable<Department> GetAll()
		{
			return _departmentRepository.All();
		}

		public virtual IEnumerable<Department> GetAllWhere(Func<Department, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual Department GetById(Guid id)
		{
			return _departmentRepository.Get(id);
		}

		public virtual Department Get(Func<Department, bool> predicate)
		{
			return _departmentRepository.Get(predicate);
		}

		public virtual void CreateItem(Department department)
		{
			_departmentRepository.Create(department);
		}

		public virtual void UpdateItem(Department department)
		{
			_departmentRepository.Update(department);
		}

		public virtual void DeleteById(Guid id)
		{
			_departmentRepository.Delete(id);
		}

		public virtual bool Exists(Guid id)
		{
			return _departmentRepository.Get(id) != null;
		}

		public virtual void AddScientificWork(Guid id, ScientificWork scientificWork)
		{
			var department = _departmentRepository.Get(id);
			if (department.ScientificWorks.Any(sw => sw.Id == scientificWork.Id))
			{
				return;
			}
			
			department.ScientificWorks.Add(scientificWork);
			_departmentRepository.Update(department);
		}

		public virtual void RemoveScientificWork(Guid id, ScientificWork scientificWork)
		{
			var department = _departmentRepository.Get(id);
			if (department.ScientificWorks.All(sw => sw.Id != scientificWork.Id))
			{
				return;
			}
			
			department.ScientificWorks.Remove(scientificWork);
			_departmentRepository.Update(department);
		}

		public virtual void AddUser(Guid id, UserProfile user)
		{
			var department = _departmentRepository.Get(id);
			if (department.Staff.Any(u => u.Id == user.Id))
			{
				return;
			}
			
			department.Staff.Add(user);
			_departmentRepository.Update(department);
		}

		public virtual void RemoveUser(Guid id, UserProfile user)
		{
			var department = _departmentRepository.Get(id);
			if (department.Staff.All(u => u.Id != user.Id))
			{
				return;
			}
			
			department.Staff.Remove(user);
			_departmentRepository.Update(department);
		}

		public virtual bool UserIsHired(UserProfile user)
		{
			return _departmentRepository.All().Any(d => d.Staff.Contains(user));
		}
	}
}