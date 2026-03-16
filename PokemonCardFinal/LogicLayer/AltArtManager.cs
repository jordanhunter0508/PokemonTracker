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
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get an alternate art.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtManager"/>
        /// </summary>
        public List<AlternateArt> GetAllAlternateArt()
        {
            List<AlternateArt> results = new List<AlternateArt>();

            try
            {
                results = _altArtAccessor.SelectAllAlternateArt();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve a list of all alternate arts", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtManager"/>
        /// </summary>
        public PaginatedResult<AlternateArt> GetActiveAlternateArts(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<AlternateArt> results = new PaginatedResult<AlternateArt>();

            if (pageNumber <= 0)
            {
                throw new ArgumentException("Page number must be greater than 0.");
            }
            if (pageSize <= 0)
            {
                throw new ArgumentException("Page size must be greater than 0.");
            }

            try
            {
                results = _altArtAccessor.SelectActiveAlternateArts(pageNumber,pageSize);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve a list of acitve alternate arts.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtManager"/>
        /// </summary>
        public PaginatedResult<AlternateArt> GetDeactiveAlternateArts(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<AlternateArt> results = new PaginatedResult<AlternateArt>();

            if (pageNumber <= 0)
            {
                throw new ArgumentException("Page number must be greater than 0.");
            }
            if (pageSize <= 0)
            {
                throw new ArgumentException("Page size must be greater than 0.");
            }

            try
            {
                results = _altArtAccessor.SelectDeactiveAlternateArts(pageNumber,pageSize);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve a list of deactive alternate arts.", ex);
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
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add an alternate art to the database.\n" +
                    "Please make sure the alternate art was not already created.", ex);
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
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update the alternate art in the database.\n" +
                    "Please make sure the alternate art was correct.", ex);
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
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete the alternate art in the database.\n" +
                    "Please make sure the alternate art is not attached to any cards.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtManager"/>
        /// </summary>
        public bool DeactivateAlternateArt(string alternateArtID)
        {
            bool result = false;

            if (String.IsNullOrWhiteSpace(alternateArtID))
            {
                throw new ArgumentNullException("AlternateArtID must not be null or blank.");
            }

            try
            {
                result = (1 == _altArtAccessor.DeactivateAlternateArt(alternateArtID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to deactivate the alternate arts in the database.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtManager"/>
        /// </summary>
        public bool ReactivateAlternateArt(string alternateArtID)
        {
            bool result = false;

            if (String.IsNullOrWhiteSpace(alternateArtID))
            {
                throw new ArgumentNullException("AlternateArtID must not be null or blank.");
            }

            try
            {
                result = (1 == _altArtAccessor.ReactivateAlternateArt(alternateArtID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to reactivate the alternate arts in the database.", ex);
            }

            return result;
        }
    }
}
