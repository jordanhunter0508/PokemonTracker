using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class ElementType
    {
        [DisplayName("Element Type")]
        [Required(ErrorMessage = "Please enter a name for the element type.")]
        [MaxLength(15, ErrorMessage = "Element type's name must be less than 15 characters.")]
        public string ElementTypeID { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Please enter a description for the element type.")]
        [MaxLength(100, ErrorMessage = "Element type's description must be less than 100 characters.")]
        public string Description { get; set; }

        [DisplayName("Active")]
        public bool Active { get; set; }
    }
}
