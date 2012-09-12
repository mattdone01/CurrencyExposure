using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExposure.Model
{
	public class BlogAuthor
	{
		public BlogAuthor()
		{
			this.CreateDate = DateTime.UtcNow;
		}

		public int Id { get; set; }
		public string AuthorName { get; set; }
		public string Email { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
