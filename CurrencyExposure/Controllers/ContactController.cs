﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CurrencyExposure.Model;
using CurrencyExposure.Repository;

namespace CurrencyExposure.Controllers
{
    public class ContactController : Controller
    {
		 private readonly IBlogRepository _blogRepository;
		 public ContactController(IBlogRepository blogRepository)
		{
			_blogRepository = blogRepository;
		}

		public ActionResult Index(bool prodEnquiry = false)
        {
			var contact = new ContactUs { ProductEnquiry = prodEnquiry };
			return View(contact);
        }

		[HttpPost]
		public ActionResult SaveContactUs(ContactUs contactDetail)
		{
			var result = new OperationStatus();
			if (ModelState.IsValid)
			{
				result = _blogRepository.SaveContactUs(contactDetail);
			}

			return Json(result);
		}
    }
}
