namespace Moviedb.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedMoviePosterPath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "MoviePosterPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "MoviePosterPath");
        }
    }
}
