namespace DAKI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nigger : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                        CurrentPoints = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        Person_PersonId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Person", t => t.Person_PersonId)
                .Index(t => t.Person_PersonId);
            
            CreateTable(
                "dbo.UserBuysPrize",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PrizeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId);
            
            CreateTable(
                "dbo.Prize",
                c => new
                    {
                        PrizeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Cost = c.Int(nullable: false),
                        Limit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PrizeId);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        BirthDate = c.DateTime(),
                        Adress = c.String(),
                    })
                .PrimaryKey(t => t.PersonId);
            
            CreateTable(
                "dbo.PersonHasJobInDep",
                c => new
                    {
                        PersonId = c.Int(nullable: false),
                        JobId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        StartingDate = c.DateTime(nullable: false),
                        Salary = c.Int(),
                    })
                .PrimaryKey(t => new { t.PersonId, t.JobId, t.DepartmentId })
                .ForeignKey("dbo.Department", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Job", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.JobId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        ParentId = c.Int(),
                        Rules = c.Boolean(),
                        Parent_DepartmentId = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentId)
                .ForeignKey("dbo.Department", t => t.Parent_DepartmentId)
                .Index(t => t.Parent_DepartmentId);
            
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Manages = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.JobId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Department", new[] { "Parent_DepartmentId" });
            DropIndex("dbo.PersonHasJobInDep", new[] { "PersonId" });
            DropIndex("dbo.PersonHasJobInDep", new[] { "JobId" });
            DropIndex("dbo.PersonHasJobInDep", new[] { "DepartmentId" });
            DropIndex("dbo.UserProfile", new[] { "Person_PersonId" });
            DropForeignKey("dbo.Department", "Parent_DepartmentId", "dbo.Department");
            DropForeignKey("dbo.PersonHasJobInDep", "PersonId", "dbo.Person");
            DropForeignKey("dbo.PersonHasJobInDep", "JobId", "dbo.Job");
            DropForeignKey("dbo.PersonHasJobInDep", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.UserProfile", "Person_PersonId", "dbo.Person");
            DropTable("dbo.Job");
            DropTable("dbo.Department");
            DropTable("dbo.PersonHasJobInDep");
            DropTable("dbo.Person");
            DropTable("dbo.Prize");
            DropTable("dbo.UserBuysPrize");
            DropTable("dbo.UserProfile");
        }
    }
}
