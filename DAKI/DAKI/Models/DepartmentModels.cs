using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Globalization;
using DAKI.App_Data;

namespace DAKI.Models
{
    public class DepContext : DbContext
    {
        public DepContext()
            : base("Enitites")
        {
        }

        public DbSet<Department> Departments { get; set; }
    }
    public class DepartmentModel
    {
        public int Id;
        public string Title;
        public string Description;
        public bool? Rules;

        public List<int> ManagerIds;

        public int? Parent;
        public List<int> Children = new List<int>();


        public DepartmentModel(Department dep)
        {
            this.Id = dep.DepartmentId;
            this.Title = dep.Title;
            this.Rules = dep.Rules;
            this.Description = dep.Description;
            this.Parent = dep.ParentId;
            foreach (var d in dep.PersonHasJobInDeps)
                if (d.Job.Manages == true)
                {
                    this.ManagerIds.Add(d.JobId);
                }
            foreach (var d in dep.Children)
                this.Children.Add(d.DepartmentId);


        }

        public DepartmentModel(int id)
        {
            using (var context = new DepContext())
            {
                var dep = context.Departments.First<Department>(i => i.DepartmentId == id);


                this.Id = dep.DepartmentId;
                this.Title = dep.Title;
                this.Description = dep.Description;
                this.Rules = dep.Rules;
                foreach (var d in dep.PersonHasJobInDeps)
                    if (d.Job.Manages == true)
                    {
                        this.ManagerIds.Add(d.JobId);
                    }
                foreach (var d in dep.Children)
                    this.Children.Add(d.DepartmentId);
                this.Parent = dep.ParentId;
            }


        }

        public DepartmentModel() { }

    }
}