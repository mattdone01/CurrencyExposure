﻿using System;
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


		public ActionResult Links()
		{
			return View();
		}

		public ActionResult ExposurePlatform()
		{
			return View();
		}

		public ActionResult EmailSubscribe()
		{
			var emailModel = new EmailSubscribe();
			return PartialView("_EmailSubscriptionPartial", emailModel);
		}

		[HttpPost]
		public ActionResult SaveEmail(EmailSubscribe emaildto)
		{
			var status = new OperationStatus();
			if (ModelState.IsValid)
			{
				status = _blogRepository.SaveEmailSubscription(emaildto);
			}

			return Json(status);
		}
    }
}
