namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_movie_date : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movies", "RelaseDate");
            DropColumn("dbo.Series", "RelaseDate");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Series", "Date", c => c.String());
            AlterColumn("dbo.Movies", "Date", c => c.String());
        }
    }
}
