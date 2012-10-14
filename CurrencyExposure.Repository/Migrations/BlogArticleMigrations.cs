using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CurrencyExposure.Model;
using CurrencyExposure.Repository.Properties;

namespace CurrencyExposure.Repository.Migrations
{
	public class BlogArticleMigrations
	{
		public static void SeeBlogs(CurrencyExposure.Repository.CurrencyExposureContext context)
		{
			//Add Blog Authors
			var mtAuthor = new BlogAuthor
			{
				AuthorName = "Matt Tyrrell",
				Email = "matt.tyrrell@currencyexposure.com"
			};
			var mdAuthor = new BlogAuthor
			{
				AuthorName = "Matt Done",
				Email = "matt.done@currencyexposure.com"
			};

			context.BlogAuthors.AddOrUpdate(
				p => p.AuthorName, mtAuthor, mdAuthor);

			//Add Categories
			var myHedgingCategory = new BlogCategory
			{
				CategoryName = "Hedging Policies",
			};


			var myProductCategory = new BlogCategory
			{
				CategoryName = "Products",
			};



			var myGeneralCategory = new BlogCategory
			{
				CategoryName = "General",
			};

			var myMarketsCategory = new BlogCategory
			{
				CategoryName = "General",
			};

			context.BlogCategorys.AddOrUpdate(
				p => p.CategoryName, myHedgingCategory, myProductCategory,
				new BlogCategory
				{
					CategoryName = "Foreign Exchange Providers",
				},
				new BlogCategory
				{
					CategoryName = "Payments",
				}
				);

			//Add Blog
			var blog1 = new Blog
			{
				Title = "Confessions of an FX Salesman",
				Article = Resources.BlogArticle1,
				BlogSummary = BlogArticle1Summary,
				BlogAuthor = mtAuthor,
				BlogCategory = myGeneralCategory,
				Tag = "Hedging, Risk, Exposure",
				CreateDate = new DateTime(2012, 09, 12, 15, 43, 31)
			};

			var blog2 = new Blog
			{
				Title = "Commonly used Hedging Policies - Part 1",
				Article = Resources.BlogArticle2,
				BlogSummary = BlogArticle2Summary,
				BlogAuthor = mtAuthor,
				BlogCategory = myHedgingCategory,
				Tag = "Payments, Risk, Exposure",
				CreateDate = new DateTime(2012, 09, 14, 10, 45, 12)
			};

			var blog3 = new Blog
			{
				Title = "Understanding Forward Exchange Contracts",
				Article = Resources.BlogArticle3,
				BlogSummary = BlogArticle3Summary,
				BlogAuthor = mtAuthor,
				BlogCategory = myProductCategory,
				Tag = "Payments, Risk, Exposure",
				CreateDate = new DateTime(2012,09,15,10,33,21)
			};

			var blog4 = new Blog
			{
				Title = "FX margins and the Interbank Rate explained",
				Article = Resources.BlogArticle4,
				BlogSummary = BlogArticle4Summary,
				BlogAuthor = mtAuthor,
				BlogCategory = myMarketsCategory,
				Tag = "Payments, Risk, Exposure",
				CreateDate = new DateTime(2012,09,20,10,54,43)
			};


			var blog5 = new Blog
			{
				Title = "Currency Options – How are they really priced?",
				Article = Resources.BlogArticle5,
				BlogSummary = BlogArticle5Summary,
				BlogAuthor = mtAuthor,
				BlogCategory = myProductCategory,
				Tag = "Payments, Risk, Exposure",
				CreateDate = new DateTime(2012,09,24,9,01,22)
			};

			context.Blogs.AddOrUpdate(p => p.Title, blog1, blog2, blog3);

			context.Blogs.AddOrUpdate(p => p.Title, blog4, blog5);

			//Add Blog Comments
			//var blogComment1 = new BlogComment
			//{
			//	Blog = blog1,
			//	Title = "This is a test Comment 1",
			//	Comment = "This is a test Comment 1",
			//	Name = "MattDone",
			//	Email = "mattdone@gmail.com"
			//};

			//var blogComment2 = new BlogComment
			//{
			//	Blog = blog2,
			//	Title = "This is a test Comment 2",
			//	Comment = "This is the content of the test comment",
			//	Name = "MattDone",
			//	Email = "mattdone@gmail.com"
			//};

			//context.BlogComments.AddOrUpdate(p => new { p.Comment, p.Name }, blogComment1);
			//context.BlogComments.AddOrUpdate(p => new { p.Comment, p.Name }, blogComment2);
			//context.BlogComments.AddOrUpdate(p => new { p.Comment, p.Name },
			//								 new BlogComment
			//								 {
			//									 Blog = blog2,
			//									 Title = "This is a test Comment",
			//									 Comment = "This is a child reply to test Comment 2",
			//									 Name = "MattTyrrell",
			//									 Email = "MT@gmail.com",
			//									 ParentComment = blogComment2
			//								 });
		}

		public const string BlogArticle1Summary = @"<p>We started this blog because we have sold foreign currency solutions to the SME and Corporate segment in Australia and have a very good understanding of the problems that businesses have in managing their foreign currency exposure....</p>";
		public const string BlogArticle2Summary = @"<p>For many CFO&rsquo;s a corporate hedging policy is not existent and decisions about when to hedge and how much are loosely proscribed. However, having a formal and objective policy that is signed off by a board of Director&rsquo;s can be very advantageous when questions are asked about FX losses....</p>";
		public const string BlogArticle3Summary = @"<p>Forward Exchange Contracts (FEC) are by far the most commonly used instrument to hedge currency exposure globally.&nbsp; According to the Bank of International Settlements, 44% of daily FX turnover is made up from FEC&rsquo;s, versus spot 37%....</p>";
		public const string BlogArticle4Summary = @"<p>The concepts in this article are simple but amazingly misunderstood by most participants in currency markets because FX providers are reticent to explain in it any great detail how they make money from their clients.&nbsp; Have you ever wondered why your bank or FX provider more often than do not display their rates...</p>";
		public const string BlogArticle5Summary = @"<p>The team here at CurrencyExposure.com are not adverse to the use of currency options in a hedging strategy.&nbsp;&nbsp;Options can be used effectively in conjunction with other products when taking a portfolio approach to hedge.&nbsp;&nbsp;However we are a cynical bunch over here and recommend that buyers beware...</p>";
	}
}
