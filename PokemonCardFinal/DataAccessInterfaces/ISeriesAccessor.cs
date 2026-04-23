using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface ISeriesAccessor
    {
        /// <summary>
        /// Requests all fields for all series from the database.
        /// </summary>
        /// <returns>Returns a list of all series in the database</returns>
        public List<Series> SelectAllSeries();

        /// <summary>
        /// Requests all active image paths from the series table
        /// </summary>
        /// <returns>Returns a list of all active Series with only the imagePath and ID</returns>
        public List<Series> SelectSeriesImagePaths();

        /// <summary>
        /// Changes the Active field for a single series
        /// based on the active parameter
        /// </summary>
        /// <param name="seriesID">Used to find the Series</param>
        /// <param name="active">Used to reactivate or deactivate</param>
        /// <returns>Returns true if 1 row was affected, false otherwise</returns>
        public int ActivateSeries(string seriesID, bool active);

        /// <summary>
        /// Changes the Active field for all boosters related to the seriesID
        /// based on the active parameter
        /// </summary>
        /// <param name="seriesID">Used to find the Series</param>
        /// <param name="active">Used to reactivate or deactivate</param>
        /// <returns>Returns true if 1 row was affected, false otherwise</returns>
        public ActivationResults ActivateBoostersBySeriesID(string seriesID, bool active);

        /// <summary>
        /// Changes the Active field for all cards related to the seriesID
        /// based on the active parameter
        /// </summary>
        /// <param name="seriesID">Used to find the Series</param>
        /// <param name="active">Used to reactivate or deactivate</param>
        /// <returns>Returns true if 1 row was affected, false otherwise</returns>
        public ActivationResults ActivateCardsBySeriesID(string seriesID, bool active);
    }
}
