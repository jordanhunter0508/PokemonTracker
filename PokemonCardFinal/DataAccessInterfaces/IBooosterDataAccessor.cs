using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface IBooosterDataAccessor
    {
        public Booster SelectBoosterByBoosterID(string boosterID);
        public List<Booster> SelectBoosters();
        public bool InsertBooster(Booster booster);
        public bool UpdateBoosterByBoosterID(Booster booster);
        public bool DeleteBooosterByBoosterID(string boosterID);
    }
}
