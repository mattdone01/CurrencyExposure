using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CurrencyExposure.Helpers;
using CurrencyExposure.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CurrencyExposure
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			Database.SetInitializer<CurrencyExposureContext>(null);
			//Custom dependency resolver
			var unityContainer = ModelContainer.Instance;
			//unityContainer.RegisterControllers(); No longer needed due to package update
			DependencyResolver.SetResolver(new UnityDependencyResolver(unityContainer));
		}
	}
}