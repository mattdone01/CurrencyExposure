using System;
using System.Security.Cryptography.X509Certificates;
using CurrencyExposure.Web.Controllers;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.OAuth.Signing;
using Xero.Api.Infrastructure.OAuth;

namespace CurrencyExposure.Web.Helpers
{
    public class XeroPublicAuthenticator : IMvcAuthenticator
    {
        private readonly string _baseUri;
        private readonly string _tokenUri;
        private readonly string _callBackUrl;
        private readonly ITokenStore _store;

        public XeroPublicAuthenticator(ITokenStore store)
        {
            _baseUri = "https://api.xero.com"; //TODO: Move to App Settings
            _tokenUri = "https://api-partner.network.xero.com"; //TODO: Move to App Settings
            _callBackUrl = "http://localhost/CurrencyExposure.Web/Token/Authorize"; //TODO: Move to App Settings
            _store = store;
        }

        private OAuthTokens _tokens;
        protected OAuthTokens Tokens
        {
            get
            {
                if (_tokens == null)
                    _tokens = new OAuthTokens(this._tokenUri, _baseUri, GetClientCertificate());
                return _tokens;
            }
        }

        protected virtual X509Certificate2 GetClientCertificate()
        {
            return null;
        }

        protected string AuthorizeUser(IToken token)
        {
            throw new NotSupportedException();
        }

        protected string CreateSignature(IToken token, string verb, Uri uri, string verifier,
            bool renewToken = false, string callback = null)
        {
            return new HmacSha1Signer().CreateSignature(token, uri, verb, verifier, callback);
        }

        protected IToken RenewToken(IToken sessionToken, IConsumer consumer)
        {
            throw new RenewTokenException();
        }

        public string GetRequestTokenAuthorizeUrl(IConsumer consumer, string userId)
        {
            IToken requestToken = GetRequestToken(consumer);
            requestToken.UserId = userId;
            return GetAuthorizeUrl(requestToken);
        }

        protected string GetAuthorizeUrl(IToken token)
        {
            return new UriBuilder(this.Tokens.AuthorizeUri)
            {
                Query = ("oauth_token=" + token.TokenKey)
            }.Uri.ToString();
        }

        protected IToken GetRequestToken(IConsumer consumer)
        {
            string authorization = GetAuthorization(new Token()
            {
                ConsumerKey = consumer.ConsumerKey,
                ConsumerSecret = consumer.ConsumerSecret
            }, "POST", Tokens.RequestUri, null, null, false, _callBackUrl);
            return Tokens.GetRequestToken(consumer, authorization);
        }

        protected string GetAuthorization(IToken token, string verb, string endpoint, string query = null, string verifier = null, bool renewToken = false, string callback = null)
        {
            UriBuilder uriBuilder = new UriBuilder(_baseUri)
            {
                Path = endpoint
            };
            if (!string.IsNullOrWhiteSpace(query))
                uriBuilder.Query = query.TrimStart('?');
            return CreateSignature(token, verb, uriBuilder.Uri, verifier, renewToken, callback);
        }

        public IToken RetrieveAndStoreAccessToken(string userId, string tokenKey, string verfier,
            string organisationShortCode)
        {
            IToken token1 = _store.Find(userId);
            if (token1 == null)
                throw new ApplicationException("Failed to look up request token for user");
            if (token1.TokenKey != tokenKey)
                throw new ApplicationException("Request token key does not match");
            IToken accessToken = Tokens.GetAccessToken(token1,
                GetAuthorization(token1, "POST", Tokens.AccessUri, null, verfier, false));
            accessToken.UserId = userId;
            _store.Delete(accessToken);
            _store.Add(accessToken);
            return accessToken;
        }

        public string GetSignature(IConsumer consumer, IUser user, Uri uri, string verb, IConsumer consumer1)
        {
            return GetAuthorization(GetToken(consumer, user), verb, uri.AbsolutePath, uri.Query, null, false);
        }

        public IToken GetToken(IConsumer consumer, IUser user)
        {
            IToken token1 = _store.Find(user.Name);
            if (token1 == null)
            {
                IToken token2 = GetToken(consumer);
                token2.UserId = user.Name;
                _store.Add(token2);
                return token2;
            }
            if (!token1.HasExpired)
                return token1;
            IToken token3 = this.RenewToken(token1, consumer);
            token3.UserId = user.Name;
            _store.Delete(token1);
            _store.Add(token3);
            return token3;
        }

        protected virtual IToken GetToken(IConsumer consumer)
        {
            IToken requestToken = GetRequestToken(consumer);
            string verifier = AuthorizeUser(requestToken);
            return Tokens.GetAccessToken(requestToken, GetAuthorization(requestToken, "POST", Tokens.AccessUri, null, verifier, false));
        }

        public IUser User { get; set; }
    }
}