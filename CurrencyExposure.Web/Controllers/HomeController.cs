using System.Web.Mvc;
using CurrencyExposure.Model;
using CurrencyExposure.Model.DatabaseObjects;
using CurrencyExposure.Repository;
using CurrencyExposure.Repository.Xero;
using Xero.Api.Example.Applications.Public;
using Xero.Api.Infrastructure.OAuth;

namespace CurrencyExposure.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ITokenRepository _tokenRepository;
		private IMvcAuthenticator _authenticator;
		private ApiUser _user;

		public HomeController(ITokenRepository tokenRepository)
		{
			_tokenRepository = tokenRepository;
			_user = XeroApiHelper.User();

			_authenticator = XeroApiHelper.MvcAuthenticator();
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Connect()
		{
			var authorizeUrl = _authenticator.GetRequestTokenAuthorizeUrl(_user.Name);

			return Redirect(authorizeUrl);
		}

		public ActionResult Authorize(string oauth_token, string oauth_verifier, string org)
		{
			var accessToken = _authenticator.RetrieveAndStoreAccessToken(_user.Name, oauth_token, oauth_verifier, org);
			if (accessToken == null)
				return View("NoAuthorized");

			var token = new OAuthToken();
			token.ConsumerKey = accessToken.ConsumerKey;
			token.ConsumerSecret = accessToken.ConsumerSecret;
			token.ExpiresAt = accessToken.SessionExpiresAt.Value;
			token.OrganisationId = accessToken.OrganisationId;
			token.Session = accessToken.Session;
			token.SessionExpiresAt = accessToken.SessionExpiresAt.Value;
			token.TokenKey = accessToken.TokenKey;
			token.TokenSecret = accessToken.TokenSecret;
			token.UserId = accessToken.UserId;
			var result = _tokenRepository.AddToken(token);
			if (!result.Status)
				return View("NoAuthorized");

			return View(accessToken);
		}

	}
}