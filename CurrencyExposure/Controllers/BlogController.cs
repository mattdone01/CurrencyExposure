using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Web.Mvc;
using CurrencyExposure.Helpers;
using CurrencyExposure.Model;
using CurrencyExposure.Repository;
using CurrencyExposure.Helpers;

namespace CurrencyExposure.Controllers
{
	public class BlogController : BaseController
    {
	    private readonly IBlogRepository _blogRepository;

	    public BlogController(IBlogRepository blogRepository)
        {
	        _blogRepository = blogRepository;
        }

		public ActionResult Index(int id = 0)
		{
			var blog = _blogRepository.GetBlog(id);
			if (Request.IsAjaxRequest())
				return PartialView("Blog", blog);

			return View("Blog", blog);
		}

		public ActionResult GetBlog(int id = 0)
		{
			return Index(id);
		}

		public ActionResult GetBlogAsJson(int id)
		{
			var blog = _blogRepository.GetBlog(id);
			return Json(blog, JsonRequestBehavior.AllowGet);
		}

		public async Task<ActionResult> GetCommentsList(int count = 5)
		{
			List<CommentsListDto> result = await _blogRepository.GetCommentsListAsync(count);
			return PartialView("_BlogRecentCommentsList", result);
		}

		public async Task<ActionResult> GetCommentsListAsJson(int count = 5)
		{
			List<CommentsListDto> result = await _blogRepository.GetCommentsListAsync(count);
			return ToJsonNet(result, JsonRequestBehavior.AllowGet);
		}

		public async Task<ActionResult> GetArticlesList(int count = 5)
		{
			List<BlogSummaryDto> result = await _blogRepository.GetArticlesListAsync(count);
			return PartialView("_BlogArticleList", result);
		}

		public async Task<JsonNetResult> GetArticlesListAsJson(int count = 5)
		{
			var result = await _blogRepository.GetArticlesListAsync(count);
			return ToJsonNet(result, JsonRequestBehavior.AllowGet);
		}

		public ActionResult GetPosts()
		{
			var blog = _blogRepository.GetBlog();
			var result =  _blogRepository.GetArticlesList(25);
			var postItems = result.Select(p => new SyndicationItem(p.Title, p.Summary, new Uri(p.BlogUrl),p.Id.ToString(),p.CreateDate));

			var feed = new SyndicationFeed(blog.Title, blog.BlogSummary, new Uri(blog.BlogUrl), blog.Id.ToString(),
			                               blog.CreateDate, postItems)
				{
					Language = "en-US"
				};

			return new FeedResult(new Rss20FeedFormatter(feed));
		}
		
		public ActionResult CreateComment()
		{
			var blogComment = new BlogCommentDto();
			return PartialView("_blogCommentPartial", blogComment);
		}

		public ActionResult BlogSearch()
		{
			if (Request.IsAjaxRequest())
				return PartialView("BlogSearch");

			return View("BlogSearch");
		}

		public ActionResult CommentSearch()
		{
			if (Request.IsAjaxRequest())
				return PartialView("CommentSearch"); 

			return View("CommentSearch");
		}

		[HttpPost]
		public ActionResult SaveComment(BlogCommentDto blogComment)
		{
			var result = new OperationStatus();
			if (ModelState.IsValid)
			{
				result = _blogRepository.SaveComments(blogComment);
				var blog = _blogRepository.GetBlog(blogComment.BlogId);
				var partialView = this.RenderPartialViewToString("_BlogCommentListPartial", blog.BlogComments);
				result.RenderedPartialViewUpdate = partialView;
			}
			else
			{
				result.Message = "Failed to save comment";
			}
			
			return Json(result); 
		}
    }
}
