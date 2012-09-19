using System;
using System.Collections.Generic;
using System.Data.Entity;
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
				if (id > 0)
				{
					return context.Blogs
						.Include("BlogComments")
						.Include("BlogCategory")
						.Include("BlogAuthor")
						.SingleOrDefault(b => b.Id == id);
				}

				return context.Blogs
					.Include("BlogComments")
					.Include("BlogCategory")
					.Include("BlogAuthor")
					.Take(1)
					.OrderByDescending(c => c.CreateDate)
					.SingleOrDefault();
			}
		}

		public List<BlogSummaryDto> GetBlogSummaries(int count = 4)
		{
			using (DataContext)
			{
				return DataContext.Blogs
					.Include("BlogAuthor")
					.OrderByDescending(s =>s.CreateDate)
					.Take(count)
					.Select(s => new BlogSummaryDto
						             {
							             Id = s.Id,
							             Title = s.Title,
							             Article = s.Article,
							             Author = s.BlogAuthor.AuthorName,
							             CreateDate = s.CreateDate
						             }).ToList();
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
