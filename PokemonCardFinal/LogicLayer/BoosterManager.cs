using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccessInterfaces;
using DataDomain;
using LogicLayerInterfaces;

namespace LogicLayer
{

    public class BoosterManager : IBoosterManager
    {
        IBoosterAccessor _boosterAccessor;

        /// <summary>
        /// General BoosterManager created for the presentaion layer
        /// </summary>
        public BoosterManager() 
        {
            _boosterAccessor = new BoosterAccsesor();
        }

        /// <summary>
        /// Used for testing to pass in fake data
        /// </summary>
        /// <param name="boosterAccessor">Set the IBoosterAccessor in the BoosterManager</param>
        public BoosterManager(IBoosterAccessor boosterAccessor) 
        {
            _boosterAccessor = boosterAccessor;
        }

        /// <summary>
        /// Implements from <see cref="IBoosterManager"/>
        /// </summary>
        public Booster GetBoosterByBoosterID(string boosterID)
        {
            Booster result = null;

            try
            {
                result = _boosterAccessor.SelectBoosterByBoosterID(boosterID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get a booster.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IBoosterManager"/>
        /// </summary>
        public List<Booster> GetBoosters()
        {
            List<Booster> results = new List<Booster>();

            try
            {
                results = _boosterAccessor.SelectBoosters();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve a list of boosters.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IBoosterManager"/>
        /// </summary>
        public List<Booster> SelectActiveBoosters()
        {
            List<Booster> results = new List<Booster>();

            try
            {
                results = _boosterAccessor.SelectActiveBoosters();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve a list of active boosters.",ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IBoosterManager"/>
        /// </summary>
        public List<string> GetBoosterIDs()
        {
            List<string> results = new List<string>();

            try
            {
                results = _boosterAccessor.SelectBoosterIDs();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get booster ids.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IBoosterManager"/>
        /// </summary>
        public bool AddBooster(Booster booster)
        {
            bool result = false;

            if (booster == null)
            {
                throw new ArgumentNullException("Booster was empty.");
            }

            try
            {
                result = (1 == _boosterAccessor.InsertBooster(booster));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add a booster to the database.\n" +
                    "Please make sure the booster was not already created.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IBoosterManager"/>
        /// </summary>
        public bool EditBooster(Booster booster)
        {
            bool result = false;

            if (booster == null)
            {
                throw new ArgumentNullException("Booster was empty.");
            }

            try
            {
                result = (1 == _boosterAccessor.UpdateBooster(booster));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update the booster in the database.\n" + 
                    "Please make sure the booster name was correct.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IBoosterManager"/>
        /// </summary>
        public bool DeleteBooster(string boosterID)
        {
            bool result = false;

            try
            {
                result = (1 == _boosterAccessor.DeleteBooster(boosterID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to delete the booster in the database.\n" + 
                    "Please make sure the booster is not attached to any cards.",ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IBoosterManager"/>
        /// </summary>
        public List<Series> GetSeriesImagePaths()
        {
            List<Series> results = new List<Series>();

            try
            {
                results = _boosterAccessor.SelectSeriesImagePaths();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get a list of series images.",ex);
            }

            return results;
        }
    }
}
