using System.Text.RegularExpressions;

namespace CurrencyExposure.Model.Helpers
{
	public static class ValidationExtensions
	{
		public static bool EmailAddressValidate(this string emailaddress)
		{
			var re = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", RegexOptions.IgnoreCase);
			return re.IsMatch(emailaddress);
		}
	}
}
