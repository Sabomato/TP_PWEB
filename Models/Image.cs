using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TP_PWEB.Models.Properties;

namespace TP_PWEB.Models
{
    public class Image
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ContentType { get; set; }

        [Required]
        public byte[] Content { get; set; }

        
        public int? VerificationId { get; set; }
        
        public virtual Verification Verification { get; set; }

        public int? PropertyId { get; set; }
        
        public virtual Property Property  { get; set; }
        

    }
}
