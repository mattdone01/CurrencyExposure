using System;
using System.Web.Mvc;
using CurrencyExposure.Repository;
using CurrencyExposure.Web.Helpers;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.OAuth;

namespace CurrencyExposure.Web.Controllers
{
    public interface IMvcAuthenticator : IAuthenticator
    {
        string GetRequestTokenAuthorizeUrl(IConsumer consumer, string userId);
        IToken RetrieveAndStoreAccessToken(string userId, string tokenKey, string verfier, string organisationShortCode);
    }

    public class TokenController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenStore _tokenStore;
        private IMvcAuthenticator _authenticator;
		private ApiUser _user;
	
		public TokenController(IAccountRepository accountRepository, ITokenStore tokenStore)
		{
            //TODO: Need to Inject this dependency.
            _authenticator = new XeroPublicAuthenticator( tokenStore);
		    _accountRepository = accountRepository;
		    _tokenStore = tokenStore;
		}

        /// <summary>
        /// Gets a branch new token, without trying to delete a token first.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var result = _accountRepository.GetUser(User.Identity.Name);
            if (!result.Status)
                return View("Error", result);

            var consumer = new Consumer(result.OperationObject.Company.ConsumerKey, result.OperationObject.Company.ConsumerSecret);
            var authorizeUrl = _authenticator.GetRequestTokenAuthorizeUrl(consumer, result.OperationObject.EmailAddress);
            return Redirect(authorizeUrl);
        }

        /// <summary>
        /// Renews a token.
        /// </summary>
        /// <returns></returns>
        public ActionResult Renew()
        {
            var result = _accountRepository.GetUser(User.Identity.Name);
            if (!result.Status)
                return View("Error", result);

            try
            {
                var token = _tokenStore.Find(result.OperationObject.Company.OrganisationId);
                _tokenStore.Delete(token);
            }
            catch (Exception ex)
            {
               //TODO: Need to log exception.
                return View("Error");
            }

            //Get New token.
            var consumer = new Consumer(result.OperationObject.Company.ConsumerKey, result.OperationObject.Company.ConsumerSecret);
            var authorizeUrl = _authenticator.GetRequestTokenAuthorizeUrl(consumer, result.OperationObject.EmailAddress);
            return Redirect(authorizeUrl);
        }

        [AllowAnonymous]
		public ActionResult Authorize(string oauth_token, string oauth_verifier, string org)
        {
            var accessToken = _authenticator.RetrieveAndStoreAccessToken(_user.Name, oauth_token, oauth_verifier, org);
            if (accessToken == null)
                return View("NoAuthorized");

            return RedirectToAction("Index", "Home");
        }
    }
}
