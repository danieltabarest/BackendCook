using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [MaxLength(50, ErrorMessage = "The maximun length for field {0} is {1} characters")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Display(Name = "Amount")]
        public int Amount{ get; set; }
        public virtual Collection<IngredientMerge> Recipes { get; set; }
    }
}
