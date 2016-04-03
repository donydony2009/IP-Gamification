using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Types
{
    enum RoleId : uint
    {
        Regular = 0,
        Advanced,
        ModeratorForum,
        Admin
    }

    public class Role
    {
        public static string Regular = "regular";
        public static string Advanced = "advanced";
        public static string ModeratorForum = "moderator_forum";
        public static string Admin = "admin";
    }
}