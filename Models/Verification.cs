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
        public string Observation { get; set; }

    }
}
