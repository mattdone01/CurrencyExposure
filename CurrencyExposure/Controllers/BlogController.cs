using System;
using System.Collections.Generic;
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

		public async Task<JsonNetResult> GetCommentsList(int count = 3)
		{
			var result = await _blogRepository.GetCommentsList(count);
			return ToJsonNet(result, JsonRequestBehavior.AllowGet);
		}

		public async Task<JsonNetResult> GetArticlesList(int count = 3)
		{
			var result = await _blogRepository.GetArticlesList(count);
			return ToJsonNet(result, JsonRequestBehavior.AllowGet);
		}
		
		public ActionResult CreateComment()
		{
			var blogComment = new BlogCommentDto();
			return PartialView("_blogCommentPartial", blogComment);
		}

		public ActionResult BlogSearch()
		{
			return View("BlogSearch");
		}

		public ActionResult CommentSearch()
		{
			return View("CommentSearch");
		}

		[HttpPost]
		public ActionResult SaveComment(BlogCommentDto blogComment)
		{
			var result = new OperationStatus();
			if (ModelState.IsValid)
			{
				result = _blogRepository.SaveComments(blogComment);
			}

			var blog = _blogRepository.GetBlog(blogComment.BlogId);
			var partialView = this.RenderPartialViewToString("_BlogCommentListPartial", blog.BlogComments);
			result.RenderedPartialViewUpdate = partialView;
			return Json(result); 
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult SaveBlog(BlogDto myBlogDto)
		{
			var result = new OperationStatus();
			if (ModelState.IsValid)
				result = _blogRepository.SaveBlog(myBlogDto);

			return Json(result); 
		}

		[HttpPost]
		public ActionResult DeleteBlog(int blogId)
		{
			var result = new OperationStatus();
			if (blogId > 0)
				result = _blogRepository.DeleteBlog(blogId);

			return Json(result);
		}
    }
}
