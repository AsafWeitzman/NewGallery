namespace NewGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ArtistID = c.Int(nullable: false, identity: true),
                        ArtistName = c.String(),
                        Email = c.String(),
                        FavoriteStyle = c.String(),
                        Rate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ArtistID);
            
            CreateTable(
                "dbo.Paints",
                c => new
                    {
                        PaintID = c.Int(nullable: false, identity: true),
                        CreateDate = c.String(),
                        Size = c.String(),
                        Price = c.Int(nullable: false),
                        Type = c.String(),
                        ArtistID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaintID)
                .ForeignKey("dbo.Artists", t => t.ArtistID, cascadeDelete: true)
                .Index(t => t.ArtistID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustName = c.String(),
                        Email = c.String(),
                        Gender = c.String(),
                        PhoneNum = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Paints", "ArtistID", "dbo.Artists");
            DropIndex("dbo.Paints", new[] { "ArtistID" });
            DropTable("dbo.Customers");
            DropTable("dbo.Paints");
            DropTable("dbo.Artists");
        }
    }
}
