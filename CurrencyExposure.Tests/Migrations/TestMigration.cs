using System.Data.Entity.Migrations;
using CurrencyExposure.Repository;

namespace CurrencyExposure.Tests.Migrations
{

	internal sealed class TestConfiguration : DbMigrationsConfiguration<CurrencyExposureContext>
	{
		public TestConfiguration()
		{
			AutomaticMigrationsEnabled = true;
		}

		protected override void Seed(CurrencyExposureContext context)
		{
			if (AutomaticMigrationsEnabled)
				TestSeedMigrations.SeedBlogs(context);
		}
	}
}
