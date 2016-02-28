namespace VoucherWorld.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migname : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NormalUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NormalUsers", t => t.NormalUserId)
                .Index(t => t.NormalUserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        UserType = c.Int(nullable: false),
                        Email = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        RegistrationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        NormalUserId = c.Int(nullable: false),
                        RouteId = c.Int(nullable: false),
                        GiftCode = c.String(),
                        EnrollDate = c.DateTime(nullable: false),
                        EnrollStatus = c.Int(nullable: false),
                        GiftCodeStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.NormalUserId, t.RouteId })
                .ForeignKey("dbo.NormalUsers", t => t.NormalUserId)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .Index(t => t.NormalUserId)
                .Index(t => t.RouteId);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsHidden = c.Boolean(nullable: false),
                        Category = c.Int(nullable: false),
                        PlaceIcon = c.String(),
                        MerchantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Merchants", t => t.MerchantId, cascadeDelete: true)
                .Index(t => t.MerchantId);
            
            CreateTable(
                "dbo.Gifts",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        GiftName = c.String(),
                        StockAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Routes", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Merchants",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        Website = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MerchantManagers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Longitude = c.Double(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Altitude = c.Double(nullable: false),
                        MerchantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Merchants", t => t.MerchantId, cascadeDelete: true)
                .Index(t => t.MerchantId);
            
            CreateTable(
                "dbo.RoutePlaces",
                c => new
                    {
                        RouteId = c.Int(nullable: false),
                        PlaceId = c.Int(nullable: false),
                        AddedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.RouteId, t.PlaceId })
                .ForeignKey("dbo.Places", t => t.PlaceId)
                .ForeignKey("dbo.Routes", t => t.RouteId)
                .Index(t => t.PlaceId)
                .Index(t => t.RouteId);
            
            CreateTable(
                "dbo.ScalingQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.OpenEndedQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Routes", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.MerchantManagers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.MerchantClients",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        MerchantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .ForeignKey("dbo.Merchants", t => t.MerchantId)
                .Index(t => t.Id)
                .Index(t => t.MerchantId);
            
            CreateTable(
                "dbo.NormalUsers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        IsFacebookUser = c.Boolean(nullable: false),
                        Point = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.OpenEndedAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Content = c.String(),
                        OpenEndedQuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.Id)
                .ForeignKey("dbo.OpenEndedQuestions", t => t.OpenEndedQuestionId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.OpenEndedQuestionId);
            
            CreateTable(
                "dbo.ScalingAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Content = c.Int(nullable: false),
                        ScalingQuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.Id)
                .ForeignKey("dbo.ScalingQuestions", t => t.ScalingQuestionId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.ScalingQuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScalingAnswers", "ScalingQuestionId", "dbo.ScalingQuestions");
            DropForeignKey("dbo.ScalingAnswers", "Id", "dbo.Answers");
            DropForeignKey("dbo.OpenEndedAnswers", "OpenEndedQuestionId", "dbo.OpenEndedQuestions");
            DropForeignKey("dbo.OpenEndedAnswers", "Id", "dbo.Answers");
            DropForeignKey("dbo.NormalUsers", "Id", "dbo.Users");
            DropForeignKey("dbo.MerchantClients", "MerchantId", "dbo.Merchants");
            DropForeignKey("dbo.MerchantClients", "Id", "dbo.Users");
            DropForeignKey("dbo.MerchantManagers", "Id", "dbo.Users");
            DropForeignKey("dbo.Answers", "NormalUserId", "dbo.NormalUsers");
            DropForeignKey("dbo.Enrollments", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.OpenEndedQuestions", "Id", "dbo.Routes");
            DropForeignKey("dbo.Routes", "MerchantId", "dbo.Merchants");
            DropForeignKey("dbo.Places", "MerchantId", "dbo.Merchants");
            DropForeignKey("dbo.ScalingQuestions", "Id", "dbo.Places");
            DropForeignKey("dbo.RoutePlaces", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.RoutePlaces", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Merchants", "Id", "dbo.MerchantManagers");
            DropForeignKey("dbo.Gifts", "Id", "dbo.Routes");
            DropForeignKey("dbo.Enrollments", "NormalUserId", "dbo.NormalUsers");
            DropIndex("dbo.ScalingAnswers", new[] { "ScalingQuestionId" });
            DropIndex("dbo.ScalingAnswers", new[] { "Id" });
            DropIndex("dbo.OpenEndedAnswers", new[] { "OpenEndedQuestionId" });
            DropIndex("dbo.OpenEndedAnswers", new[] { "Id" });
            DropIndex("dbo.NormalUsers", new[] { "Id" });
            DropIndex("dbo.MerchantClients", new[] { "MerchantId" });
            DropIndex("dbo.MerchantClients", new[] { "Id" });
            DropIndex("dbo.MerchantManagers", new[] { "Id" });
            DropIndex("dbo.Answers", new[] { "NormalUserId" });
            DropIndex("dbo.Enrollments", new[] { "RouteId" });
            DropIndex("dbo.OpenEndedQuestions", new[] { "Id" });
            DropIndex("dbo.Routes", new[] { "MerchantId" });
            DropIndex("dbo.Places", new[] { "MerchantId" });
            DropIndex("dbo.ScalingQuestions", new[] { "Id" });
            DropIndex("dbo.RoutePlaces", new[] { "RouteId" });
            DropIndex("dbo.RoutePlaces", new[] { "PlaceId" });
            DropIndex("dbo.Merchants", new[] { "Id" });
            DropIndex("dbo.Gifts", new[] { "Id" });
            DropIndex("dbo.Enrollments", new[] { "NormalUserId" });
            DropTable("dbo.ScalingAnswers");
            DropTable("dbo.OpenEndedAnswers");
            DropTable("dbo.NormalUsers");
            DropTable("dbo.MerchantClients");
            DropTable("dbo.MerchantManagers");
            DropTable("dbo.OpenEndedQuestions");
            DropTable("dbo.ScalingQuestions");
            DropTable("dbo.RoutePlaces");
            DropTable("dbo.Places");
            DropTable("dbo.Merchants");
            DropTable("dbo.Gifts");
            DropTable("dbo.Routes");
            DropTable("dbo.Enrollments");
            DropTable("dbo.Users");
            DropTable("dbo.Answers");
        }
    }
}
