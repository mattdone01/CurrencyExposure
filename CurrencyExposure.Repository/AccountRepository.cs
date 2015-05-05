using System;
using System.Data.Entity;
using System.Linq;
using CurrencyExposure.Model;
using CurrencyExposure.Model.DatabaseObjects;
using CurrencyExposure.Model.Dto;
using CurrencyExposure.Model.Enums;

namespace CurrencyExposure.Repository
{
	public class AccountRepository : IAccountRepository
	{
		public OperationStatus<User> ValidateUser(string userName, string password)
		{
			var result = new OperationStatus<User>();
			try
			{
				using (var context = new CurrencyExposureContext())
				{
					var user = context.User.Include(t => t.Company).SingleOrDefault(u => u.EmailAddress == userName && u.Password == password);
					result.OperationObject = user;
					result.Status = user != null;
				}
			}
			catch (Exception ex)
			{
				result.CreateFromException("Failed to validate user", ex);
				return result;
			}
			return result;
		}

        public OperationStatus<User> GetUser(string userName)
	    {
            var result = new OperationStatus<User>();
            try
            {
                using (var context = new CurrencyExposureContext())
                {
                    var user = context.User.Include(t => t.Company).SingleOrDefault(u => u.EmailAddress == userName);
                    result.OperationObject = user;
                    result.Status = user != null;
                }
            }
            catch (Exception ex)
            {
                result.CreateFromException(string.Format("Failed to get user {0}",userName), ex);
                return result;
            }
            return result;
	    }

		public void SaveLoginAudit(string userName)
		{
			using (var context = new CurrencyExposureContext())
			{
				var audit = new Audit
				{
					UserName = userName,
					AuditType = AuditType.UserLogin
				};
				context.Audit.Add(audit);
				context.SaveChanges();
			}
		}

		public OperationStatus ChangePassWord(ChangePasswordDto passwordDto, string userName)
		{
			var result = new OperationStatus();
			if (String.IsNullOrWhiteSpace(passwordDto.OldPassword))
			{
				result.Status = false;
				result.Message = "You must enter your old password";
				return result;
			}

			if (String.IsNullOrWhiteSpace(passwordDto.NewPassword))
			{
				result.Status = false;
				result.Message = "The new password cannot be blank";
				return result;
			}

			if (passwordDto.NewPassword != passwordDto.NewPasswordConfirm)
			{
				result.Status = false;
				result.Message = "The new password is not the same as the confirmation password.";
				return result;
			}

			var existingPassword = string.Empty;
			using (var context = new CurrencyExposureContext())
			{
				existingPassword = context.User.Single(u => u.EmailAddress == userName).Password;
			}

			if (passwordDto.OldPassword != existingPassword)
			{
				result.Status = false;
				result.Message = "The old password is incorrect";
				return result;
			}

			using (var context = new CurrencyExposureContext())
			{
				var user = context.User.Single(u => u.EmailAddress == userName);
				user.Password = passwordDto.NewPassword;
				context.User.Attach(user);
				context.Entry(user).State = EntityState.Modified;
				context.SaveChanges();
			}

			result.Status = true;
			result.Message = "Password has been successfully changed.";
			return result;
		}
	}

	public interface IAccountRepository
	{
		void SaveLoginAudit(string userName);
		OperationStatus ChangePassWord(ChangePasswordDto passwordDto, string userName);
		OperationStatus<User> ValidateUser(string userName, string password);
	    OperationStatus<User> GetUser(string userName);
	}
}
