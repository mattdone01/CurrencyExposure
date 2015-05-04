using System.Data.Entity;
using CurrencyExposure.Model;
using CurrencyExposure.Model.DatabaseObjects;

namespace CurrencyExposure.Repository
{
	public class CurrencyExposureContext : DbContext, IDisposedTracker
	{
		public DbSet<Bill> Bill { get; set; }
		public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
		public DbSet<OAuthToken> OAuthToken { get; set; }
		public DbSet<Audit> Audit { get; set; }
		public DbSet<User> User { get; set; }

		public bool IsDisposed { get; set; }

		public CurrencyExposureContext()
		{
			Configuration.LazyLoadingEnabled = false;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//modelBuilder.Entity<BlogComment>()
			//	.HasMany(page => page.ChildComments)
			//	.WithOptional(child => child.ParentComment)
			//	.HasForeignKey(child => child.ParentCommentId);
		}

		protected override void Dispose(bool disposing)
		{
			IsDisposed = true;
			base.Dispose(disposing);
		}
	}

	public interface IDisposedTracker
	{
		bool IsDisposed { get; set; }
	}
}
