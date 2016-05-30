namespace DAKI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class skills : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Badge",
                c => new
                    {
                        BadgeId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        ImageURL = c.String(),
                        NecessaryPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BadgeId);
            
            CreateTable(
                "dbo.UserHasBadge",
                c => new
                    {
                        BadgeId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BadgeId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SkillId);
            
            CreateTable(
                "dbo.UserHasSkill",
                c => new
                    {
                        SkillId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SkillId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserHasSkill");
            DropTable("dbo.Skills");
            DropTable("dbo.UserHasBadge");
            DropTable("dbo.Badge");
        }
    }
}
