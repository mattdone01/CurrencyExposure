using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyExposure.Model.Enums;

namespace CurrencyExposure.Model
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
