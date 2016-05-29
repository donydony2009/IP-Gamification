namespace DAKI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migr1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "Name", c => c.String());
            AddColumn("dbo.UserProfile", "Surname", c => c.String());
            AddColumn("dbo.UserProfile", "BirthDate", c => c.DateTime());
            AddColumn("dbo.UserProfile", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfile", "Address");
            DropColumn("dbo.UserProfile", "BirthDate");
            DropColumn("dbo.UserProfile", "Surname");
            DropColumn("dbo.UserProfile", "Name");
        }
    }
}
