using System;
using System.ComponentModel.DataAnnotations;
using ScientificReport.DAL.Entities.Publications;

namespace ScientificReport.DAL.Entities.UserProfile
{
	public class UserProfilesArticles
	{
		[Key]
		public Guid Id { get; set; }
		
		public Guid AuthorId { get; set; }
		public virtual UserProfile Author { get; set; }
		
		public Guid ArticleId { get; set; }
		public virtual Article Article { get; set; }
	}
}
