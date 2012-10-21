using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyExposure.Model;

namespace CurrencyExposure.Repository
{
	public interface IBlogRepository
	{
		Blog GetBlog(int id);
		List<BlogSummaryDto> GetBlogSummaries(int count = 5);
		Task<List<CommentsListDto>> GetCommentsList(int count = 3);
		Task<List<BlogSummaryDto>> GetArticlesList(int count = 3);
		OperationStatus SaveComments(BlogCommentDto comment);
		OperationStatus SaveContactUs(ContactUs contactDetails);
		OperationStatus SaveEmailSubscription(EmailSubscribe emailSubDto);
		OperationStatus SaveBlog(BlogDto blogDto);
		OperationStatus DeleteBlog(int blogId);
	}
}