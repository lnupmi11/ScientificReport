using System;

namespace ScientificReport.DTO.Models
{
	public abstract class PageModel
	{
		public int CurrentPage { get; set; } = 1;
		public int Count { get; set; }
		public virtual int PageSize { get; set; } = 5;
		public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
		public bool ShowPrevious => CurrentPage > 1;
		public bool ShowNext => CurrentPage < TotalPages;
	}
}
