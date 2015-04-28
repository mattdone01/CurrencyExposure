using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyExposure.Repository.Xero;
using Xero.Api.Core;
using Xero.Api.Example.Applications.Private;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Serialization;


namespace CurrencyExposure.Repository
{
	public class InvoiceRepository
	{
		public void GetInvoices(int companyId)
		{
			//var repository = ServiceProvider.GetCurrentRepository();

			 var private_app_api = new XeroCoreApi("https://api.xero.com", new PrivateAuthenticator(@"C:\Dev\your_public_privatekey.pfx"),
            new Consumer("XVWNNE07AHZDFAMDXOJO8YDMM3NX84", "X8KFYYEFONU0HL8B7U7OVB2AWYBAEG"), null,new DefaultMapper(), new DefaultMapper());

			 var org = private_app_api.Organisation;
			 var user = new ApiUser { Name = Environment.MachineName };


		//	var api = XeroApiHelper.CoreApi();
			//var invoices = api.Invoices.Find()
		}
	}
}
