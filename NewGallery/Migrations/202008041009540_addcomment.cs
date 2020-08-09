namespace NewGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcomment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Body = c.String(),
                        SentBy = c.String(),
                        Posted = c.DateTime(nullable: false),
                        Paint_PaintID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Paints", t => t.Paint_PaintID)
                .Index(t => t.Paint_PaintID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Paint_PaintID", "dbo.Paints");
            DropIndex("dbo.Comments", new[] { "Paint_PaintID" });
            DropTable("dbo.Comments");
        }
    }
}
