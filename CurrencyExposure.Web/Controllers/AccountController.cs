using System.Web.Mvc;
using System.Web.Security;
using CurrencyExposure.Model.Dto;
using CurrencyExposure.Repository;

namespace CurrencyExposure.Web.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private readonly IAccountRepository _accountRepository;
		private readonly ITokenRepository _tokenRepo;

		public AccountController(IAccountRepository accountRepository, ITokenRepository tokenRepo)
		{
			_accountRepository = accountRepository;
			_tokenRepo = tokenRepo;
		}

		// GET: /Account/Login
		[AllowAnonymous]
		public ActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginDto user, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View(user);
			}

			var result = _accountRepository.ValidateUser(user.EmailAddress, user.Password);
			if (result.Status)
			{
				FormsAuthentication.SetAuthCookie(user.EmailAddress, false);
				_accountRepository.SaveLoginAudit(user.EmailAddress);
				if (result.OperationObject.Company == null)
				{
					//Need to create a brand new token.
                    return RedirectToAction("Index", "Token");
				}
				
				//Check if token exists and is still active.
				var tokenResult = _tokenRepo.GetToken(result.OperationObject.Company.OrganisationId);
				if (tokenResult.Status && tokenResult.OperationObject != null &&
				    !_tokenRepo.IsTokenExpired(tokenResult.OperationObject))
				{
					return RedirectToLocal(returnUrl);
				}

                return RedirectToAction("Renew", "Token");
			}

			ModelState.AddModelError("", "Invalid login attempt.");
			return View(user);
		}

		public ActionResult ChangePasswordView()
		{
			return View("ChangePassword", new ChangePasswordDto());
		}

		public ActionResult ChangePassword(ChangePasswordDto passwordDto)
		{
			var result = _accountRepository.ChangePassWord(passwordDto, User.Identity.Name);
			return Json(result);
		}

		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Login", "Account");
		}

		private ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			return RedirectToAction("Index", "Home");
		}
	}
}
