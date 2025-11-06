using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class BoosterAccessorFakes : IBoosterAccessor
    {
        List<Booster> _boosters;

        /// <summary>
        /// Fills the _boosters list with fake data
        /// </summary>
        public BoosterAccessorFakes()
        { 
            _boosters = new List<Booster>();
            _boosters.Add(new Booster()
            {
                BoosterID = "Test Booster 1",
                Series = "test series",
                ReleaseDate = DateTime.Parse("2025-11-06"),
                Abbreviation = "test",
            });
            _boosters.Add(new Booster()
            {
                BoosterID = "Test Booster 2",
                Series = "booster 2 series",
                ReleaseDate = DateTime.Parse("1994-01-28"),
                Abbreviation = "ser",
            });
            _boosters.Add(new Booster()
            {
                BoosterID = "Test Booster 3",
                Series = "series",
                ReleaseDate = DateTime.Parse("2003-10-10"),
                Abbreviation = "abv",
            });
        }

        /// <summary>
        /// Implements from <see cref="IBoosterAccessor"/> used for testing
        /// </summary>
        public Booster SelectBoosterByBoosterID(string boosterID)
        {
            Booster result = null;

            foreach (Booster booster in _boosters)
            {
                if (booster.BoosterID == boosterID)
                {
                    result = booster;
                    break;
                }
            }

            if (result == null)
            {
                throw new ArgumentNullException("Booster ID could not be found.");
            }
            return result;
        }

        /// <summary>
        /// Implements from <see cref="IBoosterAccessor"/> used for testing
        /// </summary>
        public List<Booster> SelectBoosters()
        {
            List<Booster> results = null;
            results = _boosters;
            return results;
        }

        /// <summary>
        /// Implements from <see cref="IBoosterAccessor"/> used for testing
        /// </summary>
        public int InsertBooster(Booster booster)
        {
            int result = 0;
            bool notUsed = true;

            foreach (Booster boosters in _boosters)
            {
                if (boosters.BoosterID == booster.BoosterID)
                {
                    throw new Exception("Booser ID already used.");
                }
                else if(boosters.Abbreviation == booster.Abbreviation)
                {
                    throw new Exception("Abbreviation already used.");
                }
            }

            _boosters.Add(booster);
            result = 1;

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IBoosterAccessor"/> used for testing
        /// </summary>
        public int UpdateBooster(Booster booster)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implements from <see cref="IBoosterAccessor"/> used for testing
        /// </summary>
        public int DeleteBooster(string boosterID)
        {
            throw new NotImplementedException();
        }
    }
}
