using DAKI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAKI.Models;
using System.Web.Security;
using WebMatrix.WebData;
using System.Data.Entity;
using System.Data;

namespace DAKI.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AdminController : Controller
    {
        private UsersContext db = new UsersContext();
        //
        // GET: /Admin/RoleManagement

        public ActionResult RoleManagement(string command, string username)
        {
            if (!User.IsInRole(Types.Role.Admin))
            {
                return Redirect("/Admin/NoPermissions");
            }
            RoleManagementModel model = new RoleManagementModel();
            if (username != null)
            {
                if (!WebSecurity.UserExists(username))
                {
                    ModelState.AddModelError("userNotFound", "The user does not exist");
                    return View(model);
                }
            }
            
            model.UserName = username;
            ViewBag.returnUrl = Request.Url.AbsoluteUri;
            return View(model);
        }

        //
        // POST: /Account/RoleManagement
        [HttpPost]
        public ActionResult RoleManagement(RoleManagementModel model, string returnUrl)
        {
            string errorKey = "grantRoleError";
            if (!User.IsInRole(Types.Role.Admin))
            {
                return Redirect("/Admin/NoPermissions");
            }

            if (ModelState.IsValid)
            {
                if(!WebSecurity.UserExists(model.UserName))
                {
                    ModelState.AddModelError("userNotFound", "The user does not exist");
                    model.UserName = null;
                    return View(model);
                }
                if (Roles.GetAllRoles().Count(e => e == model.Role) == 0)
                {
                    ModelState.AddModelError(errorKey, "The role does not exist");
                    return View(model);
                }


                if (User.IsInRole(model.Role))
                {
                    ModelState.AddModelError(errorKey, "The user already has that role");
                    return View(model);
                }
                Roles.AddUserToRole(model.UserName, model.Role);


                return View(model);
            }

            ModelState.AddModelError("userNotFound", "Something went wrong.");
            return View(model);
        }

        //
        // DELETE: /Account/RoleManagement
        [HttpDelete]
        public ActionResult RoleManagement(RoleManagementModel model, string returnUrl, string nothing)
        {
            string errorKey = "removeRoleError";
            if (!User.IsInRole(Types.Role.Admin))
            {
                return Redirect("/Admin/NoPermissions");
            }

            if (ModelState.IsValid)
            {
                if (!WebSecurity.UserExists(model.UserName))
                {
                    ModelState.AddModelError("userNotFound", "The user does not exist");
                    model.UserName = null;
                    return View(model);
                }
                if (Roles.GetAllRoles().Count(e => e == model.Role) == 0)
                {
                    ModelState.AddModelError(errorKey, "The role does not exist");
                    return View(model);
                }

                if (!User.IsInRole(model.Role))
                {
                    ModelState.AddModelError(errorKey, "The user is not in that role");
                    return View(model);
                }
                Roles.RemoveUserFromRole(model.UserName, model.Role);

                return View(model);
            }

            ModelState.AddModelError("userNotFound", "Something went wrong.");
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Default1/Create

        public ActionResult CreatePrize()
        {
            if (!User.IsInRole(Types.Role.Admin))
            {
                return Redirect("/Admin/NoPermissions");
            }
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult CreatePrize(Prize prize)
        {
            if (!User.IsInRole(Types.Role.Admin))
            {
                return Redirect("/Admin/NoPermissions");
            }

            if (ModelState.IsValid)
            {
                db.Prizes.Add(prize);
                db.SaveChanges();
                return RedirectToAction("ListPrizes");
            }

            return View(prize);
        }

        //
        // GET: /Admin/ListPrizes

        public ActionResult ListPrizes()
        {
            if (!User.IsInRole(Types.Role.Admin))
            {
                return Redirect("/Admin/NoPermissions");
            }

            return View(db.Prizes.ToList());
        }

        //
        // GET: /Admin/DetailsPrize/5

        public ActionResult DetailsPrize(int id = 0)
        {
            if (!User.IsInRole(Types.Role.Admin))
            {
                return Redirect("/Admin/NoPermissions");
            }

            Prize prize = db.Prizes.Find(id);
            if (prize == null)
            {
                return HttpNotFound();
            }
            return View(prize);
        }

        //
        // GET: /Admin/DeletePrize/5

        public ActionResult DeletePrize(int id = 0)
        {
            if (!User.IsInRole(Types.Role.Admin))
            {
                return Redirect("/Admin/NoPermissions");
            }

            Prize prize = db.Prizes.Find(id);
            if (prize == null)
            {
                return HttpNotFound();
            }
            return View(prize);
        }

        //
        // POST: /Admin/DeletePrize/5

        [HttpPost, ActionName("DeletePrize")]
        public ActionResult DeletePrizeConfirmed(int id)
        {
            if (!User.IsInRole(Types.Role.Admin))
            {
                return Redirect("/Admin/NoPermissions");
            }

            Prize prize = db.Prizes.Find(id);
            db.Prizes.Remove(prize);
            db.SaveChanges();
            return RedirectToAction("ListPrizes");
        }

        //
        // GET: /Admin/EditPrize

        public ActionResult EditPrize(int id = 0)
        {
            if (!User.IsInRole(Types.Role.Admin))
            {
                return Redirect("/Admin/NoPermissions");
            }

            Prize prize = db.Prizes.Find(id);
            if (prize == null)
            {
                return HttpNotFound();
            }
            return View(prize);
        }

        //
        // POST: /Admin/EditPrize

        [HttpPost]
        public ActionResult EditPrize(Prize prize)
        {
            if (!User.IsInRole(Types.Role.Admin))
            {
                return Redirect("/Admin/NoPermissions");
            }

            if (ModelState.IsValid)
            {
                db.Entry(prize).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListPrizes");
            }
            return View(prize);
        }

        //
        // GET: /Admin/NoPermissions

        public ActionResult NoPermissions()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
