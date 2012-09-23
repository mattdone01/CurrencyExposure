﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExposure.Model
{
	public class CommentsListDto
	{
		public int Id { get; set; }
		public int BlogId { get; set; }
		public string Name { get; set; }
		public string Comment { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
	