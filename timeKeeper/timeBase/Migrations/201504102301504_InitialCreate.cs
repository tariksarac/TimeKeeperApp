namespace timeBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Type = c.String(maxLength: 2),
                        Time = c.Double(nullable: false),
                        Note = c.String(),
                        Person = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Person, cascadeDelete: true)
                .Index(t => t.Person);
            
            CreateTable(
                "dbo.Details",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.Double(nullable: false),
                        Note = c.String(),
                        Status = c.String(maxLength: 2),
                        Day = c.Int(nullable: false),
                        Project = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Days", t => t.Day, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project, cascadeDelete: true)
                .Index(t => t.Day)
                .Index(t => t.Project);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 40),
                        LastName = c.String(maxLength: 40),
                        Email = c.String(),
                        Phone = c.String(maxLength: 20),
                        Role = c.Int(),
                        Team = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role)
                .ForeignKey("dbo.Teams", t => t.Team)
                .Index(t => t.Role)
                .Index(t => t.Team);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Days", "Person", "dbo.People");
            DropForeignKey("dbo.People", "Team", "dbo.Teams");
            DropForeignKey("dbo.People", "Role", "dbo.Roles");
            DropForeignKey("dbo.Details", "Project", "dbo.Projects");
            DropForeignKey("dbo.Details", "Day", "dbo.Days");
            DropIndex("dbo.People", new[] { "Team" });
            DropIndex("dbo.People", new[] { "Role" });
            DropIndex("dbo.Details", new[] { "Project" });
            DropIndex("dbo.Details", new[] { "Day" });
            DropIndex("dbo.Days", new[] { "Person" });
            DropTable("dbo.Teams");
            DropTable("dbo.Roles");
            DropTable("dbo.People");
            DropTable("dbo.Projects");
            DropTable("dbo.Details");
            DropTable("dbo.Days");
        }
    }
}
