﻿using System.Data.Entity;
using System.Linq;
using CurrencyExposure.Model;
using CurrencyExposure.Repository;
using CurrencyExposure.Repository.Helpers;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace CurrencyExposure.Tests
{
	[TestClass]
	public class BlogTests
	{
		private static IBlogRepository _blogRepo;
		[ClassInitialize]
		public static void ClassInitialize(TestContext testContext)
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<CurrencyExposureContext, Migrations.TestConfiguration>());
			var emailHelper = A.Fake<IEmailHelper>();
			A.CallTo(() => emailHelper.SendEmail(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored));
			_blogRepo = new BlogRepository(emailHelper);
		}
		
		[TestMethod]
		public void GetBlogTest()
		{
			var blog = _blogRepo.GetBlog(1);
			blog.BlogCategory.CategoryName.ShouldBe("General");
			blog.BlogAuthor.AuthorName.ShouldBe("Matt Jones");
		}

		[TestMethod]
		public void GetBlogCommentTest()
		{
			var blog = _blogRepo.GetBlog(1);
			blog.BlogCategory.CategoryName.ShouldBe("General");
			blog.BlogComments.First().Comment.ShouldBe("This is a test Comment 1");
		}

		[TestMethod]
		public void GetBlogSummaryCollection()
		{
			var blogSummaries = _blogRepo.GetBlogSummaries(4);
			blogSummaries.Count.ShouldBe(4);
		}

		[TestMethod]
		public void CanGetCommentsList()
		{
			var blogComments = _blogRepo.GetCommentsList(3);
			blogComments.Result.Count.ShouldBe(3);
		}

		[TestMethod]
		public void CanGetArticleList()
		{
			var blogArticles = _blogRepo.GetArticlesList(3);
			blogArticles.Result.Count.ShouldBe(3);
		}

		[TestMethod]
		public void CanSaveNewComment()
		{
			var comment = new BlogCommentDto
				              {
					              BlogId = 1,
					              Email = "mattdone@gmail.com",
					              Name = "Matt",
					              Title = "This is a blog Title",
					              Comment = "This is the comment"
				              };
			var result = _blogRepo.SaveComments(comment);
			result.Status.ShouldBe(true);
		}

		[TestMethod]
		public void CannotSaveNewCommentWithNoEmail()
		{
			var comment = new BlogCommentDto
				              {
					              BlogId = 1,
					              Name = "Matt",
					              Title = "This is a blog Title",
					              Comment = "This is the comment"
				              };
			var result = _blogRepo.SaveComments(comment);
			result.Status.ShouldBe(false);
		}

		[TestMethod]
		public void SaveNewContactUs()
		{
			var contactUs = new ContactUs
				                {
					                Name = "Matt",
					                Phone = "0419117335",
					                ProductEnquiry = true,
					                Email = "mattdone@gmail.com",
					                Company = "MyCompany",
					                Comment = "This is a test comment"
				                };

			var result = _blogRepo.SaveContactUs(contactUs);
			result.Status.ShouldBe(true);
		}

		[TestMethod]
		public void SaveFailedNewContactUs()
		{
			var contactUs = new ContactUs
			{
				Name = "Matt",
				Phone = "0419117335",
				ProductEnquiry = true,
				Company = "MyCompany",
				Comment = "This is a test comment"
			};

			var result = _blogRepo.SaveContactUs(contactUs);
			result.Status.ShouldBe(false);
		}

		[TestMethod]
		public void SaveEmailSubscription()
		{
			var emailSub = new EmailSubscribe {Email = "mattdone@gmail.com"};
			var result = _blogRepo.SaveEmailSubscription(emailSub);
			result.Status.ShouldBe(true);
		}

		[TestMethod]
		public void SaveFailedEmailSubscription()
		{
			var emailSub = new EmailSubscribe();
			var result = _blogRepo.SaveEmailSubscription(emailSub);
			result.Status.ShouldBe(false);
		}

		[TestMethod]
		public void CannotSaveBogusEmailSubscription()
		{
			var emailSub = new EmailSubscribe { Email = "mattdone#gmail.com" };
			var result = _blogRepo.SaveEmailSubscription(emailSub);
			result.Status.ShouldBe(false);
		}
	}
}
