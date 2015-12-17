namespace timeBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sule20150412 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApiUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Secret = c.String(),
                        AppId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthTokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(),
                        Expiraton = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApiUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuthTokens", "User_Id", "dbo.ApiUsers");
            DropIndex("dbo.AuthTokens", new[] { "User_Id" });
            DropTable("dbo.AuthTokens");
            DropTable("dbo.ApiUsers");
        }
    }
}
