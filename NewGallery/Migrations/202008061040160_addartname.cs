namespace NewGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addartname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Paints", "artistname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Paints", "artistname");
        }
    }
}
