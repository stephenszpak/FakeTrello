namespace FakeTrello.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TrelloUsers",
                c => new
                    {
                        TrelloUserId = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 60),
                        Username = c.String(maxLength: 60),
                        FullName = c.String(maxLength: 60),
                        BaseUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TrelloUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.BaseUser_Id)
                .Index(t => t.BaseUser_Id);
            
            CreateTable(
                "dbo.EntityLinks",
                c => new
                    {
                        EntityId = c.Int(nullable: false, identity: true),
                        CardId = c.Int(nullable: false),
                        TrelloUser_Id = c.String(maxLength: 128),
                        TrelloUser_TrelloUserId = c.Int(),
                    })
                .PrimaryKey(t => t.EntityId)
                .ForeignKey("dbo.AspNetUsers", t => t.TrelloUser_Id)
                .ForeignKey("dbo.TrelloUsers", t => t.TrelloUser_TrelloUserId)
                .ForeignKey("dbo.Cards", t => t.CardId, cascadeDelete: true)
                .Index(t => t.CardId)
                .Index(t => t.TrelloUser_Id)
                .Index(t => t.TrelloUser_TrelloUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        BoardId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        URL = c.String(maxLength: 255),
                        Owner_TrelloUserId = c.Int(),
                    })
                .PrimaryKey(t => t.BoardId)
                .ForeignKey("dbo.TrelloUsers", t => t.Owner_TrelloUserId)
                .Index(t => t.Owner_TrelloUserId);
            
            CreateTable(
                "dbo.Lists",
                c => new
                    {
                        ListId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Board_BoardId = c.Int(),
                    })
                .PrimaryKey(t => t.ListId)
                .ForeignKey("dbo.Boards", t => t.Board_BoardId)
                .Index(t => t.Board_BoardId);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        CardId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        ListId_ListId = c.Int(),
                    })
                .PrimaryKey(t => t.CardId)
                .ForeignKey("dbo.Lists", t => t.ListId_ListId)
                .Index(t => t.ListId_ListId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Boards", "Owner_TrelloUserId", "dbo.TrelloUsers");
            DropForeignKey("dbo.Lists", "Board_BoardId", "dbo.Boards");
            DropForeignKey("dbo.Cards", "ListId_ListId", "dbo.Lists");
            DropForeignKey("dbo.EntityLinks", "CardId", "dbo.Cards");
            DropForeignKey("dbo.TrelloUsers", "BaseUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.EntityLinks", "TrelloUser_TrelloUserId", "dbo.TrelloUsers");
            DropForeignKey("dbo.EntityLinks", "TrelloUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.Cards", new[] { "ListId_ListId" });
            DropIndex("dbo.Lists", new[] { "Board_BoardId" });
            DropIndex("dbo.Boards", new[] { "Owner_TrelloUserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.EntityLinks", new[] { "TrelloUser_TrelloUserId" });
            DropIndex("dbo.EntityLinks", new[] { "TrelloUser_Id" });
            DropIndex("dbo.EntityLinks", new[] { "CardId" });
            DropIndex("dbo.TrelloUsers", new[] { "BaseUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.Cards");
            DropTable("dbo.Lists");
            DropTable("dbo.Boards");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.EntityLinks");
            DropTable("dbo.TrelloUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
