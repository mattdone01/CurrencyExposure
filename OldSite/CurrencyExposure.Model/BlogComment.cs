using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using CurrencyExposure.Model.Helpers;

namespace CurrencyExposure.Model
{
	public class BlogComment: IValidatableObject
	{
		public BlogComment()
		{
			this.CreateDate = DateTime.UtcNow;
			this.Email = string.Empty;
		}

		[Required]
		public int Id { get; set; }
		[Required]
		[StringLength(25)]
		public string Title { get; set; }
		[Required]
		[StringLength(2500)]
		public string Comment { get; set; }
		[Required]
		[StringLength(25)]
		public string Name { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required]
		public DateTime CreateDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		[ScriptIgnore]
		public Blog Blog { get; set; }

		//Parent Child
		public int? ParentCommentId { get; set; }
		[ScriptIgnore]
		public virtual BlogComment ParentComment { get; set; }
		public virtual ICollection<BlogComment> ChildComments { get; set; }

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
