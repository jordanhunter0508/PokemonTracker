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
        /// <summary>
        /// General MoveManager for the presentation layer
        /// </summary>
        public MoveManager() 
        {
            _moveAccessor = new MoveAccessor();
        }

        /// <summary>
        /// Used for testing to pass in fakes
        /// </summary>
        /// <param name="moveAccessor">Used to set the _moveAccessor to a specific IMoveAccessor</param>
        public MoveManager(IMoveAccessor moveAccessor) 
        {
            _moveAccessor = moveAccessor;
        }

        public MoveVM GetMoveVMByMoveID(string moveID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Implements from <see cref="IMoveManager"/>
        /// </summary>
        public Move GetMovseByMoveID(string moveID)
        {
            Move restultMove = null;

            try
            {
                restultMove = _moveAccessor.SelectMoveByMoveID(moveID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get move.", ex);
            }

            return restultMove;
        }
    
    }
}
