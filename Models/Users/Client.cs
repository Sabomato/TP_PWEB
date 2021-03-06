using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TP_PWEB.Models
{
    public class Client
    {

        [Required]
        [Key,ForeignKey(nameof(IdentityUser))]
        public string ClientId { get; set; }

        public IdentityUser User;

        public virtual ICollection<Reservation> Reservations { get; set; }

        
    }
}
