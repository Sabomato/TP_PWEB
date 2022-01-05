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

        public int Id { get; set; }
        [Required]
        public int Rating{ get; set; }

        public string Commentary { get; set; }

    }
}
