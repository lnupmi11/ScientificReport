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
	public class GrantService : IGrantService
	{
		private readonly GrantRepository _grantRepository;

		public GrantService(ScientificReportDbContext context)
		{
			_grantRepository = new GrantRepository(context);
		}

		public IEnumerable<Grant> GetAll()
		{
			return _grantRepository.All();
		}

		public IEnumerable<Grant> GetAllWhere(Func<Grant, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public Grant GetById(Guid id)
		{
			return _grantRepository.Get(id);
		}

		public Grant Get(Func<Grant, bool> predicate)
		{
			return _grantRepository.Get(predicate);
		}

		public void CreateItem(Grant grant)
		{
			_grantRepository.Create(grant);
		}

		public void UpdateItem(Grant grant)
		{
			_grantRepository.Update(grant);
		}

		public void DeleteById(Guid id)
		{
			_grantRepository.Delete(id);
		}

		public bool Exists(Guid id)
		{
			return _grantRepository.Get(id) != null;
		}

		public IEnumerable<UserProfile> GetUsers(Guid id)
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
