using CurrencyExposure.Repository.Xero;
using Microsoft.Practices.Unity;
using Xero.Api.Infrastructure.Interfaces;

namespace CurrencyExposure.Repository
{
	public static class IocContainer
	{
		private static IUnityContainer _instance;

		static IocContainer()
		{
			_instance = new UnityContainer();
		}

		public static IUnityContainer Instance
		{
			get
			{
                _instance.RegisterType<IAccountRepository, AccountRepository>(new PerRequestLifetimeManager());
				_instance.RegisterType<IBillRepository, BillRepository>(new PerRequestLifetimeManager());
                _instance.RegisterType<IPurchaseOrderRepository, PurchaseOrderRepository>(new PerRequestLifetimeManager());
                _instance.RegisterType<ITokenRepository, TokenRepository>(new PerRequestLifetimeManager());
                _instance.RegisterType<ITokenStore, XeroSqlTokenStore>(new PerRequestLifetimeManager());
				return _instance;
			}
		}
	}
}

