namespace ScientificReport.Controllers.Utils
{
	public class ApiResponse
	{
		public bool Success { get; set; }

		public static ApiResponse Ok =>
			new ApiResponse
			{
				Success = true
			};

		public static ApiResponse Fail =>
			new ApiResponse
			{
				Success = false
			};
	}
}