using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyExposure.Model.DatabaseObjects
{
	public class Audit
	{
		public Audit()
		{
			AuditDate = DateTime.Now;
		}
		public int Id { get; set; }
		[Required]
		public string UserName { get; set; }
		[Required]
		public DateTime AuditDate { get; set; }
		public string RssDetails { get; set; }
		[Required]
		public string AuditType { get; set; }
	}
}
