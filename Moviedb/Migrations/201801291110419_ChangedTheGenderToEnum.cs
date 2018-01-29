namespace Moviedb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTheGenderToEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.Producers", "Gender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Producers", "Gender");
            DropColumn("dbo.Actors", "Gender");
        }
    }
}
