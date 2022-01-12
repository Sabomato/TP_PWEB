using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TP_PWEB.Models
{
    [Table("Properties")]
    public class Property
    {   


        public int Id { get; set; }
        
        [Required]
        [MinLength(8,ErrorMessage ="The title is too short!")]
        [MaxLength(50,ErrorMessage ="The title is too long!")]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        
        public double Price { get; set; }

        [Required]
        
        public string Comodities { get; set; }


        [Required]
        [ForeignKey("OwnerId")]
        public virtual PropertyManager PropertyManager { get; set; }
        
        public virtual ICollection<Reservation> Reservations { get; set; }
        
        //[ForeignKey("ExitVerification")]
        public virtual ICollection<Verification> Verifications { get; set; }

        //[ForeignKey("EntranceVerification")]
        //public virtual ICollection<Verification> EntranceVerifications { get; set; }

    }
}
