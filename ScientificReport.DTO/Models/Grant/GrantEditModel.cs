using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.Grant
{
	public class GrantEditModel : GrantModel
	{
	    [Required]
		public Guid Id { get; set; }

		public GrantEditModel()
        {
        }

        public GrantEditModel(DAL.Entities.Grant grant) : base(grant)
        {
        	Id = grant.Id;
        }
	}
}
