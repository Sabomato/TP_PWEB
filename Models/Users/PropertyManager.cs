using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TP_PWEB.Models.Properties;

namespace TP_PWEB.Models
{
    public class PropertyManager
    {
        [Required]
        [Key, ForeignKey(nameof(IdentityUser))]
        public string PropertyManagerId { get; set; }

        public IdentityUser User;

        public virtual ICollection<Property> Properties{ get; set; }

       // public PropertyManager(IdentityUser user)
        //{
         //   User = user;
        //}
    }
}
