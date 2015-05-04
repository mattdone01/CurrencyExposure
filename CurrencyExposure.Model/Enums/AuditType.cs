using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExposure.Model.Enums
{
	public static class AuditType
	{
		public const string BillboardSave = "SaveBillboard";
		public const string BillboardPublished = "PublishBillboard";
		public const string UserLogin = "UserLoggedIn";
	}
}
