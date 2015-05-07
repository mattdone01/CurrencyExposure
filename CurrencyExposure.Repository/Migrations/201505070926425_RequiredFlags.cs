namespace CurrencyExposure.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredFlags : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bills", "OrganisationId", c => c.String(nullable: false));
            AlterColumn("dbo.PurchaseOrders", "OrganisationId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PurchaseOrders", "OrganisationId", c => c.String());
            AlterColumn("dbo.Bills", "OrganisationId", c => c.String());
        }
    }
}
