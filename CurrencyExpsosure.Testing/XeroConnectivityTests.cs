using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xero.Api.Core;
using Xero.Api.Example.Applications.Private;
using Xero.Api.Example.Applications.Public;
using Xero.Api.Example.TokenStores;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Serialization;

namespace CurrencyExpsosure.Testing
{
	[TestClass]
	public class XeroConnectivityTests
	{
		[TestMethod]
		public void CanConnectAndGetOrgInfo()
		{
			var user = new ApiUser { Name = Environment.MachineName };

			// Public Application Sample
			var public_app_api = new XeroCoreApi("https://api.xero.com", new PublicAuthenticator("https://api.xero.com", "https://api.xero.com", "oob",
				new MemoryTokenStore()),
				new Consumer("TV1KKJBGHFUARJEJUIOF96ELTPF6K5", "VLZPP5R6LTZ2U9EAUOTAMSDK18A8OM"), user,
				new DefaultMapper(), new DefaultMapper());




			var public_contacts = public_app_api.Contacts.Find().ToList();

		}
	}
}
