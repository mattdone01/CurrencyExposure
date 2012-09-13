using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyExposure.Model;

namespace CurrencyExposure.Repository
{
	public class BlogRepository : RepositoryBase<CurrencyExposureContext>, IBlogRepository
	{
		public Blog GetBlog(int id)
		{
			using (var context = DataContext)
			{
				return context.Blogs
					.Include("BlogComments")
					.Include("BlogCategory")
					.Include("BlogAuthor")
					.SingleOrDefault(b => b.Id == id);
			}
		}
	}
}
