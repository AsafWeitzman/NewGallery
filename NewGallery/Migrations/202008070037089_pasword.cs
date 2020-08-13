namespace NewGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pasword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Password", c => c.String());
            DropColumn("dbo.Accounts", "Passward");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Accounts", "Passward", c => c.String());
            DropColumn("dbo.Accounts", "Password");
        }
    }
}
