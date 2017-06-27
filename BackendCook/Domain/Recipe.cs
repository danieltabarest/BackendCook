using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(150, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        [Display(Name = "Direction")]
        public string Direction{ get; set; }

        [Display(Name = "Rating")]
        public int Rating { get; set; }

        public int ChefId{ get; set; }

        public int CuisineId { get; set; }

        public int IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Cuisine Cuisine { get; set; }
        public virtual Chef Chef{ get; set; }


    }
}
