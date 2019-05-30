using System.Collections.Generic;

namespace ScientificReport.Controllers.Utils
{
	public class PublicationSearchApiResponse : ApiResponse
	{
		 public IEnumerable<PublicationApiResponse> Publications { get; set; }
	}
}
