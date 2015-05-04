using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CurrencyExposure.Model.DatabaseObjects;
using CurrencyExposure.Repository;
using CurrencyExposure.Repository.Xero;

namespace CurrencyExposure.Web.Controllers
{
    public class TokenController : Controller
    {
		private readonly ITokenRepository _tokenRepository;
		private IMvcAuthenticator _authenticator;
		private ApiUser _user;
	
		public TokenController(ITokenRepository tokenRepository)
	    {
			_tokenRepository = tokenRepository;
			_user = XeroApiHelper.User();

			
	    }
        // GET: /Token/
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Authorize(string oauth_token, string oauth_verifier, string org)
		{

		}

    }
}
