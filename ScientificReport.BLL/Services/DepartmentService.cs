using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Repositories;
using ScientificReport.DTO.Models.Department;

namespace ScientificReport.BLL.Services
{
	public class DepartmentService : IDepartmentService
	{
		private readonly DepartmentRepository _departmentRepository;
		private readonly UserProfileRepository _userProfileRepository;

		public DepartmentService(ScientificReportDbContext context)
		{
			_departmentRepository = new DepartmentRepository(context);
			_userProfileRepository = new UserProfileRepository(context);
		}
		
		public virtual int GetCount()
		{
			return _departmentRepository.All().Count();
		}

		public virtual IEnumerable<Department> GetPage(int page, int count)
		{
			return _departmentRepository.All().Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual IEnumerable<Department> GetAll()
		{
			return _departmentRepository.All();
		}

		public virtual IEnumerable<Department> Filter(DepartmentIndexModel model)
		{
			IEnumerable<Department> departments;
			if (model.TitleFilter != null)
			{
				departments = GetPage(model.CurrentPage, model.PageSize).Where(d => d.Title.ToLower().Contains(model.TitleFilter.Trim().ToLower()));
			}
			else if (model.SortBy != null)
			{
				departments = SortDepartmentsBy(model.SortBy.Value, model.CurrentPage, model.PageSize);
			}
			else
			{
				departments = GetPage(model.CurrentPage, model.PageSize);
			}

			return departments;
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

		public virtual bool UserWorksInDepartment(UserProfile headOfDepartment, Guid userId)
		{
			var userDepartment = _departmentRepository.Get(d => d.Head.Id == headOfDepartment.Id);
			return userDepartment.Staff.Contains(_userProfileRepository.Get(userId));
		}

		public virtual IEnumerable<Department> SortDepartmentsBy(Department.SortByOption option, int page, int count)
		{
			var departments = GetPage(page, count);
			switch (option)
			{
				case Department.SortByOption.Title:
					departments = departments.OrderByDescending(d => d.Title);
					break;
				case Department.SortByOption.StaffCount:
					departments = departments.OrderByDescending(d => d.Staff.Count);
					break;
				case Department.SortByOption.TotalScientificWorksCount:
					departments = departments.OrderByDescending(d => d.ScientificWorks.Count);
					break;
				default:
					return new List<Department>();
			}

			return departments;
		}
	}
}
