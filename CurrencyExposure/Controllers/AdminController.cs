using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurrencyExposure.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

		public JsonResult SaveBlogPost()
		{
			return null;
		}

	    public JsonResult UploadFile()
	    {
			return null;	
	    }
    }
}
