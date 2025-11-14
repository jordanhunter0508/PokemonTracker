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

        /// <summary>
        /// Implements from <see cref="IMoveManager"/>
        /// </summary>
        public MoveVM GetMoveVMByMoveID(string moveID)
        {
            MoveVM resultMoveVM = null;
            try
            {
                Move move = GetMoveByMoveID(moveID);

                resultMoveVM = new MoveVM()
                {
                    MoveID = move.MoveID,
                    Damage = move.Damage,
                    Description = move.Description,
                    Costs = GetMoveCostsByMoveID(moveID),
                };
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Faild to get move.", ex);
            }

            if (resultMoveVM == null)
            {
                throw new ApplicationException("Failed to get move. Move was null.");
            }

            return resultMoveVM;
        }

        /// <summary>
        /// Implements from <see cref="IMoveManager"/>
        /// </summary>
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

        /// <summary>
        /// Implements from <see cref="IMoveManager"/>
        /// </summary>
        public List<MoveCost> GetMoveCostsByMoveID(string moveID)
        {
            List<MoveCost> resultMoveCosts = null;

            try
            {
                resultMoveCosts = _moveAccessor.SelectMoveCostsByMoveID(moveID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get move costs.", ex);
            }

            return resultMoveCosts;
        }

        public List<MoveVM> GetMoveVMs()
        {
            List<MoveVM> results = null;

            try
            {
                results = _moveAccessor.SelectMoveVMs();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get move costs.", ex);
            }

            return results;
        }
    }
}
