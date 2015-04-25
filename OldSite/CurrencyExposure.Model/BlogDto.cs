using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExposure.Model
{
	public class BlogDto
	{
		public int? BlogId { get; set; }
		public string Title { get; set; }
		public string Article { get; set; }
		public string BlogSummary { get; set; }
		public string Tag { get; set; }
		public int BlogCategoryId { get; set; }
		public virtual int BlogAuthorId { get; set; }
	}
}
