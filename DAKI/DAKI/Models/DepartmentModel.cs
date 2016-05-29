using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;


namespace DAKI.Models
{
   
    public class DepartmentModel
    {
        public int Id;
        public string Title;
        public string Description;
        public bool? Rules;

        public List<int> ManagerIds;

        public SelectListItem Parent;
        public List<SelectListItem> Children = new List<SelectListItem>();


        public DepartmentModel(Department dep)
        {
            this.Id = dep.DepartmentId;
            this.Title = dep.Title;
            this.Rules = dep.Rules;
            this.Description = dep.Description;
            this.Parent.Value = dep.ParentId.ToString() ;
            using(var ctx = new UsersContext()){
                var s = ctx.Departments.FirstOrDefault(d=>d.ParentId == dep.ParentId).Title;
                this.Parent.Text = s;
            }
            foreach (var d in dep.PersonHasJobInDeps)
                if (d.Job.Manages == true)
                {
                    this.ManagerIds.Add(d.JobId);
                }
            foreach (var d in dep.Children)
                this.Children.Add(new SelectListItem {
                    Value = d.DepartmentId.ToString(),
                    Text = d.Title
                });


        }

        public DepartmentModel(int id)
        {
            using (var context = new UsersContext())
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
                    this.Children.Add(new SelectListItem
                    {
                        Value = d.DepartmentId.ToString(),
                        Text = d.Title
                    });
                this.Parent.Value = dep.ParentId.ToString();
                using (var ctx = new UsersContext())
                {
                    var s = ctx.Departments.FirstOrDefault(d => d.ParentId == dep.ParentId).Title;
                    this.Parent.Text = s;
                }
            }


        }


        public List<SelectListItem> AllDepartments()
        {
            List<Department> all = new List<Department>();
            using (var dc = new UsersContext())
            {
                if (dc.Departments != null)
                all = dc.Departments.OrderBy(a => a.Parent.DepartmentId).ToList();
            }
            List<SelectListItem> deps = new List<SelectListItem>();
            foreach (var d in all)
                deps.Add(new SelectListItem
                {
                    Value = d.DepartmentId.ToString(),
                    Text = d.Title
                });
            return deps;
        }


        public DepartmentModel() { }

    }
}