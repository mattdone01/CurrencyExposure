using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
		[Required]
		public string Title { get; set; }
		[Required]
		public string Article { get; set; }
		[Required]
		public string BlogSummary { get; set; }
		public string Tag { get; set; }
		public string ExternalUrl { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		[Required]
		public virtual BlogCategory BlogCategory { get; set; }
		[Required]
		public virtual BlogAuthor BlogAuthor { get; set; }
		public ICollection<BlogComment> BlogComments { get; set; }
		public ICollection<BlogSocialLink> BlogSocialLinks { get; set; }

		[NotMapped]
		public string BlogUrl
		{
			get { return "http://www.currencyexposure.com/getblog/" + Id.ToString(); }
		}
	}
}
