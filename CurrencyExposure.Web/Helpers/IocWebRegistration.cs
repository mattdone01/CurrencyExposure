﻿using CurrencyExposure.Web.Controllers;
using CurrencyExposure.Web.Handlers;
using Microsoft.Practices.Unity;
using Xero.Api.Infrastructure.Interfaces;

namespace CurrencyExposure.Web.Helpers
{
    public static class IocWebRegistration
    {
        public static void RegisterMvcTypesForIoc(IUnityContainer container)
        {
            container.RegisterType<IImportDataHandler, XeroImportDataHandler>(new PerRequestLifetimeManager());
			container.RegisterType<IAuthenticator, XeroPublicAuthenticator>(new PerRequestLifetimeManager());
			container.RegisterType<IMvcAuthenticator, XeroPublicAuthenticator>(new PerRequestLifetimeManager());
        }
    }
}