namespace ScientificReport.DTO.Models.Department
{
	public class SelectItem
	{
		public string Value { get; set; }
		public string Text { get; set; }
		public bool Selected { get; set; }

		public SelectItem(string text, string value, bool selected)
		{
			Value = value;
			Text = text;
			Selected = selected;
		}

		public SelectItem(string text, string value)
		{
			Value = value;
			Text = text;
			Selected = false;
		}
	}
}