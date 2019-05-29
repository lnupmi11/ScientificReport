using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ScientificReport.BLL.Interfaces;
using ScientificReport.BLL.Utils;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using ScientificReport.DTO.Models.Opposition;

namespace ScientificReport.BLL.Services
{
	public class OppositionService : IOppositionService
	{
		private readonly OppositionRepository _oppositionRepository;
		private readonly DepartmentRepository _departmentRepository;
		private readonly UserProfileRepository _userProfileRepository;

		public OppositionService(ScientificReportDbContext context)
		{
			_oppositionRepository = new OppositionRepository(context);
			_departmentRepository = new DepartmentRepository(context);
			_userProfileRepository = new UserProfileRepository(context);
		}

		public virtual IEnumerable<Opposition> GetAll()
		{
			return _oppositionRepository.All();
		}

		public virtual IEnumerable<Opposition> GetAllWhere(Func<Opposition, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual IEnumerable<Opposition> GetItemsByRole(ClaimsPrincipal userClaims)
		{
			IEnumerable<Opposition> items;
			if (UserHelpers.IsAdmin(userClaims))
			{
				items = _oppositionRepository.All();
			}
			else if (UserHelpers.IsHeadOfDepartment(userClaims))
			{
				var department = _departmentRepository.Get(r => r.Head.UserName == userClaims.Identity.Name);
				items = _oppositionRepository.AllWhere(m => department.Staff.Contains(m.Opponent));
			}
			else
			{
				var user = _userProfileRepository.Get(u => u.UserName == userClaims.Identity.Name);
				items = _oppositionRepository.AllWhere(m => m.Opponent.Id == user.Id);
			}

			return items;
		}

		public virtual IEnumerable<Opposition> GetPageByRole(int page, int count, ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCountByRole(ClaimsPrincipal userClaims)
		{
			return GetItemsByRole(userClaims).Count();
		}

		public virtual Opposition GetById(Guid id)
		{
			return _oppositionRepository.Get(id);
		}

		public virtual Opposition Get(Func<Opposition, bool> predicate)
		{
			return _oppositionRepository.Get(predicate);
		}

		public virtual void CreateItem(OppositionModel model)
		{
			_oppositionRepository.Create(new Opposition
			{
				About = model.About,
				Opponent = model.Opponent,
				DateOfOpposition = model.DateOfOpposition
			});
		}

		public virtual void UpdateItem(OppositionEditModel model)
		{
			var opposition = GetById(model.Id);
			if (opposition == null)
			{
				return;
			}
			
			opposition.About = model.About;
			opposition.Opponent = model.Opponent;
			opposition.DateOfOpposition = model.DateOfOpposition;
			_oppositionRepository.Update(opposition);
		}

		public virtual void DeleteById(Guid id)
		{
			_oppositionRepository.Delete(id);
		}

		public virtual bool Exists(Guid id)
		{
			return _oppositionRepository.Get(id) != null;
		}
	}
}
