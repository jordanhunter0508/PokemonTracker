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
                throw new ApplicationException("Faild to retrive an artist.");
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
            if (surname == null || surname == "")
            {
                surname = " ";
            }

            try
            {
                resultArtist = _artistAccessor.SelectArtistByArtistName(givenName, surname);
            }
            catch (Exception)
            {
                throw new ApplicationException("Faild to retrive an artist.");
            }

            return resultArtist;
        }

        /// <summary>
        /// Implements from <see cref="IArtistManager"/>
        /// </summary>
        public List<Artist> GetArtists()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implements from <see cref="IArtistManager"/>
        /// </summary>
        public bool AddArtist(string givenName, string surname)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implements from <see cref="IArtistManager"/>
        /// </summary>
        public bool EditArtistByArtistID(int artistID, string giveName, string surname)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implements from <see cref="IArtistManager"/>
        /// </summary>
        public bool DeleteArtistByArtistID(int artistID)
        {
            throw new NotImplementedException();
        }        
    }
}
