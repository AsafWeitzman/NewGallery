namespace NewGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingIMGurl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Paints", "ImgUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Paints", "ImgUrl");
        }
    }
}
