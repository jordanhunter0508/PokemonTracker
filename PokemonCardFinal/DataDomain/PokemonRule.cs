using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataDomain
{
    public class PokemonRule
    {
        [DisplayName("Rule Name")]
        [Required(ErrorMessage = "Please enter the rule's name.")]
        [StringLength(50, ErrorMessage = "Pokemon card rule's name must be 50 characters or less.")]
        public string RuleID { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Please enter the rule's description.")]
        [StringLength(150, ErrorMessage = "Description must be 150 characters or less.")]
        public string Description { get; set; }

        [DisplayName("Active")]
        public bool Active { get; set; }
    }
}
