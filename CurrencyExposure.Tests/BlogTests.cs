using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyExposure.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyExposure.Tests
{
	[TestClass]
	public class BlogTests
	{
		[TestMethod]
		public void GetBlogTest()
		{
			BlogRepository br = new BlogRepository();
			var blog = br.GetBlog(1);
		}
	}
}
