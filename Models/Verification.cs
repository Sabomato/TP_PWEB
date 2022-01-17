using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TP_PWEB.Models.Properties;

namespace TP_PWEB.Models
{
    [ComplexType]
    public class Verification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsChecked { get; set; }

        [Required]
        public bool IsAtExit { get; set; }

        [Required]
        public string Observation { get; set; }

        
        public int PropertyId { get; set; }

        public virtual Property Property{ get; set; }


        public virtual ICollection<Image> Images { get; set; }

        [NotMapped]
        public List<IFormFile> ImagesForms { get; set; }

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
