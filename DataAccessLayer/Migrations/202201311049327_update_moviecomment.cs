namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_moviecomment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MovieComments", "Spoiler", c => c.Boolean(nullable: false));
            AddColumn("dbo.MovieComments", "Like", c => c.Int(nullable: false));
            AddColumn("dbo.MovieComments", "DisLike", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MovieComments", "DisLike");
            DropColumn("dbo.MovieComments", "Like");
            DropColumn("dbo.MovieComments", "Spoiler");
        }
    }
}
