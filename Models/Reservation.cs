using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TP_PWEB.Models
{

    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }


        //public int EvaluationId { get; set; }
        [Required]        
        public Evaluation Evaluation { get; set; }

        //public int PropertyId { get; set; }

        //[Required]
        //[ForeignKey("PropertyId")]
        //public Property Property { get; set; }


        public string ClientId { get; set; }

        [Required]
        [ForeignKey("ClientId")]
        public Client Client { get; set; }

    }
}
