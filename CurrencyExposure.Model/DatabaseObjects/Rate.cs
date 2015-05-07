using System;

namespace CurrencyExposure.Model.DatabaseObjects
{
	public class ExchangeRate
	{
		public DateTime Date { get; set; }
		public string Currency { get; set; }
		public decimal Rate { get; set; }
	}
}
