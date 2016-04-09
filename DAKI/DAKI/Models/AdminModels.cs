using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using System.Web.WebPages.Html;

namespace DAKI.Models
{   
    public class RoleManagementModel
    {
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }
    }
}
