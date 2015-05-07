using System;
using Xero.Api.Infrastructure.Interfaces;
using CurrencyExposure.Model.DatabaseObjects;
using Xero.Api.Infrastructure.OAuth;

namespace CurrencyExposure.Repository.Xero
{
    public class XeroSqlTokenStore: ITokenStore
    {
        private readonly ITokenRepository _tokenRepository;

        public XeroSqlTokenStore(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public IToken Find(string user)
        {
            var result = _tokenRepository.GetToken(user);
	        if (!result.Status)
		        return null;
            var storedToken = result.OperationObject;

            var xeroToken = new Token();
            xeroToken.ConsumerKey = storedToken.ConsumerKey;
            xeroToken.ConsumerSecret = storedToken.ConsumerSecret;
            xeroToken.ExpiresAt = storedToken.SessionExpiresAt;
            xeroToken.OrganisationId = storedToken.OrganisationId;
            xeroToken.Session = storedToken.Session;
            xeroToken.SessionExpiresAt = storedToken.SessionExpiresAt;
            xeroToken.TokenKey = storedToken.TokenKey;
            xeroToken.TokenSecret = storedToken.TokenSecret;
            xeroToken.UserId = storedToken.UserId;

            return xeroToken;
        }

        public void Add(IToken accessToken)
        {
            var token = new OAuthToken();
            token.ConsumerKey = accessToken.ConsumerKey;
            token.ConsumerSecret = accessToken.ConsumerSecret;
            token.ExpiresAt = accessToken.ExpiresAt.Value;
            token.OrganisationId = accessToken.OrganisationId;
            token.Session = accessToken.Session;
			token.SessionExpiresAt = accessToken.SessionExpiresAt ?? accessToken.ExpiresAt.Value; 
            token.TokenKey = accessToken.TokenKey;
            token.TokenSecret = accessToken.TokenSecret;
            token.UserId = accessToken.UserId;
            var result = _tokenRepository.AddToken(token);
            if (!result.Status)
                throw new Exception("Failed to Add new token");
        }

        public void Delete(IToken token)
        {
            _tokenRepository.DeleteToken(token.OrganisationId);
        }
    }
}
