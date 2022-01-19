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

    public class Reservation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Accepted")]
        public bool IsAccepted { get; set; }

        [Required]
        [Display(Name = "Delivered")]
        public bool IsDelivered { get; set; }

        [Required]
        public bool IsReceived { get; set; }

        [Required]   
        [Display(Name ="From")]
        public DateTime StartDate { get; set; }
        [Required]
        [Display(Name = "To")]
        public DateTime EndDate { get; set; }

        //[ForeignKey("StayEvaluationId")]
        public virtual Evaluation StayEvaluation { get; set; }

        //[ForeignKey("ClientEvaluationId")]
        public virtual Evaluation ClientEvaluation { get; set; }

        public virtual ICollection<VerificationReservation> VerificationReservations{ get; set; }

        [Required]
        public int PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public virtual Property Property { get; set; }

        [Required]
        public string ClientId { get; set; }

        
        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }


        [NotMapped]
        public bool IsAvailable { get; set; }

        [NotMapped]
        public string Availability
        {
            get
            {
                return IsAvailable ? "The property is not available in that period!" : "Available";
            }
        }


    }
}
