using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using CurrencyExposure.Model;
using CurrencyExposure.Model.DatabaseObjects;

namespace CurrencyExposure.Repository
{
    public class TokenRepository : ITokenRepository
	{
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

		public OperationStatus<OAuthToken> GetToken(string userId)
		{
			var result = new OperationStatus<OAuthToken>();
			try
			{
				using (var context = new CurrencyExposureContext())
				{
					var token = context.OAuthToken.FirstOrDefault(t => t.UserId == userId);
					if (token == null)
					{
						result.Message = "Token does not exist for your organisation";
						result.Status = false;
						return result;
					}
					if (token.ExpiresAt.Subtract(DateTime.Now.ToUniversalTime()).TotalSeconds < 60)
					{
						DeleteToken(token.OrganisationId);
						result.Message = "Token has expired";
						result.Status = false;
						return result;
					}
					result.OperationObject = token;
					result.Status = true;
				}

			}
			catch (Exception ex)
			{
				return result.CreateFromException("Failed to add token", ex);
			}
			return result;
		}

		public OperationStatus DeleteToken(string organisationId)
		{
			var result = new OperationStatus();
			try
			{
				using (var context = new CurrencyExposureContext())
				{
                    OAuthToken myToken = context.OAuthToken.FirstOrDefault(c => c.OrganisationId == organisationId);
					if (myToken != null)
					{
						context.Entry(myToken).State = EntityState.Deleted;
						result.RecordsAffected = context.SaveChanges();
					}
					result.Status = true;
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
		OperationStatus DeleteToken(string organisationId);
		bool IsTokenExpired(OAuthToken token);
	}
}
