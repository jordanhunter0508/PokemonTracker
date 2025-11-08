using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
            int count = 0;

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
            count = 1;

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IBoosterAccessor"/> used for testing
        /// </summary>
        public int UpdateBooster(Booster booster)
        {
            int count = 0;
            Booster updatedElement = null;

            foreach (Booster element in _boosters)
            {
                if (element.Abbreviation == booster.Abbreviation &&
                    element.BoosterID != booster.BoosterID)
                {
                    throw new Exception("Abbreviation already used.");
                }
                if (element.BoosterID == booster.BoosterID)
                {
                    updatedElement = element;
                    break;
                }
            }
            if (updatedElement != null)
            {
                updatedElement = booster;
                count++;
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IBoosterAccessor"/> used for testing
        /// </summary>
        public int DeleteBooster(string boosterID)
        {
            int count = 0;
            Booster deleteBooster = null;

            foreach (Booster booster in _boosters)
            {
                if (booster.BoosterID == boosterID)
                {
                    count++;
                    deleteBooster = booster;
                }
            }

            if (count == 1)
            {
                _boosters.Remove(deleteBooster);
            }

            return count;
        }
    }
}
