using System.Collections.Generic;
using CurrencyExposure.Model;
using CurrencyExposure.Model.DatabaseObjects;

namespace CurrencyExposure.Repository
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        public OperationStatus SavePOs(List<PurchaseOrder> bills)
        {
            return null;
        }

        public OperationStatus SavePo(PurchaseOrder bill)
        {
            return null;
        }
    }

    public interface IPurchaseOrderRepository
    {
        OperationStatus SavePOs(List<PurchaseOrder> bills);
        OperationStatus SavePo(PurchaseOrder bill);
    }
}
