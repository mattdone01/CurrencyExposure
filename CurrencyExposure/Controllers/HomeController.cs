using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CurrencyExposure.Repository;

namespace CurrencyExposure.Controllers
{
    public class HomeController : Controller
    {
         private readonly IBlogRepository _blogRepository;
		public HomeController(IBlogRepository blogRepository)
		{
			_blogRepository = blogRepository;
		}

        public ActionResult Index()
        {
	       var blogs = _blogRepository.GetBlogSummaries();
		   return View(blogs);
        }

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Contact()
		{
			return View();
		}

		public ActionResult Links()
		{
			return View();
		}
    }
}
