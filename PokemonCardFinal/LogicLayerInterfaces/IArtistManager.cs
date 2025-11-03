using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface IArtistManager
    {
        public Artist GetArtistByArtistID(int artistID);
        public Artist GetArtistByName(string givenName, string surname);
        public List<Artist> GetArtists();
        public bool CreateArtist(string givenName, string surname);
        public bool UpdateArtistByArtistID(int artistID, string giveName, string surname);
        public bool DeleteArtistByArtistID(int artistID);
    }
}
