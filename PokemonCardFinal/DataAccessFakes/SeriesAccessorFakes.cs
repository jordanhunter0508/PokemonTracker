using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class SeriesAccessorFakes : ISeriesAccessor
    {

        private List<Series> _series;
        private List<Booster> _boosters;
        private List<Card> _cards;

        /// <summary>
        /// Fills _series with fake data
        /// </summary>
        public SeriesAccessorFakes()
        {
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

            _boosters = new List<Booster>();
            _boosters.Add(new Booster() 
            { 
                BoosterID = "Booster 1",
                SeriesID = "Series 1",
                Active = true
            });
            _boosters.Add(new Booster() 
            { 
                BoosterID = "Booster 2",
                SeriesID = "Series 1",
                Active = true
            });
            _boosters.Add(new Booster() 
            { 
                BoosterID = "Booster 3",
                SeriesID = "Series 4",
                Active = false
            });
            _boosters.Add(new Booster() 
            { 
                BoosterID = "Booster 4",
                SeriesID = "Series 4",
                Active = false
            });

            _cards = new List<Card>();
            _cards.Add(new Card() 
            {
                CardID = 1,
                BoosterID = "Booster 1",
                Active = true,
            });
            _cards.Add(new Card() 
            {
                CardID = 2,
                BoosterID = "Booster 1",
                Active = false,
            });
            _cards.Add(new Card() 
            {
                CardID = 3,
                BoosterID = "Booster 2",
                Active = true,
            });
            _cards.Add(new Card() 
            {
                CardID = 4,
                BoosterID = "Booster 2",
                Active = false,
            });
            _cards.Add(new Card() 
            {
                CardID = 5,
                BoosterID = "Booster 3",
                Active = true,
            });
            _cards.Add(new Card() 
            {
                CardID = 6,
                BoosterID = "Booster 3",
                Active = false,
            });
        }

        /// <summary>
        /// Implements from <see cref="ISeriesAccessor"/> used for testing
        /// </summary>
        public List<Series> SelectAllSeries()
        {
            List<Series> results = new List<Series>();
            results = _series.ToList();
            return results;
        }

        /// <summary>
        /// Implements from <see cref="ISeriesAccessor"/> used for testing
        /// </summary>
        public List<Series> SelectSeriesImagePaths()
        {
            List<Series> result = new List<Series>();

            result = _series.Where(s => s.Active)
                            .Select(s => new Series
                            {
                                SeriesID = s.SeriesID,
                                ImagePath = s.ImagePath
                            }).ToList();
            return result;
        }

        /// <summary>
        /// Implements from <see cref="ISeriesAccessor"/> used for testing
        /// </summary>
        public int ActivateSeries(string seriesID, bool active)
        {
            int count = 0;
            Series series = _series.FirstOrDefault(s => s.SeriesID.Equals(seriesID));

            if (series != null)
            {
                series.Active = active;
                count++;
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="ISeriesAccessor"/> used for testing
        /// </summary>
        public ActivationResults ActivateBoostersBySeriesID(string seriesID, bool active)
        {
            ActivationResults results = new ActivationResults();

            results.ExpectedCount = _boosters.Where(b => b.SeriesID.Equals(seriesID)).Count();

            foreach (Booster booster in _boosters)
            {
                if (booster.SeriesID.Equals(seriesID))
                {
                    booster.Active = active;
                    results.UpdatedCount++;
                }
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ISeriesAccessor"/> used for testing
        /// </summary>
        public ActivationResults ActivateCardsBySeriesID(string seriesID, bool active)
        {
            ActivationResults results = new ActivationResults();
            results.ExpectedCount = 0;

            // Get all boosters with the series id
            IEnumerable<Booster> boosterIDs = _boosters.Where(b => b.SeriesID.Equals(seriesID));

            // Get all cards from the boosters with matching seriesIDs
            foreach (Booster booster in boosterIDs)
            { 
                results.ExpectedCount += _cards.Where(c => c.BoosterID.Equals(booster.BoosterID)).Count();
            }

            foreach (Booster booster in _boosters)
            {
                ActivateCard(seriesID, active, results, booster);
            }

            return results;
        }

        /// <summary>
        /// Helper method to activate cards based on the
        /// seriesID and boosterID
        /// </summary>
        /// <param name="seriesID">series to change activation of</param>
        /// <param name="active">Used to activate or deactivate</param>
        /// <param name="results">Used to update the UpdateCount property</param>
        /// <param name="booster">Checks this booster for cards if it has a matching seriesID</param>
        private void ActivateCard(string seriesID, bool active, ActivationResults results, Booster booster)
        {
            if (booster.SeriesID.Equals(seriesID))
            {
                foreach (Card card in _cards)
                {
                    if (card.BoosterID.Equals(booster.BoosterID))
                    {
                        card.Active = active;
                        results.UpdatedCount++;
                    }
                }
            }
        }
    }
}
