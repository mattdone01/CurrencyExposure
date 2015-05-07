using System;
using System.ComponentModel.DataAnnotations;
using CurrencyExposure.Model.Enums;

namespace CurrencyExposure.Model.DatabaseObjects
{
	public abstract class Invoice
	{
		public int Id { get; set; }
		public abstract InvoiceType InvoiceType { get; }
		public string InvoiceId { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime DueDate { get; set; }
		public string Currency { get; set; }
		public decimal Amount { get; set; }
		public decimal CurrencyRate { get; set; }
		[Required]
		public string OrganisationId { get; set; }
	}
}
