using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CurrencyExposure.Model;
using HtmlAgilityPack;

namespace CurrencyExposure.Repository
{
	public class BlogRepository : RepositoryBase<CurrencyExposureContext>, IBlogRepository
	{
		public Blog GetBlog(int id)
		{
			using (var context = new CurrencyExposureContext())
			{
				if (id > 0)
				{
					return context.Blogs
						.Include(t => t.BlogComments)
						.Include(t => t.BlogCategory)
						.Include(t => t.BlogAuthor)
						.SingleOrDefault(b => b.Id == id);
				}

				//Get latest blog.
				return context.Blogs
					.Include(t => t.BlogComments)
					.Include(t => t.BlogCategory)
					.Include(t => t.BlogAuthor)
					.Take(1)
					.OrderByDescending(c => c.CreateDate)
					.SingleOrDefault();
			}
		}

		public List<BlogSummaryDto> GetBlogSummaries(int count = 4)
		{
			using (var context = new CurrencyExposureContext())
			{
				return context.Blogs
					.Include(t => t.BlogAuthor)
					.OrderByDescending(s =>s.CreateDate)
					.Take(count)
					.Select(s => new BlogSummaryDto
						             {
							             Id = s.Id,
							             Title = s.Title,
							             Article = s.Article, //Probably need touse a blog summary
							             Author = s.BlogAuthor.AuthorName,
							             CreateDate = s.CreateDate
						             }).ToList();
			}
		}

		public Task<List<CommentsListDto>> GetCommentsList(int count = 5)
		{
			var taskCompletionSource = new TaskCompletionSource<List<CommentsListDto>>();
			using (var context = new CurrencyExposureContext())
			{
				var result = context.BlogComments
					.Include(t => t.Blog)
					.OrderByDescending(s => s.CreateDate)
					.Take(count)
					.Select(s => new CommentsListDto
						             {
							             Title =s.Title,
							             Id = s.Id,
										 Name = s.Name,
							             BlogId = s.Blog.Id,
							             CreateDate = s.CreateDate
						             }).ToList();
				taskCompletionSource.TrySetResult(result);
				return taskCompletionSource.Task;
			}
		}

		public Task<List<BlogSummaryDto>> GetArticlesList(int count = 3)
		{
			var taskCompletionSource = new TaskCompletionSource<List<BlogSummaryDto>>();
			using (var context = new CurrencyExposureContext())
			{
				var result = context.Blogs
					.Include(t => t.BlogAuthor)
					.Include(t => t.BlogCategory)
					.OrderByDescending(s => s.CreateDate)
					.Take(count)
					.Select(s => new BlogSummaryDto
					{
						Id = s.Id,
						Title = s.Title,
						Author = s.BlogAuthor.AuthorName,
						Category = s.BlogCategory.CategoryName,
						CreateDate = s.CreateDate
					}).ToList();
				taskCompletionSource.TrySetResult(result);
				return taskCompletionSource.Task;
			}
		}

		public TransactionResult SaveComments(BlogCommentDto comment)
		{
			var blogComment = new BlogComment();
			blogComment.Name = comment.Name;
			blogComment.Title = comment.Title;
			blogComment.Email = comment.Email;
			blogComment.Comment = comment.Comment;

			int recs = 0;
			using (var context = new CurrencyExposureContext())
			{
				var blog = context.Blogs.First(c => c.Id == comment.BlogId);
				blogComment.Blog = blog;
				context.BlogComments.Add(blogComment);
				recs = context.SaveChanges();
			}
			var result = new TransactionResult(true);
			if (recs == 0)
				result.ErrorText = "Failed to save comment";

			return result;
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
	