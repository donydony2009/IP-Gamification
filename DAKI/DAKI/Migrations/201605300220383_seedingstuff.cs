namespace DAKI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedingstuff : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserProfile", "cdd");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfile", "cdd", c => c.Int(nullable: false));
        }
    }
}
