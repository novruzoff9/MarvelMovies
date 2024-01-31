namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_movie_trailers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Trailer1", c => c.String());
            AddColumn("dbo.Movies", "Trailer2", c => c.String());
            AddColumn("dbo.Movies", "Trailer3", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "Trailer3");
            DropColumn("dbo.Movies", "Trailer2");
            DropColumn("dbo.Movies", "Trailer1");
        }
    }
}
