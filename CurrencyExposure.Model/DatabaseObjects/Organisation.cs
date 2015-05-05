using System.Collections.Generic;

namespace CurrencyExposure.Model.DatabaseObjects
{
	public class Organisation
	{
		public string OrganisationId { get; set; }
		public string OrganisationName { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
		public ICollection<User> LoginUsers { get; set; }
	}
}
