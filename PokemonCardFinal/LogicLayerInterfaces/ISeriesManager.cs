using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface ISeriesManager
    {
        /// <summary>
        /// Calls <see href="ISeriesAccessor.SelectAllSeries()"/> <br/>
        /// To get a list of all Series with all properties attached
        /// </summary>
        /// <returns>Returns a list of all series</returns>
        /// <exception cref="ApplicationException">Throws if there is an error connecting to the database</exception>
        public List<Series> GetAllSeries();

        /// <summary>
        /// Calls <see href="ISeriesAccessor.SelectSeriesImagePaths()"/> <br/>
        /// To get all active list of Series with only the id and imagePath
        /// </summary>
        /// <returns>Returns a list of all active Series with only the imagePath and ID</returns>
        /// <exception cref="ApplicationException">Throws if there is an error connecting to the database</exception>
        public List<Series> GetSeriesImagePaths();

        /// <summary>
        /// Changes the Active field for a single series
        /// based on the active parameter
        /// </summary>
        /// <param name="seriesID">Used to find the Series</param>
        /// <param name="active">Used to reactivate or deactivate</param>
        /// <returns>Returns true if 1 row was affected, false otherwise</returns>
        public bool ActivateSeries(string seriesID, bool active);

        /// <summary>
        /// Changes the Active field for all boosters related to the seriesID
        /// based on the active parameter
        /// </summary>
        /// <param name="seriesID">Used to find the Series</param>
        /// <param name="active">Used to reactivate or deactivate</param>
        /// <returns>Returns true if 1 row was affected, false otherwise</returns>
        public bool ActivateBoostersBySeriesID(string seriesID, bool active);

        /// <summary>
        /// Changes the Active field for all cards related to the seriesID
        /// based on the active parameter
        /// </summary>
        /// <param name="seriesID">Used to find the Series</param>
        /// <param name="active">Used to reactivate or deactivate</param>
        /// <returns>Returns true if 1 row was affected, false otherwise</returns>
        public bool ActivateCardsBySeriesID(string seriesID, bool active);
    }
}
