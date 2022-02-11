using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TP_PWEB.Models.Users;

namespace TP_PWEB.Models.PropertyEmployees
{
    public class PropertyEmployeeEditVM:PropertyEmployee
    {


        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        public  string Password { get; set; }

        
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }




        public void SetEmployee(PropertyEmployee employee)
        {
            User = employee.User;
            UserName = employee.User.UserName;
            Email = employee.User.Email;
            PropertyEmployeeId = employee.PropertyEmployeeId;
            PropertyManagerId = employee.PropertyManagerId;
        }

     

    }
}
