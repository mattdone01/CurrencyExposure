using System.Web.Mvc;
using CurrencyExposure.Repository;
using CurrencyExposure.Web.Handlers;

namespace CurrencyExposure.Web.Controllers
{
   [Authorize]
	public class ImportDataController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IImportDataHandler _importDataHandler;

        public ImportDataController(IAccountRepository accountRepository, IImportDataHandler importDataHandler)
        {
            _accountRepository = accountRepository;
            _importDataHandler = importDataHandler;
        }

        // GET: ImportData
        public ActionResult Index()
        {
            var result = _accountRepository.GetUser(User.Identity.Name);
            if (!result.Status)
                return View("Error", result);

            var importResult = _importDataHandler.ImportData(result.OperationObject);
			if(importResult.TokenExpired)
				return RedirectToAction("Renew", "Token");

            if (!importResult.Status)
                return View("Error", result);

			return View(result);
        }
    }
}