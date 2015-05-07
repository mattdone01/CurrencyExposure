using System;
using System.Collections.Generic;
using CurrencyExposure.Model;
using CurrencyExposure.Model.DatabaseObjects;
using System.Linq;

namespace CurrencyExposure.Repository
{
	public class BillRepository : IBillRepository
	{
        public OperationStatus SaveBills(List<Bill> bills, string organisationId)
	    {
			var result = new OperationStatus<User>();
			try
			{
				using (var context = new CurrencyExposureContext())
				{
					context.Bill.RemoveRange(context.Bill.Where(c => c.Id > 0 && c.OrganisationId == organisationId));
					context.Bill.AddRange(bills);
					context.SaveChanges();
				}
				result.Status = true;
			}
			catch (Exception ex)
			{
				result.CreateFromException("Failed to Save Bills", ex);
				return result;
			}
			return result;
	    }

        public OperationStatus SaveBill(Bill bill)
        {
			var result = new OperationStatus<User>();
			try
			{
				using (var context = new CurrencyExposureContext())
				{
					context.Bill.Remove(context.Bill.First(c => c.Id == bill.Id));
					context.Bill.Add(bill);
					context.SaveChanges();
				}
				result.Status = true;
			}
			catch (Exception ex)
			{
				result.CreateFromException("Failed to Save Bill", ex);
				return result;
			}
			return result;
        }
	}

	public interface IBillRepository
	{
		OperationStatus SaveBills(List<Bill> bills, string organisationId);
	    OperationStatus SaveBill(Bill bill);
	}
}