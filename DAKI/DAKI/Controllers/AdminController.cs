using DAKI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAKI.Models;
using System.Web.Security;

namespace DAKI.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/RoleManagement

        public ActionResult RoleManagement(string username)
        {
            RoleManagementModel model = new RoleManagementModel();
            model.UserName = username;
            ViewBag.returnUrl = Request.Url.AbsoluteUri;
            return View(model);
        }

        //
        // POST: /Admin/RoleManagement

        //
        // POST: /Account/GrantRole
        [HttpPost]
        public ActionResult GrantRole(GrantRoleModel model, string returnUrl)
        {
            if (ModelState.IsValid && User.IsInRole(Types.Role.Admin))
            {
                using (var context = new UsersContext())
                {
                    if (context.Database.Exists())
                    {
                        if (context.UserProfiles.Count<UserProfile>(e => e.UserName == model.UserName) == 0)
                        {
                            ModelState.AddModelError("", "The user does not exist");
                            return Redirect(returnUrl);
                        }
                    }
                }
                if (Roles.GetAllRoles().Count(e => e == model.Role) == 0)
                {
                    ModelState.AddModelError("", "The role does not exist");
                    return Redirect(returnUrl);
                }

                if (User.IsInRole(model.Role))
                {
                    ModelState.AddModelError("", "The user already has that role");
                    return Redirect(returnUrl);
                }
                Roles.AddUserToRole(model.UserName, model.Role);
                return Redirect(returnUrl);
            }

            ModelState.AddModelError("", "Something went wrong.");
            return Redirect(returnUrl);
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


    }
}
