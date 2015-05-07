namespace CurrencyExposure.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitalMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Audits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        AuditDate = c.DateTime(nullable: false),
                        RssDetails = c.String(),
                        AuditType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Currency = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrencyRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OAuthTokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        OrganisationId = c.String(),
                        ConsumerKey = c.String(),
                        ConsumerSecret = c.String(),
                        TokenKey = c.String(),
                        TokenSecret = c.String(),
                        Session = c.String(),
                        ExpiresAt = c.DateTime(nullable: false),
                        SessionExpiresAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Currency = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrencyRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        CanPublish = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Company_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organisations", t => t.Company_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.Organisations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrganisationId = c.String(),
                        OrganisationName = c.String(),
                        ConsumerKey = c.String(),
                        ConsumerSecret = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Company_Id", "dbo.Organisations");
            DropIndex("dbo.Users", new[] { "Company_Id" });
            DropTable("dbo.Organisations");
            DropTable("dbo.Users");
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.OAuthTokens");
            DropTable("dbo.Bills");
            DropTable("dbo.Audits");
        }
    }
}
