using System;
using System.ComponentModel.DataAnnotations;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Entities.Reports
{
	public class Report	: ITrackable
	{
		[Key]
		public Guid Id { get; set; }

		public DateTime Created { get; set; }
		public DateTime Edited { get; set; }
	}
}
