namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_Adminmessages_date : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdminMessages", "MessageDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdminMessages", "MessageDate");
        }
    }
}
