using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.Grant
{
	public class GrantEditModel : GrantModel
	{
	    [Required]
		public Guid Id { get; set; }

		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Users { get; set; }
		
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Authors { get; set; }

		public GrantEditModel()
        {
        }

        public GrantEditModel(DAL.Entities.Grant grant) : base(grant)
        {
        	Id = grant.Id;
        }
	}
}
