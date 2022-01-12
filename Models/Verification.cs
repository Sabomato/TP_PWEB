using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TP_PWEB.Models
{
    [ComplexType]
    public class Verification
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool isChecked { get; set; }

        [Required]
        public bool isAtExit { get; set; }

        [Required]
        public string Observation { get; set; }


        public int PropertyId { get; set; }
        public virtual Property Property{ get; set; }

        //[Required]
        // [InverseProperty("ExitVerifications")]
        /*
         public virtual Property ExitProperty{ get; set; }

         public int ExitPropertyId{ get; set; }

         //[Required]
         //[InverseProperty("EntranceVerifications")]
         public virtual Property EntranceProperty{ get; set; }

         public int EntrancePropertyId { get; set; }
        */
    }
}
