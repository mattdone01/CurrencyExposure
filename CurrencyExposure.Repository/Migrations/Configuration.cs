using System.Threading;
using CurrencyExposure.Model;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace CurrencyExposure.Repository.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<CurrencyExposure.Repository.CurrencyExposureContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

		protected override void Seed(CurrencyExposure.Repository.CurrencyExposureContext context)
		{
			if (AutomaticMigrationsEnabled)
				BlogArticleMigrations.SeedBlogs(context);
		}
    }
}
