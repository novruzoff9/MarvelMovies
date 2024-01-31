namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_animation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnimationComments",
                c => new
                    {
                        MovieCommentID = c.Int(nullable: false, identity: true),
                        MovieCommentText = c.String(),
                        CommentStatus = c.Boolean(nullable: false),
                        CommentDate = c.DateTime(nullable: false),
                        ID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MovieCommentID)
                .ForeignKey("dbo.Animations", t => t.ID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Animations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Image = c.String(maxLength: 100),
                        Description = c.String(),
                        Producer = c.String(maxLength: 50),
                        ReleaseDate = c.DateTime(nullable: false),
                        IMDB = c.String(maxLength: 10),
                        Universe = c.String(maxLength: 20),
                        Status = c.Boolean(nullable: false),
                        Broadcast = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnimationComments", "UserID", "dbo.Users");
            DropForeignKey("dbo.AnimationComments", "ID", "dbo.Animations");
            DropIndex("dbo.AnimationComments", new[] { "UserID" });
            DropIndex("dbo.AnimationComments", new[] { "ID" });
            DropTable("dbo.Animations");
            DropTable("dbo.AnimationComments");
        }
    }
}
