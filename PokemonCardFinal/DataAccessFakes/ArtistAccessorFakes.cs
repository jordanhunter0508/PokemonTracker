using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class ArtistAccessorFakes : IArtistAccessor
    {
        List<Artist> _artists;

        /// <summary>
        /// Fills the _artists list with fake data
        /// </summary>
        public ArtistAccessorFakes()
        { 
            _artists = new List<Artist>();
            _artists.Add(new Artist()
            {
                ArtistID = 1,
                GivenName = "Test Given 1",
                Surname = "Test Surname 1"
            });
            _artists.Add(new Artist()
            {
                ArtistID = 2,
                GivenName = "Test Given 1",
                Surname = "Test Surname 2"
            });
            _artists.Add(new Artist()
            {
                ArtistID = 3,
                GivenName = "Test Given 3",
                Surname = " "
            });
        }

        /// <summary>
        /// Implements from <see cref="IArtistAccessor"/> used for testing
        /// </summary>
        public Artist SelectArtistByArtistID(int artistID)
        {
            Artist resultArtist = null;

            foreach (Artist artist in _artists)
            {
                if (artist.ArtistID == artistID)
                { 
                    resultArtist = artist;
                    break;
                }
            }

            if (resultArtist == null)
            {
                throw new ArgumentException("Artist ID could not be found.");
            }

            return resultArtist;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public Artist SelectArtistByArtistName(string givenName, string surname)
        {

            Artist resultArtist = null;

            foreach (Artist artist in _artists)
            {
                if (artist.GivenName == givenName && artist.Surname == surname)
                {
                    resultArtist = artist;
                    break;
                }
            }

            if (resultArtist == null)
            {
                throw new ArgumentException("Artist ID could not be found.");
            }

            return resultArtist;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public List<Artist> SelectArtists()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public int InsertArtist(string giveName, string surname)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public int UpdateArtistByArtistID(int artistID, string giveName, string surname)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public int DeleteArtistByArtistID(int artistID)
        {
            throw new NotImplementedException();
        }
    }
}
