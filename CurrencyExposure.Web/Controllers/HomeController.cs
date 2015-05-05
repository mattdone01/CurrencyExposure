using System.Web.Mvc;

namespace CurrencyExposure.Web.Controllers
{
	public class HomeController : Controller
	{
		public HomeController()
		{

		}

		public ActionResult Index()
		{
			return View();
		}
	}
}