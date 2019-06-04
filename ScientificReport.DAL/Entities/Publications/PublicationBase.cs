using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities.Publications
{
	public class PublicationBase
	{
		[Key]
		public virtual Guid Id { get; set; }
		
		public virtual string Title { get; set; }
		
		public virtual int PublishingYear { get; set; }
	}
}
