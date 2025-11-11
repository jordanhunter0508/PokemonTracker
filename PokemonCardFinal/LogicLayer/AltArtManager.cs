using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccessInterfaces;
using DataDomain;
using LogicLayerInterfaces;
using Microsoft.IdentityModel.Tokens;

namespace LogicLayer
{
    public class AltArtManager : IAltArtManager
    {
        IAltArtAccessor _altArtAccessor;

        /// <summary>
        /// General AltArtManager created for the presentaion layer
        /// </summary>
        public AltArtManager()
        {
            _altArtAccessor = new AltArtAccessor();
        }

        /// <summary>
        /// Used for testing to pass in fake data
        /// </summary>
        /// <param name="altArtAccessor">Set the IAltArtAccessor in the AltArtManager</param>
        public AltArtManager(IAltArtAccessor altArtAccessor)
        {
            _altArtAccessor = altArtAccessor;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtManager"/>
        /// </summary>
        public AlternateArt GetAlternateArtByID(string alternateArtID)
        {
            AlternateArt result = null;

            try
            {
                result = _altArtAccessor.SelectAlternateArtByID(alternateArtID);
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed to get an alternate art.");
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtManager"/>
        /// </summary>
        public List<AlternateArt> GetAlternateArts()
        {
            List<AlternateArt> results = null;

            try
            {
                results = _altArtAccessor.SelectAlternateArts();
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed to retrieve a list of alternate arts.");
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtManager"/>
        /// </summary>
        public bool AddAlternateArt(AlternateArt alternateArt)
        {
            bool result = false;

            if (alternateArt == null)
            {
                throw new ArgumentNullException("Alternate Art was empty.");
            }

            try
            {
                result = (1 == _altArtAccessor.InsertAlternateArt(alternateArt));
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed to add an alternate art to the database.\n" +
                    "Please make sure the alternate art was not already created.");
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtManager"/>
        /// </summary>
        public bool EditAlternateArt(AlternateArt alternateArt)
        {
            bool result = false;

            if (alternateArt == null)
            {
                throw new ArgumentNullException("Alternate Art was empty.");
            }

            try
            {
                result = (1 == _altArtAccessor.UpdateAlternateArt(alternateArt));
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed to update the alternate art in the database.\n" +
                    "Please make sure the alternate art was correct.");
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtManager"/>
        /// </summary>
        public bool DeleteAlternateArt(string alternateArtID)
        {
            bool result = false;

            try
            {
                result = (1 == _altArtAccessor.DeleteAlternateArt(alternateArtID));
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed to delete the alternate art in the database.\n" +
                    "Please make sure the alternate art is not attached to any cards.");
            }

            return result;
        }
    }
}
