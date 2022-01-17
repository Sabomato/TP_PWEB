using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP_PWEB.Models.Properties
{
    public class PropertyCreateViewModel:Property
    {



        public List<SelectListItem> Categories { get; set; }

        [Display (Name = "Upload Images")]
        public List<IFormFile> ImagesForms { get; set; }



        public PropertyCreateViewModel(List<Category> categories)
        {

            Categories = new List<SelectListItem>();
            foreach (Category category in categories)
            {
                Categories.Add(new SelectListItem(category.Name, category.Id.ToString()));
            }

        }
        public PropertyCreateViewModel() { }

    }
}
