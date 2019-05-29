using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.Grant
{
	public class GrantModel
	{
		[Required]
		public string Info { get; set; }
		
	    public GrantModel()
    	{
    	}

        public GrantModel(DAL.Entities.Grant grant)
        {
	        Info = grant.Info;
        }
	}
}
