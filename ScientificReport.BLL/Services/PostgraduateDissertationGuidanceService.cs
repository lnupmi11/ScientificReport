using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;

namespace ScientificReport.BLL.Services
{
	public class PostgraduateDissertationGuidanceService : IPostgraduateDissertationGuidanceService
	{
		private readonly PostgraduateDissertationGuidanceRepository _postgraduateDissertationGuidanceRepository;

		public PostgraduateDissertationGuidanceService(ScientificReportDbContext context)
		{
			_postgraduateDissertationGuidanceRepository = new PostgraduateDissertationGuidanceRepository(context);
		}

		public virtual IEnumerable<PostgraduateDissertationGuidance> GetAll()
		{
			return _postgraduateDissertationGuidanceRepository.All();
		}

		public virtual IEnumerable<PostgraduateDissertationGuidance> GetAllWhere(Func<PostgraduateDissertationGuidance, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public virtual PostgraduateDissertationGuidance GetById(Guid id)
		{
			return _postgraduateDissertationGuidanceRepository.Get(id);
		}

		public virtual PostgraduateDissertationGuidance Get(Func<PostgraduateDissertationGuidance, bool> predicate)
		{
			return _postgraduateDissertationGuidanceRepository.Get(predicate);
		}

		public virtual void CreateItem(PostgraduateDissertationGuidance postgraduateDissertationGuidance)
		{
			_postgraduateDissertationGuidanceRepository.Create(postgraduateDissertationGuidance);
		}

		public virtual void UpdateItem(PostgraduateDissertationGuidance postgraduateDissertationGuidance)
		{
			_postgraduateDissertationGuidanceRepository.Update(postgraduateDissertationGuidance);
		}

		public virtual void DeleteById(Guid id)
		{
			_postgraduateDissertationGuidanceRepository.Delete(id);
		}

		public virtual bool Exists(Guid id)
		{
			return _postgraduateDissertationGuidanceRepository.Get(id) != null;
		}
	}
}
