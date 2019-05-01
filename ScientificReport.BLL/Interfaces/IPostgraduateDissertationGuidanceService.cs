using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;

namespace ScientificReport.BLL.Interfaces
{
	public interface IPostgraduateDissertationGuidanceService
	{
		IEnumerable<PostgraduateDissertationGuidance> GetAll();
		IEnumerable<PostgraduateDissertationGuidance> GetAllWhere(Func<PostgraduateDissertationGuidance, bool> predicate);
		PostgraduateDissertationGuidance GetById(Guid id);
		PostgraduateDissertationGuidance Get(Func<PostgraduateDissertationGuidance, bool> predicate);
		void CreateItem(PostgraduateDissertationGuidance postgraduatedissertationguidance);
		void UpdateItem(PostgraduateDissertationGuidance postgraduatedissertationguidance);
		void DeleteById(Guid id);
		bool Exists(Guid id);
	}
}
