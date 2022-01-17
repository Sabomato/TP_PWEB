using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TP_PWEB.Models
{
    public class Admin
    {

        [Required]
        [Key, ForeignKey(nameof(IdentityUser))]
        public string AdminId { get; set; }

        public IdentityUser User;


    }
}
