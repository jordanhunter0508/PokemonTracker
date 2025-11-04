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
        public List<Artist> GetArtists()
        {
            List<Artist> results = null;

            try
            {
                results = _artistAccessor.SelectArtists();
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
                Debug.WriteLine(artistID);
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
        public bool EditArtistByArtistID(int artistID, string givenName, string surname)
        {
            bool result = false;

            try
            {
                result = (1 == _artistAccessor.UpdateArtistByArtistID(artistID, givenName, surname));
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
        public bool DeleteArtistByArtistID(int artistID)
        {
            bool result = false;

            try
            {
                result = (1 == _artistAccessor.DeleteArtistByArtistID(artistID));
            }
            catch (Exception)
            {
                throw new Exception("Failed to update artist.");
            }

            return result;
        }        
    }
}
