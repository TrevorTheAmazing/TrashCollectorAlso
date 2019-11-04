namespace TrashCollectorAlso.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class api : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "coordinates", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "coordinates");
        }
    }
}
