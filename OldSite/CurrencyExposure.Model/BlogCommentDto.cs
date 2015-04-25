using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExposure.Model
{
	public class BlogCommentDto
	{
		[Required]
		public int BlogId { get; set; }
		[Required]
		[StringLength(25)]
		public string Name { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required]
		[StringLength(2500)]
		public string Comment { get; set; }
		[Required]
		[StringLength(25)]
		public string Title { get; set; }
	}
}
