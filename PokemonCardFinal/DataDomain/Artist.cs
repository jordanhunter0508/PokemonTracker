using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataDomain
{
    public class Artist
    {
        public int ArtistID { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please enter a first name for the artist.")]
        [MaxLength(50, ErrorMessage = "Artist's first name must be less than 50 characters.")]
        public string GivenName { get; set; }

        [DisplayName("Last Name")]
        [MaxLength(50, ErrorMessage = "Artist's last name must be less than 50 characters.")]
        public string? Surname { get; set; }

        [DisplayName("Active")]
        public bool Active { get; set; }

        [DisplayName("Name")]
        public string Name
        {
            get
            {
                string name = GivenName;
                if (!String.IsNullOrWhiteSpace(Surname))
                {
                    name += ", " + Surname;
                }
                return name;
            }
        }
    }
}
