using DAKI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAKI.Models;
using System.Web.Security;
using WebMatrix.WebData;

namespace DAKI.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/RoleManagement

        public ActionResult RoleManagement(string command, string username)
        {
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
        public ActionResult RoleManagement(RoleManagementModel model, string command, string returnUrl)
        {
            string errorKey = "grantRoleError";
            if (command == "remove")
            {
                errorKey = "removeRoleError";
            }
            if (ModelState.IsValid && User.IsInRole(Types.Role.Admin))
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

                switch(command)
                {
                    case "add":
                        {
                            if (User.IsInRole(model.Role))
                            {
                                ModelState.AddModelError(errorKey, "The user already has that role");
                                return View(model);
                            }
                            Roles.AddUserToRole(model.UserName, model.Role);
                        }
                        break;
                    case "remove":
                        {
                            if (!User.IsInRole(model.Role))
                            {
                                ModelState.AddModelError(errorKey, "The user is not in that role");
                                return View(model);
                            }
                            Roles.RemoveUserFromRole(model.UserName, model.Role);
                        }
                        break;
                }

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


    }
}
