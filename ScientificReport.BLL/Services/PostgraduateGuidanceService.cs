using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;

namespace ScientificReport.BLL.Services
{
	public class PostgraduateGuidanceService : IPostgraduateGuidanceService
	{
		private readonly PostgraduateGuidanceRepository _postgraduateGuidanceRepository;

		public PostgraduateGuidanceService(ScientificReportDbContext context)
		{
			_postgraduateGuidanceRepository = new PostgraduateGuidanceRepository(context);
		}

		public virtual IEnumerable<PostgraduateGuidance> GetAll()
		{
			return _postgraduateGuidanceRepository.All();
		}

		public virtual IEnumerable<PostgraduateGuidance> GetAllWhere(Func<PostgraduateGuidance, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual PostgraduateGuidance GetById(Guid id)
		{
			return _postgraduateGuidanceRepository.Get(id);
		}

		public virtual PostgraduateGuidance Get(Func<PostgraduateGuidance, bool> predicate)
		{
			return _postgraduateGuidanceRepository.Get(predicate);
		}

		public virtual void CreateItem(PostgraduateGuidance postgraduateGuidance)
		{
			_postgraduateGuidanceRepository.Create(postgraduateGuidance);
		}

		public virtual void UpdateItem(PostgraduateGuidance postgraduateGuidance)
		{
			_postgraduateGuidanceRepository.Update(postgraduateGuidance);
		}

		public virtual void DeleteById(Guid id)
		{
			_postgraduateGuidanceRepository.Delete(id);
		}

		public virtual bool Exists(Guid id)
		{
			return _postgraduateGuidanceRepository.Get(id) != null;
		}
	}
}
