using System;
using System.ComponentModel.DataAnnotations;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Entities.Reports
{
	public abstract class Report : ITrackable
	{
		[Key]
		virtual public Guid Id { get; set; }

		virtual public DateTime Created { get; set; }
		virtual public DateTime Edited { get; set; }
	}
}
