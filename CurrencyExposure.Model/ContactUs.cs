using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyExposure.Model
{
	public class ContactUs
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
		public string Email { get; set; }
		[StringLength(25)]
		public string Phone { get; set; }
		[Required]
		[StringLength(2500)]
		public string Comment { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
