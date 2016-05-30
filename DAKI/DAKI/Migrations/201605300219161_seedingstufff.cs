namespace DAKI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedingstufff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "cdd", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfile", "cdd");
        }
    }
}
