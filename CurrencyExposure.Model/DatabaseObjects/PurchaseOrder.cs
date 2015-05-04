using CurrencyExposure.Model.Enums;

namespace CurrencyExposure.Model.DatabaseObjects
{
	public class PurchaseOrder: Invoice
	{
		public override InvoiceType InvoiceType
		{
			get
			{
				return InvoiceType.PurchaseOrder;
			}
		}
	}
}
