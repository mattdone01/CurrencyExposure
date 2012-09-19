using System.Web.Mvc;
using CurrencyExposure.Repository;

namespace CurrencyExposure.Controllers
{
    public class BlogController : Controller
    {
	    private readonly IBlogRepository _blogRepository;

	    public BlogController(IBlogRepository blogRepository)
        {
	        _blogRepository = blogRepository;
        }

	    public ActionResult GetBlog(string id)
	    {
		    int blogId = -1;
		    int.TryParse(id, out blogId);
			var blog = _blogRepository.GetBlog(blogId);
			return Json(blog, JsonRequestBehavior.AllowGet);
        }
    }
}
