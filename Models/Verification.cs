using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public bool IsAtExit { get; set; }
  
        public int PropertyId { get; set; }

        public virtual Property Property{ get; set; }

        public bool isDeleted { get; set; }

        [NotMapped]
        public SelectList Type { get; set; }

        public Verification()
        {
         
            var list = new List<KeyValuePair<string, bool>>() {
                new KeyValuePair<string, bool>("Exit", true),
                new KeyValuePair<string, bool>("Entrance", false),
            };
            
            Type = new SelectList(list,"Value","Key");
            

        }

        [NotMapped]
        [Display(Name = "Moment")]
        public string Moment
        {
            get
            {
                return IsAtExit ? "Exit" : "Entrance";
            }
        }


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
