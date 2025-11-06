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
    public class BoosterManager : IBoosterManger
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
            catch (Exception)
            {
                throw new ApplicationException("Failed to get a booster.");
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IBoosterManager"/>
        /// </summary>
        public List<Booster> GetBoosters()
        {
            List<Booster> results = null;

            try
            {
                results = _boosterAccessor.SelectBoosters();
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed to retrieve a list of boosters.");
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
                throw new ArgumentNullException("Booster was null.");
            }

            try
            {
                result = (1 == _boosterAccessor.InsertBooster(booster));
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed to add booster to database.");
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IBoosterManager"/>
        /// </summary>
        public bool EditBooster(Booster booster)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implements from <see cref="IBoosterManager"/>
        /// </summary>
        public bool DeleteBooster(string boosterID)
        {
            throw new NotImplementedException();
        }
    }
}
