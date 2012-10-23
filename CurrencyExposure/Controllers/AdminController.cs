using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CurrencyExposure.Model;
using CurrencyExposure.Repository;

namespace CurrencyExposure.Controllers
{
    public class AdminController : Controller
    {
        
		private readonly IBlogRepository _blogRepository;

		public AdminController(IBlogRepository blogRepository)
        {
	        _blogRepository = blogRepository;
        }

		public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult SaveBlog(BlogDto myBlogDto)
		{
			var result = new OperationStatus();
			if (ModelState.IsValid)
				result = _blogRepository.SaveBlog(myBlogDto);

			return Json(result);
		}

		[HttpPost]
		public ActionResult DeleteBlog(int blogId)
		{
			var result = new OperationStatus();
			if (blogId > 0)
				result = _blogRepository.DeleteBlog(blogId);

			return Json(result);
		}
		
		public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
		{
			string path = System.Configuration.ConfigurationManager.AppSettings["UploadPath"];
			var status = new OperationStatus();
			try
			{
				foreach (var file in files)
				{
					string filename = Path.GetFileName(file.FileName);
					file.SaveAs(Path.Combine(path, filename));
					status.Status = true;
				}
			}
			catch (Exception ex)
			{
				return Json(OperationStatus.CreateFromException("Failed to upload file", ex));
			}

			return Json(status);
		}
    }
}
