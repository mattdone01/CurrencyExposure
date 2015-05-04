using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CurrencyExposure.Model;
using CurrencyExposure.Model.DatabaseObjects;
using CurrencyExposure.Repository.Xero;
using Xero.Api.Core.Model;
using Xero.Api.Core.Model.Types;
using Xero.Api.Example.Applications.Public;
using Invoice = CurrencyExposure.Model.DatabaseObjects.Invoice;

namespace CurrencyExposure.Web.Controllers
{
	public class InvoiceController : Controller
	{
		public ActionResult Index()
		{
			var api = XeroApiHelper.CoreApi();

			try
			{
				var invoices = api.Invoices.Find().ToList();
				List<Invoice> myInvoices = new List<Invoice>();
				foreach (var invoice in invoices)
				{
					if (invoice.Type == InvoiceType.AccountsReceivable)
						myInvoices.Add(new Bill()
						               {
							               InvoiceId = invoice.Id.ToString(),
							               CreateDate = invoice.Date.GetValueOrDefault(DateTime.MaxValue),
							               DueDate = invoice.DueDate.GetValueOrDefault(DateTime.MaxValue),
							               Amount = invoice.AmountDue.GetValueOrDefault(0),
							               Currency = invoice.CurrencyCode,
							               CurrencyRate = invoice.CurrencyRate.GetValueOrDefault(0)
						               });
					else if (invoice.Type == InvoiceType.AccountsPayable)
						myInvoices.Add(new PurchaseOrder()
						{
							InvoiceId = invoice.Id.ToString(),
							CreateDate = invoice.Date.GetValueOrDefault(DateTime.MaxValue),
							DueDate = invoice.DueDate.GetValueOrDefault(DateTime.MaxValue),
							Amount = invoice.AmountDue.GetValueOrDefault(0),
							Currency = invoice.CurrencyCode,
							CurrencyRate = invoice.CurrencyRate.GetValueOrDefault(0)
						});
				}

				return View(myInvoices);
			}
			catch (RenewTokenException e)
			{
				return RedirectToAction("Connect", "Home");
			}
		}
	}
}