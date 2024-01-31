namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_usermessage_status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserMessages", "Status", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserMessages", "Status");
        }
    }
}
