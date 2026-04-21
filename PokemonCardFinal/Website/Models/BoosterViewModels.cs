using DataDomain;

namespace Website.Models
{
    /// <summary>
    /// Holds a Series object with a list of all bosters conencted to the series
    /// </summary>
    public class SeriesWithBoosters
    {
        public Series Series { get; set; }
        public List<Booster> Boosters { get; set; } = new List<Booster>();
    }

    /// <summary>
    /// Holds a Booster and all cards from the database 
    /// with the same BoosterID
    /// </summary>
    public class BoosterDetailsVM 
    {
        public Booster Booster { get; set; }
        public List<Card> Cards { get; set; }
    }
}
