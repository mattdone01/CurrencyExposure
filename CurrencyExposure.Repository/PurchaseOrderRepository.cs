using System;
using System.Collections.Generic;
using CurrencyExposure.Model;
using CurrencyExposure.Model.DatabaseObjects;
using System.Linq;

namespace CurrencyExposure.Repository
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        public OperationStatus SavePOs(List<PurchaseOrder> pos, string organisationId)
        {
			var result = new OperationStatus<User>();
			try
			{
				using (var context = new CurrencyExposureContext())
				{
					context.PurchaseOrder.RemoveRange(context.PurchaseOrder.Where(c => c.Id > 0 && c.OrganisationId == organisationId));
					context.PurchaseOrder.AddRange(pos);
					context.SaveChanges();
				}
				result.Status = true;
			}
			catch (Exception ex)
			{
				result.CreateFromException("Failed to Save Purchase Orders", ex);
				return result;
			}
			return result;
        }

        public OperationStatus SavePo(PurchaseOrder po)
        {
			var result = new OperationStatus<User>();
			try
			{
				using (var context = new CurrencyExposureContext())
				{
					context.PurchaseOrder.Remove(context.PurchaseOrder.First(c => c.Id == po.Id));
					context.PurchaseOrder.Add(po);
					context.SaveChanges();
				}
				result.Status = true;
			}
			catch (Exception ex)
			{
				result.CreateFromException("Failed to Save Purchase Order", ex);
				return result;
			}
			return result;
        }
    }

    public interface IPurchaseOrderRepository
    {
		OperationStatus SavePOs(List<PurchaseOrder> pos, string organisationId);
        OperationStatus SavePo(PurchaseOrder po);
    }
}
