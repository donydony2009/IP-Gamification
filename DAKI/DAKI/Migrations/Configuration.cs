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
        }
    }
}
