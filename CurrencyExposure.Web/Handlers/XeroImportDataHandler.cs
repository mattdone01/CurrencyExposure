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
        public string _baseUrl = "";
        private readonly IAuthenticator _authenticator;
        private readonly IBillRepository _billRepository;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public XeroImportDataHandler(IAuthenticator authenticator, IBillRepository billRepository, IPurchaseOrderRepository purchaseOrderRepository)
        {
            _authenticator = authenticator;
            _billRepository = billRepository;
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public OperationStatus ImportData(User user)
        {
            var consumer = new Consumer(user.Company.ConsumerKey, user.Company.ConsumerSecret);
            var apiUser = new ApiUser();
            apiUser.Name = user.EmailAddress;
            apiUser.OrganisationId = user.Company.OrganisationId;

            var coreApi = new XeroCoreApi(_baseUrl, _authenticator, consumer, apiUser, new DefaultMapper(), new DefaultMapper());

            var invoices = coreApi.Invoices.Find().ToList();
            var pos = new List<PurchaseOrder>();
            var bills = new List<Bill>();
            foreach (var invoice in invoices)
            {
                if (invoice.Type == InvoiceType.AccountsReceivable)
                    bills.Add(new Bill()
                    {
                        InvoiceId = invoice.Id.ToString(),
                        CreateDate = invoice.Date.GetValueOrDefault(DateTime.MaxValue),
                        DueDate = invoice.DueDate.GetValueOrDefault(DateTime.MaxValue),
                        Amount = invoice.AmountDue.GetValueOrDefault(0),
                        Currency = invoice.CurrencyCode,
                        CurrencyRate = invoice.CurrencyRate.GetValueOrDefault(0)
                    });
                else if (invoice.Type == InvoiceType.AccountsPayable)
                    pos.Add(new PurchaseOrder()
                    {
                        InvoiceId = invoice.Id.ToString(),
                        CreateDate = invoice.Date.GetValueOrDefault(DateTime.MaxValue),
                        DueDate = invoice.DueDate.GetValueOrDefault(DateTime.MaxValue),
                        Amount = invoice.AmountDue.GetValueOrDefault(0),
                        Currency = invoice.CurrencyCode,
                        CurrencyRate = invoice.CurrencyRate.GetValueOrDefault(0)
                    });
            }
            
            var result = new OperationStatus();
            var billSaveResult = _billRepository.SaveBills(bills);
            if (!billSaveResult.Status)
            {
                result.Status = false;
                result.Message = " Failed to import bills. Please try again";
                return result;
            }
            var poSaveResult = _purchaseOrderRepository.SavePOs(pos);
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