using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;
using static DataAccessFakes.CardComponentAccessorFakes;

namespace DataAccessFakes
{
    public class CardComponentAccessorFakes : ICardComponentAccessor
    {
        List<MoveVM> _moves;

        List<CardMove> _cardMoves;
        List<CardAlternateArt> _cardAlternateArts;

        /// <summary>
        /// Loads the fake data for _moves, _cardMoves, _cardAlternateArts
        /// </summary>
        public CardComponentAccessorFakes()
        {
            _cardMoves = new List<CardMove>();
            _cardMoves.Add(new CardMove()
            {
                CardID = 1,
                MoveID = 1
            });
            _cardMoves.Add(new CardMove()
            {
                CardID = 1,
                MoveID = 2
            });
            _cardMoves.Add(new CardMove()
            {
                CardID = 2,
                MoveID = 2
            });

            _cardAlternateArts = new List<CardAlternateArt>();
            _cardAlternateArts.Add(new CardAlternateArt()
            {
                CardID = 1,
                AlternateArtID = "test Alternate Art 1"
            });
            _cardAlternateArts.Add(new CardAlternateArt()
            {
                CardID = 1,
                AlternateArtID = "test Alternate Art 2"
            });
            _cardAlternateArts.Add(new CardAlternateArt()
            {
                CardID = 2,
                AlternateArtID = "test Alternate Art 1"
            });

            _moves = new List<MoveVM>();
            _moves.Add(new MoveVM()
            {
                MoveID = 1,
                Name = "testMove1",
                Damage = 1,
                Description = "This is a test move for card 1."
            });
            _moves.Add(new MoveVM()
            {
                MoveID = 2,
                Name = "testMove2",
                Damage = 1,
                Description = "This is a test move for card 1."
            });
            _moves.Add(new MoveVM()
            {
                MoveID = 3,
                Name = "testMove3",
                Damage = 1,
                Description = "This is a test move for card 22."
            });
        }

        /// <summary>
        /// Implements from <see cref="ICardComponentAccessor"/> used for testing
        /// </summary>
        public List<string> SelectAlternateArtsByCardID(int cardID)
        {
            List<string> results = new List<string>();

            foreach (CardAlternateArt cardAlternateArt in _cardAlternateArts)
            {
                if (cardAlternateArt.CardID == cardID)
                {
                    results.Add(cardAlternateArt.AlternateArtID);
                }
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardComponentAccessor"/> used for testing
        /// </summary>
        public List<MoveVM> SelectMovesByCardID(int cardID)
        {
            List<MoveVM> results = new List<MoveVM>();

            foreach (CardMove cardMove in _cardMoves)
            {
                if (cardMove.CardID == cardID)
                {
                    results.Add(SelectMoveVMByMoveID(cardMove.MoveID));
                }
            }
            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardComponentAccessor"/> used for testing
        /// </summary>
        public int InsertCardMove(int cardID, int moveID)
        {
            int count = 0;
            int[] validMoveIDs = { 1, 2, 3 };
            int[] validCardIDs = { 1, 2, 3, 4, 5, 6, 7 };

            if (!validCardIDs.Contains(cardID))
            {
                throw new Exception("Card not in the table.");
            }
            if (!validMoveIDs.Contains(moveID))
            {
                throw new Exception("Move not in the table.");
            }

            foreach (CardMove cardMove in _cardMoves)
            {
                if (cardMove.CardID == cardID &&
                   cardMove.MoveID == moveID)
                {
                    throw new Exception("Card already has this Move.");
                }
            }

            _cardMoves.Add(new CardMove()
            {
                CardID = cardID,
                MoveID = moveID,
            });
            count = 1;

            return count;
        }

        /// <summary>
        /// Implements from <see cref="ICardComponentAccessor"/> used for testing
        /// </summary>
        public int DeleteCardMoves(int cardID)
        {
            int count = 0;

            for (int i = _cardMoves.Count - 1; i >= 0; i--)
            {
                if (_cardMoves[i].CardID == cardID)
                {
                    _cardMoves.RemoveAt(i);
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="ICardComponentAccessor"/> used for testing
        /// </summary>
        public int InsertCardAlternateArt(int cardID, string alternateArtID)
        {
            int count = 0;
            string[] validAlternateArts = { "test Alternate Art 1", "test Alternate Art 2", "test Alternate Art 3" };
            int[] validCardIDs = { 1, 2, 3, 4, 5, 6, 7 };

            if (!validCardIDs.Contains(cardID))
            {
                throw new Exception("Card not in the table.");
            }
            if (!validAlternateArts.Contains(alternateArtID))
            {
                throw new Exception("Alternate Art not in the table.");
            }

            for (int i = 0; i < _cardAlternateArts.Count; i++)
            {
                if (_cardAlternateArts[i].CardID == cardID &&
                   _cardAlternateArts[i].AlternateArtID == alternateArtID)
                {
                    throw new Exception("Card already has this Alternate Art.");
                }
            }

            _cardAlternateArts.Add(new CardAlternateArt()
            {
                CardID = cardID,
                AlternateArtID = alternateArtID,
            });
            count = 1;

            return count;
        }

        /// <summary>
        /// Implements from <see cref="ICardComponentAccessor"/> used for testing
        /// </summary>
        public int DeleteCardAlternateArts(int cardID)
        {
            int count = 0;

            for (int i = _cardAlternateArts.Count - 1; i >= 0; i--)
            {
                if (_cardAlternateArts[i].CardID == cardID)
                {
                    _cardAlternateArts.RemoveAt(i);
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Used to help SelectMovesByCardID find the correct Move
        /// </summary>
        private MoveVM SelectMoveVMByMoveID(int moveID)
        {
            MoveVM resultMove = null;

            foreach (MoveVM move in _moves)
            {
                if (move.MoveID == moveID)
                {
                    resultMove = move;
                    break;
                }
            }

            return resultMove;
        }


        // Used to represent the join table from the database
        // used for testing purposes only.
        internal class CardMove
        {
            public int CardID { get; set; }
            public int MoveID { get; set; }
        }

        // Used to represent the join table from the database
        // used for testing purposes only.
        internal class CardAlternateArt
        {
            public int CardID { get; set; }
            public string AlternateArtID { get; set; }
        }
    }
}
