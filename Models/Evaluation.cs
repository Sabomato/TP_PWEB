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
        [Range(0, 10,ErrorMessage ="The rating is between 0 and 10!")]       
        public double Rating { get; set; }

        [DataType(DataType.Text)]
        public string Commentary { get; set; }






        [NotMapped]
        [Required]
        public bool IsClient { get; set; }

        [NotMapped]
        public string Username { get; set; }

        [NotMapped]
        [Display(Name ="Stay time (days)")]
        public int StayTime { get; set; }

        [NotMapped]
        public int ReservationId { get; set; }

        

    }
}
