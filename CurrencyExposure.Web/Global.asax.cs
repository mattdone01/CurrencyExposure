using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CurrencyExposure.Repository;
using CurrencyExposure.Web.Helpers;
using Microsoft.Practices.Unity;

namespace CurrencyExposure.Web
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
			IUnityContainer unityContainer = IocContainer.Instance;
		    IocWebRegistration.RegisterMvcTypesForIoc(unityContainer);
			DependencyResolver.SetResolver(new UnityDependencyResolver(unityContainer));
		}
	}
}