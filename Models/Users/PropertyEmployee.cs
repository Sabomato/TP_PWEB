using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TP_PWEB.Models.Users
{
    public class PropertyEmployee
    {
        [Required]
        [Key, ForeignKey(nameof(IdentityUser))]
        public string PropertyEmployeeId { get; set; }
        
        public IdentityUser User;
        
        [Required]
        public string PropertyManagerId { get; set; }

        public virtual PropertyManager PropertyManager{ get; set; }


   
    }
}
