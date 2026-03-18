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
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public MoveVM GetMoveVMByMoveID(int moveID)
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
        public Move GetMoveByMoveID(int moveID)
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
        public List<MoveCost> GetMoveCostsByMoveID(int moveID)
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
        public List<MoveVM> GetAllMoveVMs()
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
                        Name = move.Name,
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
        public PaginatedResult<Move> GetActiveMoves(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<Move> results = new PaginatedResult<Move>();

            if (pageNumber <= 0)
            {
                throw new ArgumentException("Page number must be greater than 0.");
            }
            if (pageSize <= 0)
            {
                throw new ArgumentException("Page size must be greater than 0.");
            }

            try
            {
                results = _moveAccessor.SelectActiveMoves(pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve a list of active moves.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IMoveManager"/>
        /// </summary>
        public PaginatedResult<Move> GetDeactiveMoves(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<Move> results = new PaginatedResult<Move>();

            if (pageNumber <= 0)
            {
                throw new ArgumentException("Page number must be greater than 0.");
            }
            if (pageSize <= 0)
            {
                throw new ArgumentException("Page size must be greater than 0.");
            }

            try
            {
                results = _moveAccessor.SelectDeactiveMoves(pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve a list of deactive moves.", ex);
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

            if (moveVM == null)
            {
                throw new ArgumentNullException("Failed to add a Move. MoveVM was null.");
            }

            try
            {
                int moveID = AddMove(moveVM);

                foreach (MoveCost cost in moveVM.Costs)
                {
                    // Makes sure the moveCost is going to the correct move
                    cost.MoveID = moveID;
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
        public int AddMove(Move move)
        {
            int result = -1;

            if (move == null)
            {
                throw new ArgumentNullException("Move was empty");
            }

            try
            {
                result = _moveAccessor.InsertMove(move);

                if (result == -1)
                {
                    throw new ApplicationException("Faild to add a move to the database.");
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
        public bool DeleteMove(int moveID)
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

        /// <summary>
        /// Implements from <see cref="IMoveManager"/>
        /// </summary>
        public bool EditMoveVM(MoveVM moveVM)
        {
            bool isEdited = false;
            bool isValid = true;

            if (moveVM == null)
            {
                throw new ArgumentNullException("Failed to update a move. MoveVM was null.");
            }

            try
            {
                if (!EditMove(moveVM))
                {
                    isValid = false;
                }

                DeleteMoveCost(moveVM.MoveID);

                // Deletes each move then recreates it.
                // Because the pk is a combo of two fk
                // Someone couldn't update the element type of a move
                // only the quantity
                foreach (MoveCost cost in moveVM.Costs)
                {
                    cost.MoveID = moveVM.MoveID;
                    Debug.WriteLine(cost.ElementType + " " + cost.MoveID + " " + cost.Quantity);
                    if (!AddMoveCost(cost))
                    {
                        Debug.WriteLine("Inside if add fails.");
                        isValid = false;
                        break;
                    }
                    else { Debug.WriteLine("movecost added"); }
                }

                if (isValid)
                {
                    isEdited = true;
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to update a move VM.", ex);
            }

            return isEdited;
        }

        /// <summary>
        /// Implements from <see cref="IMoveManager"/>
        /// </summary>
        public bool EditMove(Move move)
        {
            bool isEdited = false;

            if (move == null)
            {
                throw new ArgumentNullException("Failed to update a move. Move was null.");
            }

            try
            {
                isEdited = (1 == _moveAccessor.UpdateMove(move));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to update a move.", ex);
            }

            return isEdited;
        }

        /// <summary>
        /// Implements from <see cref="IMoveManager"/>
        /// </summary>
        public bool DeleteMoveCost(int moveID)
        {
            bool result = false;

            try
            {
                result = (1 <= _moveAccessor.DeleteMoveCost(moveID));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to delete a move.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IMoveManager"/>
        /// </summary>
        public bool DeactivateMove(int moveID)
        {
            bool result = true;

            try
            {
                result = (1 == _moveAccessor.DeactivateMove(moveID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to deactivate the move.", ex);
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IMoveManager"/>
        /// </summary>
        public bool ReactivateMove(int moveID)
        {
            bool result = true;

            try
            {
                result = (1 == _moveAccessor.ReactivateMove(moveID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to reactivate the move.", ex);
            }

            return result;
        }
    }
}

