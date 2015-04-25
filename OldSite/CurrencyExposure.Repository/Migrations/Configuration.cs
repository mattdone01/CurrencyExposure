using System.Data.Entity.Migrations;

namespace CurrencyExposure.Repository.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<CurrencyExposure.Repository.CurrencyExposureContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

		protected override void Seed(CurrencyExposure.Repository.CurrencyExposureContext context)
		{
			if (AutomaticMigrationsEnabled)
				BlogArticleMigrations.SeedBlogs(context);
		}
    }
}
