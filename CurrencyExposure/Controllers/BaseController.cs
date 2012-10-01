using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CurrencyExposure.Helpers;
using Newtonsoft.Json;

namespace CurrencyExposure.Controllers
{
	public class BaseController : Controller
	{
		protected BaseController()
		{
			ViewData["Message"] = string.Empty;
		}

		public JsonNetResult ToJsonNet(object data)
		{
			var jsonNetResult = new JsonNetResult();
			jsonNetResult.Formatting = Formatting.Indented;
			jsonNetResult.Data = data;
			return jsonNetResult;
		}

		public JsonNetResult ToJsonNet(object data, JsonRequestBehavior behaviour)
		{
			var jsonNetResult = new JsonNetResult();
			jsonNetResult.Formatting = Formatting.Indented;
			jsonNetResult.Data = data;
			return jsonNetResult;
		}
	}
}