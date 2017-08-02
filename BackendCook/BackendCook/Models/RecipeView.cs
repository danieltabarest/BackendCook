﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace BackendCook.Models
{
    public class RecipeView
    {
        public HttpPostedFileBase LogoFile { get; set; }

        public int RecipeId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(150, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        [Display(Name = "Direction")]
        public string Direction { get; set; }

        [Display(Name = "Rating")]
        public int Rating { get; set; }

        public int ChefId { get; set; }

        public string Image { get; set; }

    }
}
