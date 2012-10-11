using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CurrencyExposure.Model;
using CurrencyExposure.Repository;

namespace CurrencyExposure.Controllers
{
    public class HomeController : BaseController
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

		public ActionResult ExposurePlatform()
		{
			return View();
		}

		public ActionResult SaveContactUs(ContactUs contactDetail)
		{
			ViewData["Message"] = string.Empty;
			var result = new TransactionResult(false);
			if (ModelState.IsValid)
			{
				result = _blogRepository.SaveContactUs(contactDetail);
			}
			ViewData["Message"] = !result.Success ? result.ErrorText : "Thanks for contacting us.";
			return View("Contact");
		}

		public ActionResult EmailSubscribe()
		{
			var emailModel = new EmailSubscribe();
			return PartialView("_EmailSubscriptionPartial", emailModel);
		}
    }
}
