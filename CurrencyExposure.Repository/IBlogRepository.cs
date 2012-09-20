using System.Collections.Generic;
using System.Linq;
using CurrencyExposure.Model;

namespace CurrencyExposure.Repository
{
	public interface IBlogRepository
	{
		Blog GetBlog(int id);
		List<BlogSummaryDto> GetBlogSummaries(int count = 5);
	}
}