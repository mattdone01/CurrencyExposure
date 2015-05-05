using System.Collections.Generic;
using CurrencyExposure.Model;
using CurrencyExposure.Model.DatabaseObjects;

namespace CurrencyExposure.Repository
{
	public class BillRepository : IBillRepository
	{
        public OperationStatus SaveBills(List<Bill> bills)
	    {
            return null;
	    }

        public OperationStatus SaveBill(Bill bill)
        {
            return null;
        }
	}

	public interface IBillRepository
	{
	    OperationStatus SaveBills(List<Bill> bills);
	    OperationStatus SaveBill(Bill bill);
	}
}