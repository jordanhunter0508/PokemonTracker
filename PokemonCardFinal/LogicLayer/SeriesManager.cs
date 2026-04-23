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
    public class SeriesManager : ISeriesManager
    {
        private ISeriesAccessor _seriesAccessor;

        /// <summary>
        /// General SeriesManager created for the presentaion layer
        /// </summary>
        public SeriesManager()
        {
            _seriesAccessor = new SeriesAccessor();
        }

        /// <summary>
        /// Used for testing to pass in fake data
        /// </summary>
        /// <param name="seriesAccessor">Set the ISeriesAccessor in the SeriesManager</param>
        public SeriesManager(ISeriesAccessor seriesAccessor)
        {
            _seriesAccessor = seriesAccessor;
        }

        /// <summary>
        /// Implements from <see cref="ISeriesManager"/>
        /// </summary>
        public List<Series> GetAllSeries()
        {
            List<Series> results = new List<Series>();

            try
            {
                results = _seriesAccessor.SelectAllSeries();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get a list of all series.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ISeriesManager"/>
        /// </summary>
        public List<Series> GetSeriesImagePaths()
        {
            List<Series> results = new List<Series>();

            try
            {
                results = _seriesAccessor.SelectSeriesImagePaths();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get a list of series images.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ISeriesManager"/>
        /// </summary>
        public bool ActivateSeries(string seriesID, bool active)
        {
            bool wasUpdated = false;

            if (string.IsNullOrWhiteSpace(seriesID))
            {
                throw new ArgumentException("SeriesID was either null or blank.");
            }

            try
            {
                wasUpdated = (1 == _seriesAccessor.ActivateSeries(seriesID, active));
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to update {seriesID}'s active status.", ex);
            }

            return wasUpdated;
        }

        /// <summary>
        /// Implements from <see cref="ISeriesManager"/>
        /// </summary>
        public bool ActivateBoostersBySeriesID(string seriesID, bool active)
        {
            bool wasUpdated = false;

            if (string.IsNullOrWhiteSpace(seriesID))
            {
                throw new ArgumentException("SeriesID was either null or blank.");
            }

            try
            {
                ActivationResults results = _seriesAccessor.ActivateBoostersBySeriesID(seriesID, active);

                if (results.ExpectedCount != 0)
                {
                    wasUpdated = (results.ExpectedCount == results.UpdatedCount);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to update boosters active status. Related to {seriesID}", ex);
            }

            return wasUpdated;
        }

        /// <summary>
        /// Implements from <see cref="ISeriesManager"/>
        /// </summary>
        public bool ActivateCardsBySeriesID(string seriesID, bool active)
        {
            bool wasUpdated = false;

            if (string.IsNullOrWhiteSpace(seriesID))
            {
                throw new ArgumentException("SeriesID was either null or blank.");
            }

            try
            {
                ActivationResults results = _seriesAccessor.ActivateCardsBySeriesID(seriesID, active);
                if (results.ExpectedCount != 0)
                {
                    wasUpdated = (results.ExpectedCount == results.UpdatedCount);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to update cards active status. Related to {seriesID}", ex);
            }

            return wasUpdated;
        }
    }
}
