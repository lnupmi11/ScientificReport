using System.Collections.Generic;

namespace ScientificReport.DTO.Models.Article
{
	public class ArticleIndexModel : PageModel
	{
		public IEnumerable<DAL.Entities.Article> Articles { get; set; }
	}
}
