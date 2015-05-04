using System;
using CurrencyExposure.Model.Enums;

namespace CurrencyExposure.Model.DatabaseObjects
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
