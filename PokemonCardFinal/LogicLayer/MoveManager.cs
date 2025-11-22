using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        /// <summary>
        /// Implements from <see cref="IMoveManager"/>
        /// </summary>
        public List<MoveVM> GetMoveVMs()
        {
            List<MoveVM> results = null;
            List<Move> moves = null;

            try
            {
                results = _moveAccessor.SelectMoveVMsWithMoveCost();
                moves = GetMovesWithoutMoveCost();
                foreach (Move move in moves)
                {
                    results.Add(new MoveVM()
                    {
                        MoveID = move.MoveID,
                        Damage = move.Damage,
                        Description = move.Description,
                        Costs = new List<MoveCost>()
                    });
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get a list of moves and there costs.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IMoveManager"/>
        /// </summary>
        public List<MoveVM> GetMoveVMsWithMoveCost()
        {
            List<MoveVM> results = null;

            try
            {
                results = _moveAccessor.SelectMoveVMsWithMoveCost();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get a list of moves and there costs.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IMoveManager"/>
        /// </summary>
        public List<Move> GetMovesWithoutMoveCost()
        {
            List<Move> results = null;

            try
            {
                results = _moveAccessor.SelectMovesWithoutMoveCost();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to get a list of moves.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IMoveManager"/>
        /// </summary>
        public bool AddMoveVM(MoveVM moveVM)
        {
            bool result = false;
            bool valid = true;

            try
            {
                if (!AddMove(moveVM))
                {
                    valid = false;
                }

                foreach (MoveCost cost in moveVM.Costs)
                {
                    if (!AddMoveCost(cost))
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add a move to the database\n", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IMoveManager"/>
        /// </summary>
        public bool AddMove(Move move)
        {
            bool result = false;

            if (move == null)
            {
                throw new ArgumentNullException("Move was empty");
            }

            try
            {
                result = (1 == _moveAccessor.InsertMove(move));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add a move to the database\n", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IMoveManager"/>
        /// </summary>
        public bool AddMoveCost(MoveCost cost)
        {
            bool result = true;

            if (cost == null)
            {
                throw new ArgumentNullException("Move was empty");
            }

            try
            {
                result = (1 == _moveAccessor.InsertMoveCost(cost));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add a move cost to the database\n", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IMoveManager"/>
        /// </summary>
        public bool DeleteMove(string moveID)
        {
            bool result = false;

            try
            {
                result = (1 == _moveAccessor.DeleteMove(moveID));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to delete a move.", ex);
            }

            return result;
        }
    }
}
