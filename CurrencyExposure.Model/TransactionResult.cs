using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExposure.Model
{
	public class TransactionResult
	{
		public TransactionResult(bool success)
		{
			_success = success;
		}

		private bool _success = true;
		private string _errorText;

		public bool Success
		{
			get { return _success;}
		}

		public string ErrorText
		{
			get { return _errorText; }
			set
			{
				_errorText = value;
				_success = string.IsNullOrEmpty(ErrorText);
			}
		}

		public TransactionResult()
		{
			ErrorText = string.Empty;
		}
	}
}
