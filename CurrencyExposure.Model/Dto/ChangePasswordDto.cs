﻿namespace CurrencyExposure.Model.Dto
{
	public class ChangePasswordDto
	{
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }
		public string NewPasswordConfirm { get; set; }
	}
}
