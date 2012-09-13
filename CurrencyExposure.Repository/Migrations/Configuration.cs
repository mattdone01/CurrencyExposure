using CurrencyExposure.Model;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace CurrencyExposure.Repository.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<CurrencyExposure.Repository.CurrencyExposureContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

		protected override void Seed(CurrencyExposure.Repository.CurrencyExposureContext context)
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

			context.BlogCategorys.AddOrUpdate(
				p => p.CategoryName, myHedgingCategory,myProductCategory,
				new BlogCategory
					{
						CategoryName = "Foreign Exchange Providers",
					},
				new BlogCategory
					{
						CategoryName = "Payments",
					},
				new BlogCategory
					{
						CategoryName = "Markets",
					}
				);

			//Add Blog
			var blog1 = new Blog
				            {
					            Title = "This is the first blog post",
					            Article = "This is some data",
					            BlogAuthor = mdAuthor,
					            BlogCategory = myHedgingCategory,
					            Tag = "Hedging, Risk, Exposure"
				            };

			var blog2 = new Blog
				            {
					            Title = "This is the second blog post",
					            Article = "This is some data",
					            BlogAuthor = mtAuthor,
					            BlogCategory = myProductCategory,
					            Tag = "Payments, Risk, Exposure"
				            };

			context.Blogs.AddOrUpdate(p => p.Title, blog1, blog2);
			                          


			//Add Blog Comments
			var blogComment1 = new BlogComment
				                  {
					                  Blog = blog1,
					                  Comment = "This is a test Comment 1",
					                  Name = "MattDone",
					                  Email = "mattdone@gmail.com"
				                  };

			var blogComment2 = new BlogComment
			{
				Blog = blog2,
				Comment = "This is a test Comment 2",
				Name = "MattDone",
				Email = "mattdone@gmail.com"
			};

			context.BlogComments.AddOrUpdate(p => new {p.Comment, p.Name}, blogComment1);
			context.BlogComments.AddOrUpdate(p => new { p.Comment, p.Name }, blogComment2);
			context.BlogComments.AddOrUpdate(p => new {p.Comment, p.Name},
			                                 new BlogComment
				                                 {
					                                 Blog = blog2,
					                                 Comment = "This is a child reply to test Comment 2",
					                                 Name = "MattTyrrell",
					                                 Email = "MT@gmail.com",
													 ParentComment = blogComment2
				                                 });
		}
    }
}
