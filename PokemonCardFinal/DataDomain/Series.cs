using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Series
    {
        public string SeriesID { get; set; }
        public int BoosterCount { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImagePath { get; set; }
        public bool Active { get; set; }
    }
}
