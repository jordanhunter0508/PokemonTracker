using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccess
{
    public class ArtistAccesor : IArtistAccessor
    {
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
