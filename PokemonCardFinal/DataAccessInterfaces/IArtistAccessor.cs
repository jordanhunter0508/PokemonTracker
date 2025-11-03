using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface IArtistAccessor
    {
        public Artist SelectArtistByArtistID(int artistID);
        public Artist SelectArtistByArtistName(string givenName, string surname);
        public List<Artist> SelectArtists();
        public int InsertArtist(string giveName, string surname);
        public int UpdateArtistByArtistID(int artistID,string giveName, string surname);
        public int DeleteArtistByArtistID(int artistID);
    }
}
