using Microsoft.Practices.Unity;

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
				_instance.RegisterType<ITokenRepository, TokenRepository>(new PerRequestLifetimeManager());
				_instance.RegisterType<IBillRepository, BillRepository>(new PerRequestLifetimeManager());
				_instance.RegisterType<ITokenRepository, TokenRepository>(new PerRequestLifetimeManager());
				return _instance;
			}
		}
	}
}

