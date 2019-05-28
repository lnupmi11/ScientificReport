using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Repositories;
using ScientificReport.DTO.Models.Grant;

namespace ScientificReport.BLL.Services
{
	public class GrantService : IGrantService
	{
		private readonly GrantRepository _grantRepository;

		public GrantService(ScientificReportDbContext context)
		{
			_grantRepository = new GrantRepository(context);
		}

		public virtual IEnumerable<Grant> GetAll()
		{
			return _grantRepository.All();
		}

		public virtual IEnumerable<Grant> GetAllWhere(Func<Grant, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual IEnumerable<Grant> GetPage(int page, int count)
		{
			return _grantRepository.All().Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCount()
		{
			return _grantRepository.All().Count();
		}

		public virtual Grant GetById(Guid id)
		{
			return _grantRepository.Get(id);
		}

		public virtual Grant Get(Func<Grant, bool> predicate)
		{
			return _grantRepository.Get(predicate);
		}

		public virtual void CreateItem(GrantModel model)
		{
			_grantRepository.Create(new Grant());
		}

		public virtual void UpdateItem(GrantEditModel model)
		{
			var grant = GetById(model.Id);
			if (grant == null)
			{
				return;
			}
			
			_grantRepository.Update(grant);
		}

		public virtual void DeleteById(Guid id)
		{
			_grantRepository.Delete(id);
		}

		public virtual bool Exists(Guid id)
		{
			return _grantRepository.Get(id) != null;
		}

		public virtual IEnumerable<UserProfile> GetUsers(Guid id)
		{
			var grant = _grantRepository.Get(id);
			IEnumerable<UserProfile> users = null;
			if (grant != null)
			{
				users = grant.UserProfilesGrants.Select(u => u.UserProfile);
			}

			return users;
		}
	}
}
