using CurrencyExposure.Model.Enums;

namespace CurrencyExposure.Model.DatabaseObjects
{
	public class Bill: Invoice
	{
		public override InvoiceType InvoiceType
		{
			get
			{
				return InvoiceType.Bill;
			}
		}
	}
}
