using System;

namespace ScientificReport.DTO.Models.Opposition
{
	public class OppositionEditModel : OppositionModel
	{
		public Guid Id { get; set; }

		public OppositionEditModel()
		{
		}

		public OppositionEditModel(DAL.Entities.Opposition opposition) : base(opposition)
		{
			Id = opposition.Id;
		}
	}
}
