using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using CurrencyExposure.Model;
using CurrencyExposure.Model.DatabaseObjects;
using CurrencyExposure.Repository.Xero;

namespace CurrencyExposure.Repository
{
	public class TokenRepository : ITokenRepository
	{
		public void AutorizeToken(string name, string token, string verifier, string orgId)
		{

			GetToken(this._tokenUri, token, "oauth/AccessToken", header);
			
			

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

			return System.Web.UI.WebControls.View(accessToken);
		}


		public OperationStatus AddToken(OAuthToken token)
		{
			var result = new OperationStatus();
			try
			{
				using (var context = new CurrencyExposureContext())
				{
					context.Entry(token).State = EntityState.Added;
					result.RecordsAffected = context.SaveChanges();
				}
				result.NewId = token.Id;
			}
			catch (DbEntityValidationException dbEx)
			{
				string error = string.Empty;
				foreach (var validationErrors in dbEx.EntityValidationErrors)
				{
					foreach (var validationError in validationErrors.ValidationErrors)
					{
						error += string.Format(validationError.ErrorMessage) + Environment.NewLine;
					}
				}
				return result.CreateFromException("Validation Exeption " + error, dbEx);
			}
			catch (Exception ex)
			{
				return result.CreateFromException("Failed to add token", ex);
			}

			result.Status = result.RecordsAffected > 0;
			result.Message = !result.Status ? "Failed to add token" : "Token saved";
			return result;
		}

		public OperationStatus<OAuthToken> GetToken(string organisationId)
		{
			var result = new OperationStatus<OAuthToken>();
			try
			{
				using (var context = new CurrencyExposureContext())
				{
					var token = context.OAuthToken.FirstOrDefault(t => t.OrganisationId == organisationId);
					if (token == null)
					{
						result.Message = "Token does not exist for your organisation";
						result.Status = false;
						return result;
					}
					if (token.SessionExpiresAt.Subtract(DateTime.Now.ToUniversalTime()).Seconds < 60)
					{
						DeleteToken(token.Id);
						result.Message = "Token has expired";
						result.Status = false;
						return result;
					}
					result.OperationObject = token;
				}

			}
			catch (Exception ex)
			{
				return result.CreateFromException("Failed to add token", ex);
			}
			return result;
		}

		public OperationStatus DeleteToken(int tokenId)
		{
			var result = new OperationStatus();
			try
			{
				using (var context = new CurrencyExposureContext())
				{
					OAuthToken myToken = context.OAuthToken.FirstOrDefault(c => c.Id == tokenId);
					context.Entry(myToken).State = EntityState.Deleted;
					result.RecordsAffected = context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				return result.CreateFromException("Failed to delete token", ex);
			}
			return result;
		}

		public bool IsTokenExpired(OAuthToken token)
		{
			return token.SessionExpiresAt > DateTime.UtcNow;
		}
	}

	public interface ITokenRepository
	{
		OperationStatus AddToken(OAuthToken token);
		OperationStatus<OAuthToken> GetToken(string organisationId);
		OperationStatus DeleteToken(int tokenId);
		bool IsTokenExpired(OAuthToken token);
	}
}
