using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyExposure.Model;
using CurrencyExposure.Model.DatabaseObjects;
using CurrencyExposure.Repository;
using Xero.Api.Core;
using Xero.Api.Core.Model.Types;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Serialization;

namespace CurrencyExposure.Web.Handlers
{
	public class XeroImportDataHandler : IImportDataHandler
	{
		private string _baseUri = "https://api.xero.com"; //TODO: Move to App Settings
		private readonly IAuthenticator _authenticator;
		private readonly IBillRepository _billRepository;
		private readonly IPurchaseOrderRepository _purchaseOrderRepository;
		private readonly ITokenStore _tokenStore;

		public XeroImportDataHandler(IAuthenticator authenticator, IBillRepository billRepository,
			IPurchaseOrderRepository purchaseOrderRepository, ITokenStore tokenStore)
		{
			_authenticator = authenticator;
			_billRepository = billRepository;
			_purchaseOrderRepository = purchaseOrderRepository;
			_tokenStore = tokenStore;
		}

		public OperationStatus ImportData(User user)
		{
			var token = _tokenStore.Find(user.EmailAddress);
			if (token == null || token.HasExpired)
				return new OperationStatus(false, "Please Renew your token") {TokenExpired = true};

			var consumer = new Consumer(user.Company.ConsumerKey, user.Company.ConsumerSecret);
			var apiUser = new ApiUser
			              {
				              Name = user.EmailAddress,
				              OrganisationId = user.Company.OrganisationId
			              };

			var coreApi = new XeroCoreApi(_baseUri, _authenticator, consumer, apiUser, new DefaultMapper(), new DefaultMapper());

			var invoices = coreApi.Invoices.Find().ToList();
			var pos = new List<PurchaseOrder>();
			var bills = new List<Bill>();
			foreach (var invoice in invoices)
			{
				if (invoice.Type == InvoiceType.AccountsReceivable)
					bills.Add(new Bill
					          {
						          InvoiceId = invoice.Id.ToString(),
						          CreateDate = invoice.Date.GetValueOrDefault(DateTime.MaxValue),
						          DueDate = invoice.DueDate.GetValueOrDefault(DateTime.MaxValue),
						          Amount = invoice.AmountDue.GetValueOrDefault(0),
						          Currency = invoice.CurrencyCode?? "AUD",
						          CurrencyRate = invoice.CurrencyRate.GetValueOrDefault(0),
						          OrganisationId = user.Company.OrganisationId
					          });
				else if (invoice.Type == InvoiceType.AccountsPayable)
					pos.Add(new PurchaseOrder
					        {
						        InvoiceId = invoice.Id.ToString(),
						        CreateDate = invoice.Date.GetValueOrDefault(DateTime.MaxValue),
						        DueDate = invoice.DueDate.GetValueOrDefault(DateTime.MaxValue),
						        Amount = invoice.AmountDue.GetValueOrDefault(0),
						        Currency = invoice.CurrencyCode ?? "AUD",
						        CurrencyRate = invoice.CurrencyRate.GetValueOrDefault(0),
						        OrganisationId = user.Company.OrganisationId
					        });
			}

			var result = new OperationStatus();
			var billSaveResult = _billRepository.SaveBills(bills, user.Company.OrganisationId);
			if (!billSaveResult.Status)
			{
				result.Status = false;
				result.Message = " Failed to import bills. Please try again";
				return result;
			}
			var poSaveResult = _purchaseOrderRepository.SavePOs(pos, user.Company.OrganisationId);
			if (!poSaveResult.Status)
			{
				result.Status = false;
				result.Message = " Failed to import purchase orders. Please try again";
				return result;
			}
			result.Status = true;
			result.Message = "Data has been successfully imported";
			return result;
		}
	}

	public interface IImportDataHandler
    {
        OperationStatus ImportData(User user);
    }
}