namespace DAKI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DAKI.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<DAKI.Models.UsersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAKI.Models.UsersContext context)
        {
            context.Departments.AddOrUpdate( x => x.DepartmentId,
                    new Department() { DepartmentId = 1, Title = "Administration" , Description = "Administration department", ParentId = null, Rules = true },
                    new Department() { DepartmentId = 2, Title = "Accounting", Description = "Accounting department", ParentId = 1, Rules = true },
                    new Department() { DepartmentId = 3, Title = "Research", Description = "Research department", ParentId = 1, Rules = true },
                    new Department() { DepartmentId = 4, Title = "Operations", Description = "Operations department", ParentId = 3, Rules = true },
                    new Department() { DepartmentId = 5, Title = "Sales", Description = "Sales department", ParentId = 4, Rules = true }
                );
            context.Badges.AddOrUpdate(x => x.BadgeId,
                    new Badge() { BadgeId = 1, Title = "Platinum Badge", Description = "A badge to go with your ego problems, you un-self-sufficient man.", NecessaryPoints = 10000, ImageURL = "orderedList0.png" },
                    new Badge() { BadgeId = 2, Title = "Golden Badge", Description = "Great job, kisser.", NecessaryPoints = 3000, ImageURL = "orderedList0.png" },
                    new Badge() { BadgeId = 3, Title = "Silver Badge", Description = "Be careful with the IT vampire girl.", NecessaryPoints = 1000, ImageURL = "orderedList0.png" },
                    new Badge() { BadgeId = 4, Title = "Copper Badge", Description = "Neeeah...", NecessaryPoints = 500, ImageURL = "orderedList0.png" },
                    new Badge() { BadgeId = 5, Title = "Iron Badge", Description = "FER!", NecessaryPoints = 10, ImageURL = "orderedList0.png" }
                );
            context.Achievements.AddOrUpdate(x => x.AchievementId,
                    new Achievement() { AchievementId = 1, Title = "Task1", Description = "Complete documents for the Jasons' sale", Points = 100, ImageURL = "orderedList0.png" },
                    new Achievement() { AchievementId = 2, Title = "Task2", Description = "Research in legal implies of Brown's case", Points = 50, ImageURL = "orderedList1.png" },
                    new Achievement() { AchievementId = 3, Title = "Repair coffee maker", Description = "Research department", Points = 160, ImageURL = "orderedList2.png" },
                    new Achievement() { AchievementId = 4, Title = "One month on-time", Description = "Operations department", Points = 150, ImageURL = "orderedList3.png" },
                    new Achievement() { AchievementId = 5, Title = "Top Sales", Description = "Sales department", Points = 300, ImageURL = "orderedList4.png" }
                );
        }
    }
}
