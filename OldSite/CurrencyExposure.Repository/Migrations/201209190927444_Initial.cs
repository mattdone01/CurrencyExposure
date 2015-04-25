namespace CurrencyExposure.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Article = c.String(),
                        Tag = c.String(),
                        ExternalUrl = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                        BlogCategory_Id = c.Int(),
                        BlogAuthor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogCategories", t => t.BlogCategory_Id)
                .ForeignKey("dbo.BlogAuthors", t => t.BlogAuthor_Id)
                .Index(t => t.BlogCategory_Id)
                .Index(t => t.BlogAuthor_Id);
            
            CreateTable(
                "dbo.BlogCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlogAuthors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorName = c.String(),
                        Email = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlogComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                        ParentCommentId = c.Int(),
                        Blog_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.Blog_Id)
                .ForeignKey("dbo.BlogComments", t => t.ParentCommentId)
                .Index(t => t.Blog_Id)
                .Index(t => t.ParentCommentId);
            
            CreateTable(
                "dbo.BlogSocialLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SocialType = c.String(),
                        Uri = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        Blog_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.Blog_Id)
                .Index(t => t.Blog_Id);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        DisplayName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.BlogSocialLinks", new[] { "Blog_Id" });
            DropIndex("dbo.BlogComments", new[] { "ParentCommentId" });
            DropIndex("dbo.BlogComments", new[] { "Blog_Id" });
            DropIndex("dbo.Blogs", new[] { "BlogAuthor_Id" });
            DropIndex("dbo.Blogs", new[] { "BlogCategory_Id" });
            DropForeignKey("dbo.BlogSocialLinks", "Blog_Id", "dbo.Blogs");
            DropForeignKey("dbo.BlogComments", "ParentCommentId", "dbo.BlogComments");
            DropForeignKey("dbo.BlogComments", "Blog_Id", "dbo.Blogs");
            DropForeignKey("dbo.Blogs", "BlogAuthor_Id", "dbo.BlogAuthors");
            DropForeignKey("dbo.Blogs", "BlogCategory_Id", "dbo.BlogCategories");
            DropTable("dbo.UserProfile");
            DropTable("dbo.BlogSocialLinks");
            DropTable("dbo.BlogComments");
            DropTable("dbo.BlogAuthors");
            DropTable("dbo.BlogCategories");
            DropTable("dbo.Blogs");
        }
    }
}
