using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TP_PWEB.Models
{
    public class VerificationReservation
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int VerificationId { get; set; }

        public Verification Verification { get; set; }

        public int ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }

        [Required]
        [Display(Name = "Done")]
        public bool IsChecked { get; set; }


        public string Observation { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        [NotMapped]
        public List<IFormFile> ImagesForms { get; set; }


        [NotMapped]
        [Display(Name = "Status")]
        public string Checked
        {
            get
            {
                return IsChecked ? "Done" : "Undone";
            }
        }

      
    }
}
