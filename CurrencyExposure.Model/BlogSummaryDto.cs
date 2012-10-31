using System;
using System.ComponentModel.DataAnnotations.Schema;

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

		[NotMapped]
		public string BlogUrl
		{
			get { return "http://www.currencyexposure.com/getblog/" + Id.ToString(); }
		}
	}
}
