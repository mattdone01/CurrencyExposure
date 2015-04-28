using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyExposure.Model.Enums;

namespace CurrencyExposure.Model
{
	public abstract class Invoice
	{
		public abstract InvoiceType InvoiceType { get; }
		public string InvoiceId { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime DueDate { get; set; }
		public string Currency { get; set; }
		public decimal Amount { get; set; }
		public decimal CurrencyRate { get; set; }	
	}
}
