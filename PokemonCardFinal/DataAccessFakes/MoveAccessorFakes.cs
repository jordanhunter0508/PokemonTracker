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
        }
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
    }
}
