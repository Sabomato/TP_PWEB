using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TP_PWEB.Models.Properties
{
    [Table("Properties")]
    public class Property
    {   

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [MinLength(8,ErrorMessage ="The title is too short!")]
        [MaxLength(50,ErrorMessage ="The title is too long!")]
        public string Title { get; set; }

        
        public int CategoryId { get; set; }
        
        public virtual Category Category { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display (Name ="Price(€)/Night")]
        public double Price { get; set; }

        [Required]
        
        public string Comodities { get; set; }

        [Required]
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual PropertyManager PropertyManager { get; set; }
        
        public virtual ICollection<Reservation> Reservations { get; set; }
        
        //[ForeignKey("ExitVerification")]
        public virtual ICollection<Verification> Verifications { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        [NotMapped]
        public double? Rating { get; set; }

        [NotMapped]
        public SelectList Categories { get; set; }

        [NotMapped]
        [Required]
        [Display(Name = "Upload Images")]
        public List<IFormFile> ImagesForms { get; set; }

        [NotMapped]
        public Image CoverImage { get; set; }

        [NotMapped]
        public string CurrentClientId { get; set; }


        public Property(List<Category> categories)
        {
            
            Categories = new SelectList(categories,nameof(Category.Id),nameof(Category.Name));
            
        }

        public Property() { }

        //[ForeignKey("EntranceVerification")]
        //public virtual ICollection<Verification> EntranceVerifications { get; set; }

    }
}
