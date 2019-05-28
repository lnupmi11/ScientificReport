using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using ScientificReport.DTO.Models.Membership;

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

		public virtual IEnumerable<Membership> GetPage(int page, int count)
		{
			return _membershipRepository.All().Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCount()
		{
			return _membershipRepository.All().Count();
		}

		public virtual Membership GetById(Guid id)
		{
			return _membershipRepository.Get(id);
		}

		public virtual Membership Get(Func<Membership, bool> predicate)
		{
			return _membershipRepository.Get(predicate);
		}

		public virtual void CreateItem(MembershipModel model)
		{
			_membershipRepository.Create(new Membership
			{
				MemberOf = model.MemberOf,
				MembershipInfo = model.MembershipInfo
			});
		}

		public virtual void UpdateItem(MembershipEditModel model)
		{
			var membership = GetById(model.Id);
			if (membership == null)
			{
				return;
			}
			
			membership.MemberOf = model.MemberOf;
			membership.MembershipInfo = model.MembershipInfo;
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
