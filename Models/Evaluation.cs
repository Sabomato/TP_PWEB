using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TP_PWEB.Models
{
    //[ComplexType]
  
    public class Evaluation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Range(0, 10)]
        public double Rating { get; set; }

        public string Commentary { get; set; }
       
        //[Required]
        //[InverseProperty("StayEvaluation")]
        //public virtual Reservation StayReservation{ get; set; }
        //public int StayReservationId { get; set; }


        //[Required]
        //[InverseProperty("ClientEvaluation")]
        //public virtual Reservation ClientReservation { get; set; }
        //public int ClientReservationId { get; set; }


    }
}
