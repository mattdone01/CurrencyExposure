using System.Web.Mvc;
using CurrencyExposure.Repository.Xero;
using Xero.Api.Example.Applications.Public;

namespace CurrencyExposure.Web.Controllers
{
	public class OrganisationController : Controller
	{
		public ActionResult Index()
		{
			var api = XeroApiHelper.CoreApi();

			try
			{
				var organisation = api.Organisation;

				return View(organisation);
			}
			catch (RenewTokenException e)
			{
				return RedirectToAction("Connect", "Home");
			}  
		}
	}
}