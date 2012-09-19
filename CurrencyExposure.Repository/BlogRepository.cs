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

		public OperationStatus SaveNewBlog(Blog blog)
		{
			DataContext.Entry(blog).State = System.Data.EntityState.Modified;
			try
			{
				DataContext.SaveChanges();
			}
			catch (Exception ex)
			{
				return OperationStatus.CreateFromException("Failed to Save Blog.", ex);
			}
			return new OperationStatus { Status = true };
		}
	}
}
