using System;
using System.Collections.Generic;
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
                MoveID = "test move 1",
                Damage = 10,
                Description = "This is a test move."
            });
            _moves.Add(new Move()
            {
                MoveID = "test move 2",
                Damage = 100,
                Description = "This is a test move."
            });
            _moves.Add(new Move()
            {
                MoveID = "test move 3",
                Damage = 0,
                Description = "This is a test move."
            });

            _moveCosts.Add(new MoveCost()
            {
                MoveID = "test move 1",
                ElementType = "element",
                Quantity = 1,
            });
            _moveCosts.Add(new MoveCost()
            {
                MoveID = "test move 1",
                ElementType = "test element",
                Quantity = 2,
            });
            _moveCosts.Add(new MoveCost()
            {
                MoveID = "test move 2",
                ElementType = "test element",
                Quantity = 2,
            });
            _moveCosts.Add(new MoveCost()
            {
                MoveID = "test move 2",
                ElementType = "test element",
                Quantity = 2,
            });


            _moveVMs.Add(new MoveVM()
            { 
                MoveID = _moves[0].MoveID,
                Damage = _moves[0].Damage,
                Description = _moves[0].Description,
                Costs = SelectMoveCostsByMoveID(_moves[0].MoveID),
            });
            _moveVMs.Add(new MoveVM()
            { 
                MoveID = _moves[1].MoveID,
                Damage = _moves[1].Damage,
                Description = _moves[1].Description,
                Costs = SelectMoveCostsByMoveID(_moves[1].MoveID),
            });
            _moveVMs.Add(new MoveVM()
            { 
                MoveID = _moves[2].MoveID,
                Damage = _moves[2].Damage,
                Description = _moves[2].Description,
                Costs = SelectMoveCostsByMoveID(_moves[2].MoveID),
            });
        }

        /// <summary>
        /// Implements from <see cref="IMoveAccessor"/> used for testing
        /// </summary>
        public Move SelectMoveByMoveID(string moveID)
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
        public List<MoveCost> SelectMoveCostsByMoveID(string moveID)
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
        public List<MoveVM> SelectMoveVMs()
        {
            List<MoveVM> results = null;
            results = _moveVMs;
            return results;
        }
    }
}
