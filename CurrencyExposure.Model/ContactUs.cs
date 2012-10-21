using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CurrencyExposure.Model.Helpers;

namespace CurrencyExposure.Model
{
	public class ContactUs: IValidatableObject
	{
		public ContactUs()
		{
			this.CreateDate = DateTime.UtcNow;
			this.Phone = string.Empty;
			this.Company = string.Empty;
		}

		public int Id { get; set; }
		[Required]
		[StringLength(25)]
		public string Name { get; set; }
		[StringLength(100)]
		public string Company { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[StringLength(25)]
		public string Phone { get; set; }
		[Required]
		[StringLength(2500)]
		public string Comment { get; set; }
		public bool ProductEnquiry { get; set; }
		public DateTime CreateDate { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var results = new List<ValidationResult>();
			if (!Email.EmailAddressValidate())
			{
				results.Add(new ValidationResult("Email Address is not valid"));
			}

			return results;
		}
	}
}
