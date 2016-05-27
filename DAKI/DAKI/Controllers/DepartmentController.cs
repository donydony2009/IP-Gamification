using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAKI.Models;
using DAKI.App_Data;
namespace DAKI.Controllers
{
    public class DepartmentController : Controller
    {

        //
        // GET: /Department/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        //
        // GET: /Department/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpNotFoundResult("Invalid department id!");

            DepartmentModel model = null;
            using (var ctx = new DepContext())
            {
                var departament = ctx.Departments.FirstOrDefault(item => item.DepartmentId == id);
                if (departament == null)
                    return new HttpNotFoundResult("Department not found");
                model = new DepartmentModel(departament);
            }
            return View(model);
        }

        //
        // GET: /Department/Create

        public ActionResult Create()
        {
            DepartmentModel model = new DepartmentModel();
            return View(model);
        }

        //
        // POST: /Department/Create

        [HttpPost]
        public ActionResult Create(DepartmentModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            using (var ctx = new DepContext())
            {
                var department = new Department()
                {
                    Title = model.Title,
                    Description = model.Description,
                    ParentId = model.Parent,
                    Rules = model.Rules
                };
                if (model.Children != null)
                    foreach (var c in model.Children)
                    {
                        var child = ctx.Departments.FirstOrDefault(item => item.DepartmentId == c);
                        child.ParentId = department.DepartmentId;
                    }
                //model.Children.Each(d => department.Children.Add(ctx.Departments.FirstOrDefault(item => item.ParentId == d)));

            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpNotFoundResult("Invalid department id.");
            DepartmentModel model = null;
            using (var ctx = new DepContext())
            {
                var dep = ctx.Departments.FirstOrDefault(item => item.DepartmentId == id);
                if (dep == null)
                    return new HttpNotFoundResult("Department not found");
                model = new DepartmentModel(dep);

            }
            return View(model);
        }

        //
        // POST: /Department/Edit/5

        [HttpPost]
        public ActionResult Edit(DepartmentModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            using (var ctx = new DepContext())
            {
                var department = ctx.Departments.FirstOrDefault(item => item.DepartmentId == model.Id);
                department.Title = model.Title;
                department.Description = model.Description;
                department.ParentId = model.Parent;
                department.Rules = model.Rules;

                if (model.Children != null)
                    foreach (var c in model.Children)
                    {
                        var child = ctx.Departments.FirstOrDefault(item => item.DepartmentId == c);
                        child.ParentId = department.DepartmentId;
                    }
                //model.Children.Each(d => department.Children.Add(ctx.Departments.FirstOrDefault(item => item.ParentId == d)));

            }
            return RedirectToAction("Index");
        }


        //[HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpNotFoundResult("Invalid department id.");
            using (var ctx = new DepContext())
            {
                var dep = ctx.Departments.FirstOrDefault(item => item.DepartmentId == id);
                if (dep == null)
                    return new HttpNotFoundResult("The department could not be found.");
                dep.Children.Clear();
                dep.PersonHasJobInDeps.Clear();

                ctx.Departments.Remove(dep);
                ctx.SaveChanges();

            }
            return RedirectToAction("Index");
        }


        /*
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            using (var ctx = new DepContext())
            {
                var dep = ctx.Departments.FirstOrDefault(item => item.DepartmentId == id);
                if(dep == null)
                    return new HttpNotFoundResult("The department could not be found.");
                dep.Children.Clear();
                dep.PersonHasJobInDeps.Clear();

                ctx.Departments.Remove(dep);
                ctx.SaveChanges();

            }
                return RedirectToAction("Index");   
        }
          */

        public ActionResult Simple()
        {
            List<Department> all = new List<Department>();
            using (Entities dc = new Entities())
            {
                all = dc.Departments.OrderBy(a => a.Parent).ToList();
            }
            return View(all);
        }

    }
}
