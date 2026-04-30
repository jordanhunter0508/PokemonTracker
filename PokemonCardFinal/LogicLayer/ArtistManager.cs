using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccessInterfaces;
using DataDomain;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class ArtistManager : IArtistManager
    {
        IArtistAccessor _artistAccessor;

        /// <summary>
        /// General ArtistManager created for the presentaion layer
        /// </summary>
        public ArtistManager()
        {
            _artistAccessor = new ArtistAccesor();
        }

        /// <summary>
        /// Used for testing to pass in fake data
        /// </summary>
        /// <param name="artistAccessor">Set the IArtistAccessor in the ArtistManager</param>
        public ArtistManager(IArtistAccessor artistAccessor)
        {
            _artistAccessor = artistAccessor;
        }

        /// <summary>
        /// Implements from <see cref="IArtistManager"/>
        /// </summary>
        public Artist GetArtistByArtistID(int artistID)
        {
            Artist resultArtist = null;

            try
            {
                resultArtist = _artistAccessor.SelectArtistByArtistID(artistID);
            }
            catch (Exception)
            {
                throw new ApplicationException("Faild to retrieve an artist.");
            }

            return resultArtist;
        }

        /// <summary>
        /// Implements from <see cref="IArtistManager"/>
        /// </summary>
        public Artist GetArtistByName(string givenName, string surname)
        {
            Artist resultArtist = null;

            if (givenName == null || givenName == "")
            {
                throw new ArgumentNullException("Given name cannot be empty.");
            }

            try
            {
                resultArtist = _artistAccessor.SelectArtistByArtistName(givenName, surname);
            }
            catch (Exception)
            {
                throw new ApplicationException("Faild to retrieve an artist.");
            }

            return resultArtist;
        }

        /// <summary>
        /// Implements from <see cref="IArtistManager"/>
        /// </summary>
        public List<Artist> GetAllArtists()
        {
            List<Artist> results = null;

            try
            {
                results = FormatArtists(_artistAccessor.SelectAllArtists());
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed to retrieve artists");
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IArtistManager"/>
        /// </summary>
        public bool AddArtist(string givenName, string surname)
        {
            bool result = false;

            try
            {
                Artist artist = null;
                int artistID = _artistAccessor.InsertArtist(givenName, surname);
                artist = _artistAccessor.SelectArtistByArtistID(artistID);

                if (_artistAccessor != null)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed to create new record of an artist.\nCheck if artist is already added.");
            }
            return result;
        }

        /// <summary>
        /// Implements from <see cref="IArtistManager"/>
        /// </summary>
        public bool EditArtist(int artistID, string givenName, string surname)
        {
            bool result = false;

            try
            {
                result = (1 == _artistAccessor.UpdateArtist(artistID, givenName, surname));
            }
            catch (Exception)
            {
                throw new Exception("Failed to update artist.");
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IArtistManager"/>
        /// </summary>
        public bool DeleteArtist(int artistID)
        {
            bool result = false;

            try
            {
                result = (1 == _artistAccessor.DeleteArtist(artistID));
            }
            catch (Exception)
            {
                throw new Exception("Failed to delete artist.");
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IArtistManager"/>
        /// </summary>
        public bool DeactivateArtist(int artistID)
        {
            bool result = false;

            try
            {
                result = (1 == _artistAccessor.DeactivateArtist(artistID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to deactivate the artist.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IArtistManager"/>
        /// </summary>
        public bool ReactivateArtist(int artistID)
        {
            bool result = false;

            try
            {
                result = (1 == _artistAccessor.ReactivateArtist(artistID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to reactivate the artist.", ex);
            }

            return result;
        }

        /// <summary>
        /// Makes sure the first leter of the given and surname is capital
        /// then sorts by the id.
        /// </summary>
        /// <param name="artists">The IEnumerable that is being sorted</param>
        /// <returns>Returns an IEnumberable of type Artist that is formated for dispaly.</returns>
        private List<Artist> FormatArtists(IEnumerable<Artist> artists)
        {
            if (artists == null)
            {
                throw new ArgumentNullException("Artists could not be formatted.");
            }

            foreach (Artist artist in artists)
            {
                artist.GivenName = char.ToUpper(artist.GivenName[0]) + artist.GivenName.Substring(1);
                if (artist.Surname.Length != 0)
                {
                    artist.Surname = char.ToUpper(artist.Surname[0]) + artist.Surname.Substring(1);
                }
            }
            artists = artists.OrderBy(artist => artist.ArtistID);
            return artists.ToList();
        }
    }
}
