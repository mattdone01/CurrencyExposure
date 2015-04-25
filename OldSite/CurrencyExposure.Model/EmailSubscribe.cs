using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CurrencyExposure.Model.Helpers;

namespace CurrencyExposure.Model
{
	public class EmailSubscribe: IValidatableObject
	{
		public EmailSubscribe()
		{
			this.CreateDate = DateTime.UtcNow;
		}

		public int Id { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		public DateTime CreateDate { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var results = new List<ValidationResult>();
			if(!Email.EmailAddressValidate())
			{
				results.Add(new ValidationResult("Email Address is invalid"));
			}

			return results;
		}
	}
}
