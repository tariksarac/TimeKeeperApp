namespace timeBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sulejoj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuthTokens", "Expiration", c => c.DateTime(nullable: false));
            DropColumn("dbo.AuthTokens", "Expiraton");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AuthTokens", "Expiraton", c => c.DateTime(nullable: false));
            DropColumn("dbo.AuthTokens", "Expiration");
        }
    }
}
