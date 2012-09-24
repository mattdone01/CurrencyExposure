using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExposure.Model
{
	public class Blog
	{
		public Blog()
		{
			this.CreateDate = DateTime.UtcNow;
			BlogComments = new HashSet<BlogComment>();
			BlogSocialLinks = new HashSet<BlogSocialLink>();
		}
		
		public int Id { get; set; }
		public string Title { get; set; }
		public string Article { get; set; }
		public string BlogSummary { get; set; }
		public string Tag { get; set; }
		public string ExternalUrl { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime? UpdateDate { get; set; }

		public virtual BlogCategory BlogCategory { get; set; }
		public virtual BlogAuthor BlogAuthor { get; set; }
		public ICollection<BlogComment> BlogComments { get; set; }
		public ICollection<BlogSocialLink> BlogSocialLinks { get; set; }
	}
}
