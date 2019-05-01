using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;

namespace ScientificReport.BLL.Interfaces
{
	public interface IPostgraduateGuidanceService
	{
		IEnumerable<PostgraduateGuidance> GetAll();
		IEnumerable<PostgraduateGuidance> GetAllWhere(Func<PostgraduateGuidance, bool> predicate);
		PostgraduateGuidance GetById(Guid id);
		PostgraduateGuidance Get(Func<PostgraduateGuidance, bool> predicate);
		void CreateItem(PostgraduateGuidance postgraduateguidance);
		void UpdateItem(PostgraduateGuidance postgraduateguidance);
		void DeleteById(Guid id);
		bool Exists(Guid id);
	}
}
