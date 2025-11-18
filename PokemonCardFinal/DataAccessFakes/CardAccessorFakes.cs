using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class CardAccessorFakes : ICardAccessor
    {
        List<Card> _cards;
        List<MoveVM> _moves;

        // This is used to represent a join table
        List<CardMove> _cardMoves;
        List<CardAlternateArt> _cardAlternateArts;

        /// <summary>
        /// Fills the _cards list with fake data
        /// </summary>
        public CardAccessorFakes() 
        {
            _cards = new List<Card>();
            _cards.Add(new Card()
            { 
                CardID = 1,
                ArtistID = 1,
                AbilityID = "test ability 1",
                BoosterID = "test booster 1",
                PokemonRuleID = "test pokemon rule 1",
                ElementTypeID = "test element",
                Name = "test 1",
                BoosterNumber = 1,
                CardType = "test type 1",
                Rarity = "test rarity 1",
                WeaknessType = "weakness 1",
                ResistanceType = "resistance 1",
                WeaknessValue = 1,
                ResistanceValue = 1,
                RetreatCost = 1,
                Health = 100,
                Stage = "test stage"
            });
            _cards.Add(new Card()
            { 
                CardID = 2,
                ArtistID = 1,
                AbilityID = "test ability 2",
                BoosterID = "test booster 1",
                PokemonRuleID = "test pokemon rule 2",
                ElementTypeID = "test element",
                Name = "test 2",
                BoosterNumber = 2,
                CardType = "test type 1",
                Rarity = "test rarity 1",
                WeaknessType = "weakness 2",
                ResistanceType = "resistance 2",
                WeaknessValue = 1,
                ResistanceValue = 1,
                RetreatCost = 1,
                Health = 100,
                Stage = "test stage"
            });
            _cards.Add(new Card()
            { 
                CardID = 3,
                ArtistID = 2,
                AbilityID = "test ability 1",
                BoosterID = "test booster 3",
                PokemonRuleID = "test pokemon rule 3",
                ElementTypeID = "test element",
                Name = "test 1",
                BoosterNumber = 1,
                CardType = "test type 3",
                Rarity = "test rarity 1",
                WeaknessType = "weakness 1",
                ResistanceType = "resistance 1",
                WeaknessValue = 1,
                ResistanceValue = 1,
                RetreatCost = 1,
                Health = 100,
                Stage = "test stage"
            });

            _moves = new List<MoveVM>();
            _moves.Add(new MoveVM()
            { 
                MoveID = "testMove1",
                Damage = 1,
                Description = "This is a test move for card 1."
            });
            _moves.Add(new MoveVM()
            { 
                MoveID = "testMove2",
                Damage = 1,
                Description = "This is a test move for card 1."
            });
            _moves.Add(new MoveVM()
            { 
                MoveID = "testMove3",
                Damage = 1,
                Description = "This is a test move for card 22."
            });

            _cardMoves = new List<CardMove>();
            _cardMoves.Add(new CardMove()
            {
                CardID = 1,
                MoveID = "testMove1"
            });
            _cardMoves.Add(new CardMove()
            {
                CardID = 1,
                MoveID = "testMove2"
            });
            _cardMoves.Add(new CardMove()
            {
                CardID = 2,
                MoveID = "testMove2"
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
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public Card SelectCardByCardID(int cardID)
        {
            Card resultCard = null;

            foreach (Card card in _cards)
            {
                if (card.CardID == cardID)
                { 
                    resultCard = card;
                }
            }

            return resultCard;
        }
        
        /// <summary>
        /// Implements from <see cref="ICardAccessor"/> used for testing
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
        /// Implements from <see cref="ICardAccessor"/> used for testing
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
        /// Used to help SelectMovesByCardID find the correct Move
        /// </summary>
        private MoveVM SelectMoveVMByMoveID(string moveID)
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
    }

    // Used to represent the join table from the database
    // used for testing purposes only.
    internal class CardMove 
    {
        public int CardID { get; set; }
        public string MoveID { get; set; }
    }

    // Used to represent the join table from the database
    // used for testing purposes only.
    internal class CardAlternateArt 
    {
        public int CardID { get; set; }
        public string AlternateArtID { get; set; }
    }
}
