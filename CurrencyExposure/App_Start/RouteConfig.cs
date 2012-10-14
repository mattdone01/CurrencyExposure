using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CurrencyExposure
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "GetBlog",
				url: "getblog/{id}",
				defaults: new { controller = "Blog", action = "GetBlog", id = UrlParameter.Optional }
				);

			routes.MapRoute(
				name: "ContactUsProductEnquiry",
				url: "contactenq/{prodenquiry}",
				defaults: new { controller = "Contact", action = "Index", prodEnquiry = UrlParameter.Optional }
				);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
				);


		}
	}
}