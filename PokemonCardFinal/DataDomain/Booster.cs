using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Booster
    {
        [DisplayName("Set Name")]
        public string BoosterID { get; set; }

        [DisplayName("Set Series")]
        public string SeriesID { get; set; }

        [DisplayName("Release Date")]
        public DateTime ReleaseDate { get; set; }

        [DisplayName("Set Abbreviation")]
        public string Abbreviation { get; set; }

        [DisplayName("Base Card Count")]
        public int? BaseCount { get; set; }

        [DisplayName("Secret Card Count")]
        public int? SecretCount { get; set; }

        [DisplayName("Total Card Count")]
        public int? TotalCount 
        {
            get
            {
                return BaseCount + SecretCount;
            }
        }

        [DisplayName("Logo Path")]
        public string LogoPath { get; set; }

        [DisplayName("Symbol Path")]
        public string SymbolPath { get; set; }
        public bool Active { get; set; }
        public bool IsFull { get; set; }
    }
}
