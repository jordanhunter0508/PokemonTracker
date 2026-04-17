using DataDomain;

namespace Website.Models
{
    /// <summary>
    /// Holds a Series object with a list of all bosters conencted to the series
    /// </summary>
    public class SeriesWithBoosters
    {
        public Series Series {get;set;}
        public List<Booster> Boosters { get; set; } = new List<Booster>();
    }
}
