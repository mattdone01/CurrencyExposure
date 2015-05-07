using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyExposure.Model;
using RestSharp;

namespace CurrencyExposure.Repository
{
	public class RateRepository
	{
		public OperationStatus LoadRates(string currency)
		{
			var client = new RestClient("http://openexchangerates.org/api/latest.json?app_id=d65635c545674167a0483e1fa9659f33");
		}
	}
}
