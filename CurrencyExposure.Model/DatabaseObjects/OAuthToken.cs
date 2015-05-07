using System;

namespace CurrencyExposure.Model.DatabaseObjects
{
	public class OAuthToken
	{
		public int Id { get; set; }

		public string UserId { get; set; }

		public string OrganisationId { get; set; }

		public string ConsumerKey { get;set; }

		public string ConsumerSecret { get;set; }

		public string TokenKey { get; set;}

		public string TokenSecret { get; set;}

		public string Session { get; set; }

		public DateTime ExpiresAt { get; set;}

		public DateTime? SessionExpiresAt { get; set; }
	}
}
