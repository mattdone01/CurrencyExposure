using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyExposure.Model.DatabaseObjects
{
	public class User
	{
		public User()
		{
			CreateDate = DateTime.Now;
			CanPublish = true;
		}

		public int Id { get; set; }
		[Required]
		public string EmailAddress { get; set; }
		[Required]
		public string Password { get; set; }
		public bool CanPublish { get; set; }
		[Required]
		public DateTime CreateDate { get; set; }
		public Organisation Company { get; set; }
	}
}
