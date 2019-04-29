using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class UserProfilesArticles
	{
		[Key]
		public Guid Id { get; set; }
		
		public string AuthorId { get; set; }
		public virtual UserProfile Author { get; set; }
		
		public int ArticleId { get; set; }
		public virtual Article Article { get; set; }
	}
}
