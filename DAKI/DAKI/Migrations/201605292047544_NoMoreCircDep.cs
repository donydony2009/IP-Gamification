namespace DAKI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoMoreCircDep : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserProfile", "Person_PersonId", "dbo.Person");
            DropForeignKey("dbo.PersonHasJobInDep", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.PersonHasJobInDep", "JobId", "dbo.Job");
            DropForeignKey("dbo.PersonHasJobInDep", "PersonId", "dbo.Person");
            DropForeignKey("dbo.Department", "Parent_DepartmentId", "dbo.Department");
            DropIndex("dbo.UserProfile", new[] { "Person_PersonId" });
            DropIndex("dbo.PersonHasJobInDep", new[] { "DepartmentId" });
            DropIndex("dbo.PersonHasJobInDep", new[] { "JobId" });
            DropIndex("dbo.PersonHasJobInDep", new[] { "PersonId" });
            DropIndex("dbo.Department", new[] { "Parent_DepartmentId" });
            AlterColumn("dbo.Department", "Title", c => c.String(nullable: false));
            DropColumn("dbo.UserProfile", "Person_PersonId");
            DropColumn("dbo.Department", "Parent_DepartmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Department", "Parent_DepartmentId", c => c.Int());
            AddColumn("dbo.UserProfile", "Person_PersonId", c => c.Int());
            AlterColumn("dbo.Department", "Title", c => c.String());
            CreateIndex("dbo.Department", "Parent_DepartmentId");
            CreateIndex("dbo.PersonHasJobInDep", "PersonId");
            CreateIndex("dbo.PersonHasJobInDep", "JobId");
            CreateIndex("dbo.PersonHasJobInDep", "DepartmentId");
            CreateIndex("dbo.UserProfile", "Person_PersonId");
            AddForeignKey("dbo.Department", "Parent_DepartmentId", "dbo.Department", "DepartmentId");
            AddForeignKey("dbo.PersonHasJobInDep", "PersonId", "dbo.Person", "PersonId", cascadeDelete: true);
            AddForeignKey("dbo.PersonHasJobInDep", "JobId", "dbo.Job", "JobId", cascadeDelete: true);
            AddForeignKey("dbo.PersonHasJobInDep", "DepartmentId", "dbo.Department", "DepartmentId", cascadeDelete: true);
            AddForeignKey("dbo.UserProfile", "Person_PersonId", "dbo.Person", "PersonId");
        }
    }
}
