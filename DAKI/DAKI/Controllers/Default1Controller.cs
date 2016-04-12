using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAKI.Models;

namespace DAKI.Controllers
{
    public class Default1Controller : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Default1/

        public ActionResult Index()
        {
            return View(db.Prizes.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id = 0)
        {
            Prize prize = db.Prizes.Find(id);
            if (prize == null)
            {
                return HttpNotFound();
            }
            return View(prize);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Create(Prize prize)
        {
            if (ModelState.IsValid)
            {
                db.Prizes.Add(prize);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prize);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Prize prize = db.Prizes.Find(id);
            if (prize == null)
            {
                return HttpNotFound();
            }
            return View(prize);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(Prize prize)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prize).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prize);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Prize prize = db.Prizes.Find(id);
            if (prize == null)
            {
                return HttpNotFound();
            }
            return View(prize);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Prize prize = db.Prizes.Find(id);
            db.Prizes.Remove(prize);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}