using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CurrencyExposure.Model;
using CurrencyExposure.Repository.Helpers;

namespace CurrencyExposure.Repository
{
	public class BlogRepository : RepositoryBase<CurrencyExposureContext>, IBlogRepository
	{
		private readonly IEmailHelper _emailHelper = null;
		private const string EmailTo = "info@currencyexposure.com";

		public BlogRepository(IEmailHelper emailHelper)
		{
			_emailHelper = emailHelper;
		}

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
							             Summary = s.BlogSummary,
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

		public OperationStatus SaveBlog(BlogDto blogDto)
		{

			var result = new OperationStatus();
			try
			{
				using (var context = new CurrencyExposureContext())
				{
					var newBlog = new Blog();
					if (blogDto.BlogId.HasValue)
						newBlog = context.Blogs.First(c => c.Id == blogDto.BlogId);

					newBlog.Title = blogDto.Title;
					newBlog.BlogSummary = blogDto.BlogSummary;
					newBlog.Article = blogDto.Article;
					newBlog.Tag = blogDto.Tag;
					newBlog.BlogAuthor = context.BlogAuthors.First(c => c.Id == blogDto.BlogAuthorId);
					newBlog.BlogCategory = context.BlogCategorys.First(c => c.Id == blogDto.BlogCategoryId);

					if (blogDto.BlogId.HasValue)
					{
						context.Blogs.Attach(newBlog);
						context.Entry(newBlog).State = EntityState.Modified;
						result.RecordsAffected = context.SaveChanges();
					}
					else
					{
						context.Entry(newBlog).State = EntityState.Added;
						result.RecordsAffected = context.SaveChanges();
					}
					result.NewId = newBlog.Id;
				}
			}
			catch (DbEntityValidationException dbEx)
			{
				string error = string.Empty;
				foreach (var validationErrors in dbEx.EntityValidationErrors)
				{
					foreach (var validationError in validationErrors.ValidationErrors)
					{
						error += string.Format(validationError.ErrorMessage) + Environment.NewLine;
					}
				}
				return OperationStatus.CreateFromException("Validation Exeption " + error, dbEx);
			}
			catch (Exception ex)
			{
				return OperationStatus.CreateFromException("Failed to save blog", ex);
			}

			result.Status = result.RecordsAffected > 0;
			result.Message = !result.Status ? "Failed to save blog" : "Blog saved";
			return result;
		}

		public OperationStatus DeleteBlog(int blogId)
		{
			var result = new OperationStatus();
			try
			{
				using (var context = new CurrencyExposureContext())
				{
					Blog myBlog = context.Blogs.FirstOrDefault(c => c.Id == blogId);
					context.Entry(myBlog).State = EntityState.Deleted;
					result.RecordsAffected = context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				return OperationStatus.CreateFromException("Failed to delete blog", ex);
			}

			result.Status = result.RecordsAffected > 0;
			result.Message = !result.Status ? "Failed to delete blog" : "Blog deleted";
			return result;
		}

		public OperationStatus SaveComments(BlogCommentDto comment)
		{
			var blogComment = new BlogComment();
			blogComment.Name = comment.Name;
			blogComment.Title = comment.Title;
			blogComment.Email = comment.Email;
			blogComment.Comment = comment.Comment;

			var result = new OperationStatus();
			try
			{
				using (var context = new CurrencyExposureContext())
				{
					var blog = context.Blogs.First(c => c.Id == comment.BlogId);
					blogComment.Blog = blog;
					context.BlogComments.Add(blogComment);
					result.RecordsAffected = context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				return OperationStatus.CreateFromException("Failed to create comment", ex);
			}

			result.Status = result.RecordsAffected > 0;
			if (!result.Status)
				result.Message = "Failed to save comment";
			else
			{
				_emailHelper.SendEmail(EmailTo, string.Format("A New Comment has been added by {0}", comment.Email),
				                       "A New Comment has been added");
				if (result.Status)
					result.Message = "Thanks for commenting";
			}

			return result;
		}

		public OperationStatus SaveContactUs(ContactUs contactDetails)
		{
			var result = new OperationStatus();
			try
			{
				using (var context = new CurrencyExposureContext())
				{
					context.ContactUs.Add(contactDetails);
					result.RecordsAffected = context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				return OperationStatus.CreateFromException("Failed to add contact us. Please try again", ex);
			}

			result.Status = result.RecordsAffected > 0;
			if (!result.Status)
				result.Message = "Failed to add contact us. Please try again";
			else
			{
				_emailHelper.SendEmail(EmailTo, string.Format("A new contact us has been recieved from {0}", contactDetails.Email),
									   "A new contact us has been recieved");
				if (result.Status)
					result.Message = "Thanks for contacting us";
			}

			return result;
		}

		public OperationStatus SaveEmailSubscription(EmailSubscribe emailSubDto)
		{
			var result = new OperationStatus();
			try
			{
				using (var context = new CurrencyExposureContext())
				{
					context.EmailSubscription.Add(emailSubDto);
					result.RecordsAffected = context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				return OperationStatus.CreateFromException("Failed to Save subscription.", ex);
			}

			result.Status = result.RecordsAffected > 0;
			if (!result.Status)
				result.Message = "Failed to subscribe. Please try again";
			else
			{
				_emailHelper.SendEmail(EmailTo, string.Format("A new subscription has been recieved from {0}", emailSubDto.Email),
									   "A new subscription has been recieved");
				if (result.Status)
					result.Message = "Thanks for subscribing";
			}
			return result;
		}
	}
}
	