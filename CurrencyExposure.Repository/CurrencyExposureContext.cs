using System.Data.Entity;
using CurrencyExposure.Model;

namespace CurrencyExposure.Repository
{
	public class CurrencyExposureContext : DbContext, IDisposedTracker
	{
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<BlogAuthor> BlogAuthors { get; set; }
		public DbSet<BlogCategory> BlogCategorys { get; set; }
		public DbSet<BlogComment> BlogComments { get; set; }
		public DbSet<BlogSocialLink> BlogSocialLinks { get; set; }
		public DbSet<EmailSubscribe> EmailSubscription { get; set; }
		//public DbSet<UserProfile> UserProfiles { get; set; }
		public DbSet<ContactUs> ContactUs { get; set; }
		public bool IsDisposed { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<BlogComment>()
				.HasMany(page => page.ChildComments)
				.WithOptional(child => child.ParentComment)
				.HasForeignKey(child => child.ParentCommentId);
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