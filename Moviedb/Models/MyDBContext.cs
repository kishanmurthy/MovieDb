namespace Moviedb.Models
{
    using System.Data.Entity;

    public class MyDbContext : DbContext
    {
        // Your context has been configured to use a 'MyDBContext' connection string from your application's
        // configuration file (App.config or Web.config). By default, this connection string targets the
        // 'Moviedb.Models.MyDBContext' database on your LocalDb instance.
        //
        // If you wish to target a different database and/or database provider, modify the 'MyDBContext'
        // connection string in the application configuration file.
        public MyDbContext()
            : base("name=MyDBContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }

        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}