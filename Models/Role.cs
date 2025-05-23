using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SE_TRENDZZ.Models
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }

    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
}
