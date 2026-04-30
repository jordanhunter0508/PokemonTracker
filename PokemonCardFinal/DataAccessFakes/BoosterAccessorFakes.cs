using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
        List<Series> _series;
        List<Card> _cards;

        /// <summary>
        /// Fills the _boosters list with fake data
        /// </summary>
        public BoosterAccessorFakes()
        {
            _boosters = new List<Booster>();
            _boosters.Add(new Booster()
            {
                BoosterID = "Test Booster 1",
                SeriesID = "Series 2",
                ReleaseDate = DateTime.Parse("2025-11-06"),
                Abbreviation = "test",
                BaseCount = 1,
                SecretCount = 1,
                LogoPath = null,
                SymbolPath = null,
                Active = true,
                IsFull = false,
            });
            _boosters.Add(new Booster()
            {
                BoosterID = "Test Booster 2",
                SeriesID = "Series 1",
                ReleaseDate = DateTime.Parse("1994-01-28"),
                Abbreviation = "ser",
                BaseCount = 3,
                SecretCount = 11,
                LogoPath = null,
                SymbolPath = null,
                Active = true,
                IsFull = true,
            });
            _boosters.Add(new Booster()
            {
                BoosterID = "Test Booster 3",
                SeriesID = "Series 3",
                ReleaseDate = DateTime.Parse("2003-10-10"),
                Abbreviation = "abv",
                BaseCount = 1,
                SecretCount = 3,
                LogoPath = null,
                SymbolPath = null,
                Active = true,
                IsFull = true,
            });
            _boosters.Add(new Booster()
            {
                BoosterID = "Test Booster 4",
                SeriesID = "Series 1",
                ReleaseDate = DateTime.Parse("2003-10-10"),
                Abbreviation = "abv",
                BaseCount = 10,
                SecretCount = 13,
                LogoPath = null,
                SymbolPath = null,
                Active = false,
                IsFull = false,
            });
            _boosters.Add(new Booster()
            {
                BoosterID = "Test Booster 5",
                SeriesID = "Series 4",
                ReleaseDate = DateTime.Parse("2003-10-10"),
                Abbreviation = "abv",
                BaseCount = 10,
                SecretCount = 13,
                LogoPath = null,
                SymbolPath = null,
                Active = true,
                IsFull = false,
            });

            _series = new List<Series>();
            _series.Add(new Series()
            {
                SeriesID = "Series 1",
                ImagePath = "image/path",
                Active = true,
            });
            _series.Add(new Series()
            {
                SeriesID = "Series 2",
                ImagePath = "image/path2",
                Active = true,
            });
            _series.Add(new Series()
            {
                SeriesID = "Series 3",
                ImagePath = "image/path3",
                Active = true,
            });
            _series.Add(new Series()
            {
                SeriesID = "Series 4",
                ImagePath = "image/path4",
                Active = false,
            });

            _cards = new List<Card>();
            _cards.Add(new Card()
            {
                CardID = 1,
                BoosterID = "Test Booster 1",
                Active = true,
            });
            _cards.Add(new Card()
            {
                CardID = 2,
                BoosterID = "Test Booster 1",
                Active = true,
            });
            _cards.Add(new Card()
            {
                CardID = 3,
                BoosterID = "Test Booster 4",
                Active = true,
            });
            _cards.Add(new Card()
            {
                CardID = 4,
                BoosterID = "Test Booster 4",
                Active = true,
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
        public List<string> SelectBoosterIDs()
        {
            List<string> results = null;
            results = _boosters.Select(b => b.BoosterID).ToList();

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IBoosterAccessor"/> used for testing
        /// </summary>
        public List<string> SelectActiveBoosterIDs()
        {
            List<string> results = new List<string>();

            foreach (var booster in _boosters)
            {
                foreach (var series in _series)
                {
                    if (string.Equals(booster.SeriesID, series.SeriesID) &&
                        booster.Active && series.Active)
                    {
                        results.Add(booster.BoosterID);

                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IBoosterAccessor"/> used for testing
        /// </summary>
        public List<Booster> SelectActiveBoosters()
        {
            List<Booster> results = new List<Booster>();

            foreach (var booster in _boosters)
            {
                foreach (var series in _series)
                {
                    if (string.Equals(booster.SeriesID, series.SeriesID) &&
                        booster.Active && series.Active)
                    {
                        results.Add(booster);

                    }
                }
            }

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
                else if (boosters.Abbreviation == booster.Abbreviation)
                {
                    throw new Exception("Abbreviation already used.");
                }
            }

            var validSeriesID = _series.Select(s => s.SeriesID);

            if (!validSeriesID.Contains(booster.SeriesID))
            {
                throw new Exception("Invalid seriesID.");
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
                    count++;
                }
            }
            if (updatedElement != null)
            {
                updatedElement = booster;
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

        /// <summary>
        /// Implements from <see cref="IBoosterAccessor"/> used for testing
        /// </summary>
        public int ActivateBooster(string boosterID, bool active)
        {
            int count = 0;

            Booster booster = _boosters.FirstOrDefault(b => b.BoosterID.Equals(boosterID));

            if (booster != null)
            {
                booster.Active = active;
                count++;
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IBoosterAccessor"/> used for testing
        /// </summary>
        public ActivationResults ActivateCardsByBoosterID(string boosterID, bool active)
        {
            ActivationResults results = new ActivationResults();

            results.ExpectedCount = _cards.Where(c => c.BoosterID.Equals(boosterID)).Count();

            foreach (Card card in _cards)
            {
                if (card.BoosterID.Equals(boosterID))
                { 
                    card.Active = active;
                    results.UpdatedCount++;
                }
            }

            return results;
        }
    }
}
