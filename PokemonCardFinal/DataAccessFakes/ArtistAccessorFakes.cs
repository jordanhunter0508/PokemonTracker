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
                GivenName = "Test Given1",
                Surname = ""
            });
        }
       
        public Artist SelectArtistByArtistID(int artistID)
        {
            throw new NotImplementedException();
        }

        public Artist SelectArtistByArtistName(string givenName, string surname)
        {
            throw new NotImplementedException();
        }

        public List<Artist> SelectArtists()
        {
            throw new NotImplementedException();
        }

        public int InsertArtist(string giveName, string surname)
        {
            throw new NotImplementedException();
        }

        public int UpdateArtistByArtistID(int artistID, string giveName, string surname)
        {
            throw new NotImplementedException();
        }

        public int DeleteArtistByArtistID(int artistID)
        {
            throw new NotImplementedException();
        }
    }
}
