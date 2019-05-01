using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;

namespace ScientificReport.BLL.Services
{
	public class MembershipService : IMembershipService
	{
		private readonly MembershipRepository _membershipRepository;

		public MembershipService(ScientificReportDbContext context)
		{
			_membershipRepository = new MembershipRepository(context);
		}

		public virtual IEnumerable<Membership> GetAll()
		{
			return _membershipRepository.All();
		}

		public virtual IEnumerable<Membership> GetAllWhere(Func<Membership, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual Membership GetById(Guid id)
		{
			return _membershipRepository.Get(id);
		}

		public virtual Membership Get(Func<Membership, bool> predicate)
		{
			return _membershipRepository.Get(predicate);
		}

		public virtual void CreateItem(Membership membership)
		{
			_membershipRepository.Create(membership);
		}

		public virtual void UpdateItem(Membership membership)
		{
			_membershipRepository.Update(membership);
		}

		public virtual void DeleteById(Guid id)
		{
			_membershipRepository.Delete(id);
		}

		public virtual bool Exists(Guid id)
		{
			return _membershipRepository.Get(id) != null;
		}
	}
}
