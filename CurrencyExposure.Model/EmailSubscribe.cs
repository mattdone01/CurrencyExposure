using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyExposure.Model
{
	public class EmailSubscribe
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
	}
}
