using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExposure.Model
{
	public class BlogSocialLink
	{
		public BlogSocialLink()
		{
			this.CreateDate = DateTime.UtcNow;
		}

		public int Id { get; set; }
		public string SocialType { get; set; }
		public string Uri { get; set; }
		public Blog Blog { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
