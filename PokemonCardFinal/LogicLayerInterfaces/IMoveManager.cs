using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface IMoveManager
    {
        public Move GetMoveByMoveID(string moveID);
    }
}
