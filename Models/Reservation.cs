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

        //[ForeignKey("StayEvaluationId")]
        public virtual Evaluation StayEvaluation { get; set; }

        //[ForeignKey("ClientEvaluationId")]
        public virtual Evaluation ClientEvaluation { get; set; }


        public int PropertyId { get; set; }

        [Required]
        [ForeignKey("PropertyId")]
        public virtual Property Property { get; set; }


        public string ClientId { get; set; }

        [Required]
        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }

    }
}
