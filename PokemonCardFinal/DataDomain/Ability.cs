using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Ability
    {
        [DisplayName("Ability Name")]
        [Required(ErrorMessage = "Please enter an ability name.")]
        [StringLength(30, ErrorMessage = "Ability name must be 30 characters or less.")]
        public string AbilityID { get; set; }

        [DisplayName("Ability Type")]
        [Required(ErrorMessage = "Please select an ability type.")]
        [StringLength(25, ErrorMessage = "Ability type must be 25 characters or less.")]
        public string AbilityType { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Please enter the ability's description.")]
        [StringLength(650, ErrorMessage = "Description must be 650 characters or less.")]
        public string Description { get; set; }

        [DisplayName("Active")]
        public bool Active { get; set; }
    }

    public class AbilityVM : Ability
    {
        [DisplayName("Description")]
        public string DescriptionDisplay
        {
            get
            {
                if (string.IsNullOrEmpty(Description) ||
                    Description.Length <= 75)
                {
                    return Description;
                }

                string truncated = Description.Substring(0, 40);

                int lastSpace = truncated.LastIndexOf(' ');
                if (lastSpace > 0)
                {
                    truncated = truncated.Substring(0, lastSpace);
                }

                return truncated + "...";
            }
        }
    }
}
