using System;

namespace CurrencyExposure.Model
{
	public class BlogSummaryDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Summary { get; set; }
		public string Author { get; set; }
		public string Category { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
