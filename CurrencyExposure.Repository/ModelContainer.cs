using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExposure.Repository
{
	public static class ModelContainer
	{
		private static IUnityContainer _instance;

		static ModelContainer()
		{
			_instance = new UnityContainer();
		}

		public static IUnityContainer Instance
		{
			get
			{
				//_Instance.RegisterType<IAccountRepository, AccountRepository>(new HierarchicalLifetimeManager());
				return _instance;
			}
		}
	}
}
