using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyExposure.Repository;
using CurrencyExposure.Repository.Migrations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyExposure.Tests
{
	[TestClass]
	public class BlogTests
	{
		[ClassInitialize]
		public static void ClassInitialize(TestContext testContext)
		{
			Database.SetInitializer<CurrencyExposureContext>(null);
		}

		[TestMethod]
		public void GetBlogTest()
		{
			var br = new BlogRepository();
			var blog = br.GetBlog(1);
			Assert.AreEqual("Hedging Policies", blog.BlogCategory.CategoryName);
			Assert.AreEqual("Matt Done", blog.BlogAuthor.AuthorName);
		}

		[TestMethod]
		public void GetBlogCommentTest()
		{
			var br = new BlogRepository();
			var blog = br.GetBlog(1);
			Assert.AreEqual("Hedging Policies", blog.BlogCategory.CategoryName);
			Assert.AreEqual("This is a test Comment 1", blog.BlogComments.First().Comment);
		}

		[TestMethod]
		public void GetBlogWithChildCommentsTest()
		{
			var br = new BlogRepository();
			var blog = br.GetBlog(2);
			Assert.AreEqual("Products", blog.BlogCategory.CategoryName);
			Assert.AreEqual("This is a test Comment 2", blog.BlogComments.First().Comment);
			Assert.AreEqual("This is a child reply to test Comment 2", blog.BlogComments.First().ChildComments.First().Comment);
		}
	}
}
