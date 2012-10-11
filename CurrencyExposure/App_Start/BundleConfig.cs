﻿using System.Web;
using System.Web.Optimization;

namespace CurrencyExposure
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/libs/jquery-{version}.js", 
						"~/Scripts/libs/jQuery.tmpl.js",
						"~/Scripts/libs/jquery.tipTip.js"));


			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/libs/jquery.unobtrusive*",
						"~/Scripts/libs/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/currencyexposure").Include(
				"~/Scripts/startup.js"));

			bundles.Add(new ScriptBundle("~/bundles/kendo").Include("~/Scripts/libs/kendo/kendo.web.js"));
			   // "~/Scripts/libs/kendo/kendo.core.js",
			   // "~/Scripts/libs/kendo/kendo.data.js",
			   // "~/Scripts/libs/kendo/kendo.fx.js",
			   // "~/Scripts/libs/kendo/kendo.resizable.js",
			   // "~/Scripts/libs/kendo/kendo.window.js",
			   // "~/Scripts/libs/kendo/kendo.upload.js",
			   //"~/Scripts/libs/kendo/kendo.draganddrop.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/libs/modernizr-*"));

			bundles.Add(new StyleBundle("~/Content/less").Include("~/Content/site.less"));
			bundles.Add(new StyleBundle("~/Content/tipTip/css").Include("~/Content/tipTip/tipTip.css"));

			bundles.Add(new StyleBundle("~/Content/kendo/css").Include(
				"~/Content/kendo/kendo.common.css",
				"~/Content/kendo/kendo.default.css"));

		}
	}
}