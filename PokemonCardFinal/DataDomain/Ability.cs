using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Ability
    {
        [Required(ErrorMessage = "Please enter an ability name.")]
        [StringLength(30, ErrorMessage = "Ability name must be 30 characters or less.")]
        public string AbilityID { get; set; }

        [Required(ErrorMessage = "Please select an ability type.")]
        [StringLength(25, ErrorMessage = "Ability type must be 25 characters or less.")]
        public string AbilityType { get; set; }

        [Required(ErrorMessage = "Please enter the ability's description.")]
        [StringLength(650, ErrorMessage = "Description must be 650 characters or less.")]
        public string Description { get; set; }
    }
}
