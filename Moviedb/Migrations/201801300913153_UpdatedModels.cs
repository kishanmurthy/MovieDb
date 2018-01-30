namespace Moviedb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Actors", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Actors", "DOB", c => c.DateTime());
            AlterColumn("dbo.Movies", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Producers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Producers", "DOB", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Producers", "DOB", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Producers", "Name", c => c.String());
            AlterColumn("dbo.Movies", "Name", c => c.String());
            AlterColumn("dbo.Actors", "DOB", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Actors", "Name", c => c.String());
        }
    }
}
