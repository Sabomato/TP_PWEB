using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TP_PWEB.Models.Properties;

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

        [NotMapped]
        public bool IsClient { get; set; }

        [NotMapped]

        public string Username { get; set; }

        [NotMapped]

        public int StayTime { get; set; }


       // [Required]

        //[Required]
        //[InverseProperty("StayEvaluation")]
        //public virtual Reservation StayReservation{ get; set; }
        //public int StayReservationId { get; set; }


        //[Required]
        //[InverseProperty("ClientEvaluation")]
        //public virtual Reservation ClientReservation { get; set; }
        //public int ClientReservationId { get; set; }
        /*
        [Required]
        public int PropertyId { get; set; }

        public Property Property { get; set; }
        */
    }
}
