using System.Collections.Generic;

namespace ScientificReport.DTO.Models.Article
{
	public class ArticleDetailsModel
	{
		public DAL.Entities.Publications.Article Article { get; set; }
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Authors { get; set; }
	}
}
