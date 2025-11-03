using System;
using System.Collections.Generic;
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

        public Artist GetArtistByArtistID(int artistID)
        {
            throw new NotImplementedException();
        }

        public Artist GetArtistByName(string givenName, string surname)
        {
            throw new NotImplementedException();
        }

        public List<Artist> GetArtists()
        {
            throw new NotImplementedException();
        }

        public bool CreateArtist(string givenName, string surname)
        {
            throw new NotImplementedException();
        }

        public bool UpdateArtistByArtistID(int artistID, string giveName, string surname)
        {
            throw new NotImplementedException();
        }

        public bool DeleteArtistByArtistID(int artistID)
        {
            throw new NotImplementedException();
        }        
    }
}
