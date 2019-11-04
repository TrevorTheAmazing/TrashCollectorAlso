namespace TrashCollectorAlso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class api1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "lat", c => c.Double(nullable: false));
            AddColumn("dbo.Customers", "lng", c => c.Double(nullable: false));
            DropColumn("dbo.Customers", "coordinates");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "coordinates", c => c.String());
            DropColumn("dbo.Customers", "lng");
            DropColumn("dbo.Customers", "lat");
        }
    }
}
