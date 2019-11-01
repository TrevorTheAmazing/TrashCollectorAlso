namespace TrashCollectorAlso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thing1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customers", "monthlyCharge");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "monthlyCharge", c => c.Double(nullable: false));
        }
    }
}
