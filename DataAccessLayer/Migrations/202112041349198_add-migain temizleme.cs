namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigaintemizleme : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movies", "RelaseDate");
            DropColumn("dbo.Series", "RelaseDate");
        }
        
        public override void Down()
        {
        }
    }
}
