using System.Web.Mvc;
using CurrencyExposure.Repository;
using Xero.Api.Infrastructure.Interfaces;

namespace CurrencyExposure.Web.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly IAccountRepository _accountRepository;
		private readonly ITokenStore _tokenStore;

		public HomeController(IAccountRepository accountRepository, ITokenStore tokenStore)
		{
			_accountRepository = accountRepository;
			_tokenStore = tokenStore;
		}

		public ActionResult Index()
		{
			var result = _accountRepository.GetUser(User.Identity.Name);
			if (!result.Status)
				return View("Error", result);

			var token = _tokenStore.Find(User.Identity.Name);
			if (token == null || token.HasExpired)
			{
				return RedirectToAction("Index","Token");
			}

			return View();
		}
	}
}