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

		public IEnumerable<PostgraduateGuidance> GetAll()
		{
			return _postgraduateGuidanceRepository.All();
		}

		public IEnumerable<PostgraduateGuidance> GetAllWhere(Func<PostgraduateGuidance, bool> predicate)
		{
			return GetAll().Where(predicate);
		}

		public PostgraduateGuidance GetById(Guid id)
		{
			return _postgraduateGuidanceRepository.Get(id);
		}

		public PostgraduateGuidance Get(Func<PostgraduateGuidance, bool> predicate)
		{
			return _postgraduateGuidanceRepository.Get(predicate);
		}

		public void CreateItem(PostgraduateGuidance postgraduateGuidance)
		{
			_postgraduateGuidanceRepository.Create(postgraduateGuidance);
		}

		public void UpdateItem(PostgraduateGuidance postgraduateGuidance)
		{
			_postgraduateGuidanceRepository.Update(postgraduateGuidance);
		}

		public void DeleteById(Guid id)
		{
			_postgraduateGuidanceRepository.Delete(id);
		}

		public bool Exists(Guid id)
		{
			return _postgraduateGuidanceRepository.Get(id) != null;
		}
	}
}
