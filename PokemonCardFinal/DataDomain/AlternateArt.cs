using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class AlternateArt
    {
        [DisplayName("Alternate Art Name")]
        [Required(ErrorMessage = "Please enter an name for the alternate art.")]
        [MaxLength(50, ErrorMessage = "Alternate art's name must be less than 50 characters.")]
        public string AlternateArtID { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Please enter a description for the alternate art.")]
        [MaxLength(250, ErrorMessage = "Alternate art's description must be less than 250 characters.")]
        public string Description { get; set; }

        [DisplayName("Active")]
        public bool Active { get; set; }
    }
}
