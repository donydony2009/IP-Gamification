namespace DAKI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class achievements : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Achievement",
                c => new
                    {
                        AchievementId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        ImageURL = c.String(),
                        Points = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AchievementId);
            
            CreateTable(
                "dbo.UserHasAchievement",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        AchievementId = c.Int(nullable: false),
                        Date = c.DateTimeOffset(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.AchievementId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserHasAchievement");
            DropTable("dbo.Achievement");
        }
    }
}
