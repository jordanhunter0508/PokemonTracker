using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccessInterfaces;
using DataDomain;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class MoveManager : IMoveManager
    {
        IMoveAccessor _moveAccessor;
        public MoveManager() 
        {
            _moveAccessor = new MoveAccessor();
        }

        public MoveManager(IMoveAccessor moveAccessor) 
        {
            _moveAccessor = moveAccessor;
        }

        public Move GetMoveByMoveID(string moveID)
        {
            Move resultMove = null;

            try
            {
                resultMove = _moveAccessor.SelectMoveByMoveID(moveID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get move.", ex);
            }

            return resultMove;
        }
    }
}
