namespace CurrencyExposure.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrgId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bills", "OrganisationId", c => c.String());
            AddColumn("dbo.PurchaseOrders", "OrganisationId", c => c.String());
            AlterColumn("dbo.OAuthTokens", "SessionExpiresAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OAuthTokens", "SessionExpiresAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.PurchaseOrders", "OrganisationId");
            DropColumn("dbo.Bills", "OrganisationId");
        }
    }
}
