using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class MoveAccessorFakes : IMoveAccessor
    {
        List<Move> _moves = new List<Move> ();
        List<MoveVM> _moveVMs = new List<MoveVM>();
        List<MoveCost> _moveCosts = new List<MoveCost> ();

        /// <summary>
        /// Fills the _moves,_moveCosts list with fake data
        /// </summary>
        public MoveAccessorFakes()
        {
            _moves.Add(new Move()
            {
                MoveID = 1,
                Name = "test move 1",
                Damage = 10,
                Description = "This is a test move."
            });
            _moves.Add(new Move()
            {
                MoveID = 2,
                Name = "test move 2",
                Damage = 100,
                Description = "This is a test move."
            });
            _moves.Add(new Move()
            {
                MoveID = 3,
                Name = "test move 3",
                Damage = 0,
                Description = "This is a test move."
            });

            _moveCosts.Add(new MoveCost()
            {
                MoveID = 1,
                ElementType = "element",
                Quantity = 1,
            });
            _moveCosts.Add(new MoveCost()
            {
                MoveID = 1,
                ElementType = "test element",
                Quantity = 2,
            });
            _moveCosts.Add(new MoveCost()
            {
                MoveID = 2,
                ElementType = "element",
                Quantity = 2,
            });
            _moveCosts.Add(new MoveCost()
            {
                MoveID = 2,
                ElementType = "test element",
                Quantity = 2,
            });


            _moveVMs.Add(new MoveVM()
            { 
                MoveID = _moves[0].MoveID,
                Name = _moves[0].Name,
                Damage = _moves[0].Damage,
                Description = _moves[0].Description,
                Costs = SelectMoveCostsByMoveID(_moves[0].MoveID),
            });
            _moveVMs.Add(new MoveVM()
            { 
                MoveID = _moves[1].MoveID,
                Name = _moves[1].Name,
                Damage = _moves[1].Damage,
                Description = _moves[1].Description,
                Costs = SelectMoveCostsByMoveID(_moves[1].MoveID),
            });
            _moveVMs.Add(new MoveVM()
            { 
                MoveID = _moves[2].MoveID,
                Name = _moves[2].Name,
                Damage = _moves[2].Damage,
                Description = _moves[2].Description,
                Costs = SelectMoveCostsByMoveID(_moves[2].MoveID),
            });
        }

        /// <summary>
        /// Implements from <see cref="IMoveAccessor"/> used for testing
        /// </summary>
        public Move SelectMoveByMoveID(int moveID)
        {
            Move resultMove = null;
            foreach (Move move in _moves)
            {
                if (move.MoveID == moveID)
                {
                    resultMove = move;
                    break;
                }
            }
            return resultMove;
        }

        /// <summary>
        /// Implements from <see cref="IMoveAccessor"/> used for testing
        /// </summary>
        public List<MoveCost> SelectMoveCostsByMoveID(int moveID)
        {
            List<MoveCost> resultMoveCosts = new List<MoveCost>();
            foreach (MoveCost moveCost in _moveCosts)
            {
                if (moveCost.MoveID == moveID)
                {
                    resultMoveCosts.Add(moveCost);
                }
            }
            return resultMoveCosts;
        }

        /// <summary>
        /// Implements from <see cref="IMoveAccessor"/> used for testing
        /// </summary>
        public List<MoveVM> SelectMoveVMsWithMoveCost()
        {
            List<MoveVM> results = new List<MoveVM>();

            foreach (MoveVM moveVM in _moveVMs)
            {
                // if the move does have a move cost
                if (moveVM.Costs.Count > 0)
                { 
                    results.Add(moveVM);
                }
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IMoveAccessor"/> used for testing
        /// </summary>
        public List<Move> SelectMovesWithoutMoveCost()
        {
            List<Move> results = new List<Move>();

            foreach (MoveVM moveVM in _moveVMs)
            {
                // if the move does have a move cost
                if (moveVM.Costs.Count == 0)
                {
                    results.Add(moveVM);
                    //Debug.WriteLine(moveVM.Costs.Count);
                }
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IMoveAccessor"/> used for testing
        /// </summary>
        public int InsertMove(Move move)
        {
            int count = 0;

            foreach (Move element in _moves)
            {
                if (element.MoveID == move.MoveID)
                {
                    throw new Exception("MoveID was already used.");
                }
            }

            _moves.Add(move);
            count = 1;
            return count;
        }

        /// <summary>
        /// Implements from <see cref="IMoveAccessor"/> used for testing
        /// </summary>
        public int InsertMoveCost(MoveCost cost)
        {
            int count = 0;

            // Represents the ElementType table's IDs
            string[] elements = { "element", "test element", "new element" };

            if (SelectMoveByMoveID(cost.MoveID) == null)
            { 
                throw new Exception("MoveID does not have a corresponding Move.");
            }

            if (!elements.Contains(cost.ElementType))
            {
                throw new Exception("Element does not have a corresponding ElementTypeID.");
            }

            foreach (MoveCost element in _moveCosts)
            {
                if (element.MoveID == cost.MoveID && element.ElementType == cost.ElementType)
                {
                    throw new Exception("Both MoveID and ElementTypeID are duplicated.");
                }
            }

            _moveCosts.Add(cost);
            count = 1;
            return count;
        }

        /// <summary>
        /// Implements from <see cref="IMoveAccessor"/> used for testing
        /// </summary>
        public int DeleteMove(int moveID)
        {
            int count = 0;
            Move deletedMove = null;

            foreach (Move move in _moves)
            {
                if (moveID == move.MoveID)
                { 
                    deletedMove = move;
                }
            }

            if (deletedMove != null)
            {
                _moves.Remove(deletedMove);
                count = 1;
            }

            return count;
        }
    }
}
