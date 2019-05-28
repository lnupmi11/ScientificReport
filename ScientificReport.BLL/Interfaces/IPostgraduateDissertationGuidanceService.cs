using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.PostgraduateDissertationGuidance;

namespace ScientificReport.BLL.Interfaces
{
	public interface IPostgraduateDissertationGuidanceService
	{
		IEnumerable<PostgraduateDissertationGuidance> GetAll();
		IEnumerable<PostgraduateDissertationGuidance> GetAllWhere(Func<PostgraduateDissertationGuidance, bool> predicate);
		IEnumerable<PostgraduateDissertationGuidance> GetPage(int page, int count);
		int GetCount();
		PostgraduateDissertationGuidance GetById(Guid id);
		PostgraduateDissertationGuidance Get(Func<PostgraduateDissertationGuidance, bool> predicate);
		void CreateItem(PostgraduateDissertationGuidanceModel model);
		void UpdateItem(PostgraduateDissertationGuidanceEditModel model);
		void DeleteById(Guid id);
		bool Exists(Guid id);
	}
}
