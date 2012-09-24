using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using CurrencyExposure.Helpers;
using CurrencyExposure.Model;
using CurrencyExposure.Repository;

namespace CurrencyExposure.Controllers
{
	public class BlogController : BaseController
    {
	    private readonly IBlogRepository _blogRepository;

	    public BlogController(IBlogRepository blogRepository)
        {
	        _blogRepository = blogRepository;
        }

		public ActionResult GetBlog(int id = 0)
		{
			var blog = _blogRepository.GetBlog(id);
			return View("Blog",blog);
		}

		public async Task<JsonNetResult> GetCommentsList(int count = 3)
		{
			var createDate = DateTime.MinValue;
			var result = await _blogRepository.GetCommentsList(count);
			return ToJsonNet(result, JsonRequestBehavior.AllowGet);
		}

		public async Task<JsonNetResult> GetArticlesList(int count = 3)
		{
			var createDate = DateTime.MinValue;
			var result = await _blogRepository.GetArticlesList(count);
			return ToJsonNet(result, JsonRequestBehavior.AllowGet);
		}

		public async Task<JsonResult> GetArchiveList()
		{
			return null;
		}
    }
}
