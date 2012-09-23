using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CurrencyExposure.Model
{
	public class BlogComment
	{
		public BlogComment()
		{
			this.CreateDate = DateTime.UtcNow;
			this.Email = string.Empty;
		}

		public int Id { get; set; }
		public string Title { get; set; }
		public string Comment { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		[ScriptIgnore]
		public Blog Blog { get; set; }

		//Parent Child
		public int? ParentCommentId { get; set; }
		[ScriptIgnore]
		public virtual BlogComment ParentComment { get; set; }
		public virtual ICollection<BlogComment> ChildComments { get; set; }
	}
}
