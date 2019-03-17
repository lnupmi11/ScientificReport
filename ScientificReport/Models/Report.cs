using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ScientificReport.Models
{
	public class Report
	{
		public int Id { get; set; }
		public string Title { get; set; }

		[DataType(DataType.Date)]
		public DateTime Created { get; set; }
	}
}