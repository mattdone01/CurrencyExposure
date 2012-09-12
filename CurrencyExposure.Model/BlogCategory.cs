using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExposure.Model
{
	public class BlogCategory
	{
		public BlogCategory()
		{
			this.CreateDate = DateTime.UtcNow;
		}

		public int Id { get; set; }
		public string CategoryName { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
