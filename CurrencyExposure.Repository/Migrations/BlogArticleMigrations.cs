using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyExposure.Model;

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
				Article = "This is some data",
				BlogAuthor = mtAuthor,
				BlogCategory = myGeneralCategory,
				Tag = "Hedging, Risk, Exposure"
			};

			var blog2 = new Blog
			{
				Title = "Commonly used Hedging Policies - Part 1",
				Article = "This is some data",
				BlogAuthor = mtAuthor,
				BlogCategory = myHedgingCategory,
				Tag = "Payments, Risk, Exposure"
			};

			var blog3 = new Blog
			{
				Title = "Understanding Forward Exchange Contracts",
				Article = "This is some data",
				BlogAuthor = mtAuthor,
				BlogCategory = myProductCategory,
				Tag = "Payments, Risk, Exposure"
			};

			var blog4 = new Blog
			{
				Title = "FX margins and the Interbank Rate explained",
				Article = "This is some data",
				BlogAuthor = mtAuthor,
				BlogCategory = myMarketsCategory,
				Tag = "Payments, Risk, Exposure"
			};

			context.Blogs.AddOrUpdate(p => p.Title, blog1, blog2, blog3);

			context.Blogs.AddOrUpdate(p => p.Title, blog4);

			//Add Blog Comments
			var blogComment1 = new BlogComment
			{
				Blog = blog1,
				Title = "This is a test Comment 1",
				Comment = "This is a test Comment 1",
				Name = "MattDone",
				Email = "mattdone@gmail.com"
			};

			var blogComment2 = new BlogComment
			{
				Blog = blog2,
				Title = "This is a test Comment 2",
				Comment = "This is the content of the test comment",
				Name = "MattDone",
				Email = "mattdone@gmail.com"
			};

			context.BlogComments.AddOrUpdate(p => new { p.Comment, p.Name }, blogComment1);
			context.BlogComments.AddOrUpdate(p => new { p.Comment, p.Name }, blogComment2);
			context.BlogComments.AddOrUpdate(p => new { p.Comment, p.Name },
											 new BlogComment
											 {
												 Blog = blog2,
												 Title = "This is a test Comment",
												 Comment = "This is a child reply to test Comment 2",
												 Name = "MattTyrrell",
												 Email = "MT@gmail.com",
												 ParentComment = blogComment2
											 });
		}
		
		public string BlogArticle1
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string BlogArticle2
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public  string BlogArticle3
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string BlogArticle4 		
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public const string BlogArticle1Summary = "";
		public const string BlogArticle2Summary = "";
		public const string BlogArticle3Summary = "";
		public const string BlogArticle4Summary = "";
	}
}
