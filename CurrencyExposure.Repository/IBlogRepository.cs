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
		Task<List<CommentsListDto>> GetCommentsList(DateTime pageId, int count = 5);
		Task<List<BlogSummaryDto>> GetArticlesList(DateTime pageId, int count = 3);
	}
}