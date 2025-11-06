using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface IBoosterManger
    {
        public Booster GetBoosterByBoosterID(string boosterID); 
        public List<Booster> GetBoosters();
        public bool AddBooster(Booster booster);
        public bool EditBoosterByBoosterID(Booster booster);
        public bool DeleteBooosterByBoosterID(string boosterID);
    }
}
