using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Interfaces
{
	public interface ITrackable
	{
		[DataType(DataType.Date)]
		DateTime Created { get; set; }

		[DataType(DataType.Date)]
		DateTime Edited { get; set; }
	}
}