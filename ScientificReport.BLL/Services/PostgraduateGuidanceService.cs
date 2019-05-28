using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using ScientificReport.DTO.Models.PostgraduateGuidance;

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

		public virtual IEnumerable<PostgraduateGuidance> GetPage(int page, int count)
		{
			return _postgraduateGuidanceRepository.All().Skip((page - 1) * count).Take(count).ToList();
		}

		public virtual int GetCount()
		{
			return _postgraduateGuidanceRepository.All().Count();
		}

		public virtual PostgraduateGuidance GetById(Guid id)
		{
			return _postgraduateGuidanceRepository.Get(id);
		}

		public virtual PostgraduateGuidance Get(Func<PostgraduateGuidance, bool> predicate)
		{
			return _postgraduateGuidanceRepository.Get(predicate);
		}

		public virtual void CreateItem(PostgraduateGuidanceModel model)
		{
			_postgraduateGuidanceRepository.Create(new PostgraduateGuidance
			{
				Guide = model.Guide,
				PostgraduateInfo = model.PostgraduateInfo,
				PostgraduateName = model.PostgraduateName
			});
		}

		public virtual void UpdateItem(PostgraduateGuidanceEditModel model)
		{
			var postgraduateGuidance = GetById(model.Id);
			if (postgraduateGuidance == null)
			{
				return;
			}

			postgraduateGuidance.Guide = model.Guide;
			postgraduateGuidance.PostgraduateInfo = model.PostgraduateInfo;
			postgraduateGuidance.PostgraduateName = model.PostgraduateName;
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
