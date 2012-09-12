using CurrencyExposure.Model;

namespace CurrencyExposure.Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CurrencyExposure.Repository.CurrencyExposureContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CurrencyExposure.Repository.CurrencyExposureContext context)
        {
	        //Add Blog Authors
			context.BlogAuthors.AddOrUpdate(
		        p => p.AuthorName,
		        new BlogAuthor
			        {
				        AuthorName = "Matt Tyrrell",
				        Email = "matt.tyrrell@currencyexposure.com"
			        },
		        new BlogAuthor
			        {
				        AuthorName = "Matt Done",
				        Email = "matt.done@currencyexposure.com"
			        }
		        );

			//Add Categories
	        context.BlogCategorys.AddOrUpdate(
		        p => p.CategoryName,
		        new BlogCategory
			        {
				        CategoryName = "Hedging Policies",
			        },
		        new BlogCategory
			        {
				        CategoryName = "Products",
			        },
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

	        context.Blogs.AddOrUpdate(p => p.Title,
	            new Blog
		            {
			            Title = "This is the first blog post",
			            Article = "This is some data",
						BlogAuthor = context.BlogAuthors.FirstOrDefault(c => c.AuthorName == "Matt Tyrrell"),
			            BlogCategory =
				            context.BlogCategorys.FirstOrDefault(c => c.CategoryName == "Hedging Policies"),
			            Tag = "Hedging, Risk, Exposure"
		            },
	            new Blog
		            {
			            Title = "This is the second blog post",
			            Article = "This is some data",
			            BlogAuthor = context.BlogAuthors.FirstOrDefault(c => c.AuthorName == "Matt Done"),
			            BlogCategory =
							context.BlogCategorys.FirstOrDefault(c => c.CategoryName == "Payments"),
						Tag = "Payments, Risk, Exposure"
		            });

        }
    }
}
