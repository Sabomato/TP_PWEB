using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP_PWEB.Models
{
    public static class RoleNames
    {

        public const string Admin = "Admin";

        public const string PropertyOwner = "Property Owner";

        public const string PropertyEmployee = "Property Employee";

        public const string Client = "Client";

        public static  string[] Roles ={
            Admin,PropertyOwner,PropertyEmployee,Client
        };



    }
}
