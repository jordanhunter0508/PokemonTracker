using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface IAltArtManager
    {
        public AlternateArt GetAlternateArtByID(string alternateArtID);
        public List<AlternateArt> GetAlternateArts();
        public bool AddAlternateArt(AlternateArt alternateArt);
        public bool EditAlternateArt(AlternateArt alternateArt);
        public bool DeleteAlternateArt(string alternateArtID);
    }
}
