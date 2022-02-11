using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TP_PWEB.Models.Users;

namespace TP_PWEB.Models.PropertyEmployees
{
    public class PropertyEmployeeCreateVM:PropertyEmployee
    {


        
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



    }
}
