using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class CardAccessorFakes : ICardAccessor
    {
        List<Card> _cards;
        List<MoveVM> _moves;
        List<Booster> _boosters;

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
                ElementTypeID = "test element 1",
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
                ElementTypeID = "test element 1",
                Name = "test 2",
                BoosterNumber = 2,
                CardType = "test type 1",
                Rarity = "test rarity 2",
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
                ElementTypeID = "test element 2",
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

            _boosters = new List<Booster>();
            _boosters.Add(new Booster()
            {
                BoosterID = "test booster 1",
                Series = "test series",
                ReleaseDate = DateTime.Parse("2025-11-06"),
                Abbreviation = "test",
            });
            _boosters.Add(new Booster()
            {
                BoosterID = "test booster 2",
                Series = "booster 2 series",
                ReleaseDate = DateTime.Parse("1994-01-28"),
                Abbreviation = "ser",
            });
            _boosters.Add(new Booster()
            {
                BoosterID = "test booster 3",
                Series = "series",
                ReleaseDate = DateTime.Parse("2003-10-10"),
                Abbreviation = "abv",
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
        public List<Card> SelectCardsByReleaseDate(DateTime releaseDate)
        {
            List<Card> results = new List<Card>();
            string boosterID = "";

            foreach (Booster booster in _boosters)
            {
                if (booster.ReleaseDate == releaseDate)
                {
                    boosterID = booster.BoosterID;
                    break;
                }
            }

            foreach (Card card in _cards)
            {
                if (card.BoosterID == boosterID)
                {
                    results.Add(card);
                }
            }

            return results;
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
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public Dictionary<int, Card> SelectCards()
        {
            Dictionary<int, Card> results = new Dictionary<int, Card>();

            foreach (Card card in _cards)
            {
                results.Add(card.CardID, card);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public Dictionary<int, Card> SelectCardsByCardName(string name)
        {
            Dictionary<int, Card> results = new Dictionary<int, Card>();

            foreach (Card card in _cards)
            {
                if (card.Name.Contains(name))
                {
                    results.Add(card.CardID, card);
                }
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public Dictionary<int, List<MoveVM>> SelectCardMoves()
        {
            Dictionary<int, List<MoveVM>> results = new Dictionary<int, List<MoveVM>>();

            foreach (CardMove cardMove in _cardMoves)
            {
                // checks if the key is already used
                if (!results.ContainsKey(cardMove.CardID))
                {
                    // if not add the key and create a new list
                    results.Add(cardMove.CardID, new List<MoveVM>());
                }

                // each row has a cardID and a moveID if the CardID is already a key
                // then add the move at the cardID
                results[cardMove.CardID].Add(SelectMoveVMByMoveID(cardMove.MoveID));
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public Dictionary<int, List<MoveVM>> SelectCardMovesByCardName(string name)
        {
            Dictionary<int, List<MoveVM>> results = new Dictionary<int, List<MoveVM>>();

            foreach (Card card in _cards)
            {
                if (card.Name.Contains(name))
                {
                    results.Add(card.CardID, SelectMovesByCardID(card.CardID));
                }
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public Dictionary<int, List<string>> SelectCardAlternateArts()
        {
            Dictionary<int, List<string>> results = new Dictionary<int, List<string>>();

            foreach (CardAlternateArt altArts in _cardAlternateArts)
            {
                // checks if the key is already used
                if (!results.ContainsKey(altArts.CardID))
                {
                    // if not add the key and create a new list
                    results.Add(altArts.CardID, new List<string>());
                }

                // each row has a cardID and a moveID if the CardID is already a key
                // then add the move at the cardID
                results[altArts.CardID].Add(altArts.AlternateArtID);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public Dictionary<int, List<string>> SelectCardAlternateArtsByCardName(string name)
        {
            Dictionary<int, List<string>> results = new Dictionary<int, List<string>>();

            foreach (Card card in _cards)
            {
                if (card.Name.Contains(name))
                {
                    results.Add(card.CardID, SelectAlternateArtsByCardID(card.CardID));
                }
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public int DeleteCard(int cardID)
        {
            int count = 0;
            Card deletedCard = null;

            foreach (Card card in _cards)
            {
                if (card.CardID == cardID)
                {
                    deletedCard = card;
                    break;
                }
            }

            if (deletedCard != null)
            {
                _cards.Remove(deletedCard);
                count = 1;
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public int InsertCard(Card card)
        {
            int count = 0;

            // values used to represent the tables in the database
            // these are to make sure the foreign key is valid
            int[] validArtists = { 1, 2, 3, 4, 5 };
            string[] validAbilities = { "test ability 1", "test ability 2", "test ability 3", "test ability 4" };
            string[] validBoosters = { "test booster 1", "test booster 2", "test booster 3", "test booster 4" };
            string[] validRules = { "test pokemon rule 1", "test pokemon rule 2", "test pokemon rule 3", "test pokemon rule 4" };
            string[] validElements = { "test element 1", "test element 2", "test element 3", "test element 4" };

            if (!validArtists.Contains(card.ArtistID))
            {
                throw new Exception("Invalid ArtistID");
            }
            if (!validAbilities.Contains(card.AbilityID))
            {
                throw new Exception("Invalid AbilityID");
            }
            if (!validBoosters.Contains(card.BoosterID))
            {
                throw new Exception("Invalid BoosterID");
            }
            if (!validRules.Contains(card.PokemonRuleID))
            {
                throw new Exception("Invalid PokemonRuleID");
            }
            if (!validElements.Contains(card.ElementTypeID))
            {
                throw new Exception("Invalid ElementTypeID");
            }

            foreach (Card element in _cards)
            {
                if (element.BoosterID == card.BoosterID &&
                    element.BoosterNumber == card.BoosterNumber &&
                    element.Rarity == card.Rarity)
                {
                    throw new Exception("Unique Constraint (BoosterID, BoosterNumber, Rarity) already used.");
                }
            }

            _cards.Add(card);
            count = 1;

            return count;
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public int UpdateCard(Card card)
        {
            int count = 0;
            int index = -1;

            // values used to represent the tables in the database
            // these are to make sure the foreign key is valid
            int[] validArtists = { 1, 2, 3, 4, 5 };
            string[] validAbilities = { "test ability 1", "test ability 2", "test ability 3", "test ability 4" };
            string[] validBoosters = { "test booster 1", "test booster 2", "test booster 3", "test booster 4" };
            string[] validRules = { "test pokemon rule 1", "test pokemon rule 2", "test pokemon rule 3", "test pokemon rule 4" };
            string[] validElements = { "test element 1", "test element 2", "test element 3", "test element 4" };

            if (!validArtists.Contains(card.ArtistID))
            {
                throw new Exception("Invalid ArtistID");
            }
            if (!validAbilities.Contains(card.AbilityID))
            {
                throw new Exception("Invalid AbilityID");
            }
            if (!validBoosters.Contains(card.BoosterID))
            {
                throw new Exception("Invalid BoosterID");
            }
            if (!validRules.Contains(card.PokemonRuleID))
            {
                throw new Exception("Invalid PokemonRuleID");
            }
            if (!validElements.Contains(card.ElementTypeID))
            {
                throw new Exception("Invalid ElementTypeID");
            }

            for(int i = 0; i < _cards.Count; i++)
            {
                if (_cards[i].CardID != card.CardID &&
                    _cards[i].BoosterID == card.BoosterID &&
                    _cards[i].BoosterNumber == card.BoosterNumber &&
                    _cards[i].Rarity == card.Rarity)
                {
                    throw new Exception("Unique Constraint (BoosterID, BoosterNumber, Rarity) already used.");
                }

                if (_cards[i].CardID == card.CardID &&
                    _cards[i].BoosterID == card.BoosterID &&
                    _cards[i].BoosterNumber == card.BoosterNumber &&
                    _cards[i].Rarity == card.Rarity) 
                {
                    index = i;
                }
            }

            if (index != -1)
            {
                _cards[index] = card;
                count = 1;
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public int InsertCardMove(int cardID, int moveID)
        {
            int count = 0;
            int[] validMoveID = { 1, 2, 3 };

            if (!validMoveID.Contains(moveID))
            {
                throw new Exception("Move not in the table.");
            }

            if (SelectCardByCardID(cardID) == null)
            {
                throw new Exception("CardID not valid.");
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
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>    
        public int DeleteCardMove(int cardID, int moveID)
        {
            int count = 0;
            CardMove deletedMove = null;

            foreach (CardMove cardMove in _cardMoves)
            {
                if (cardMove.CardID == cardID &&
                    cardMove.MoveID == moveID)
                {
                    deletedMove = cardMove;
                    break;
                }
            }

            if (deletedMove != null)
            {
                _cardMoves.Remove(deletedMove);
                count = 1;
            }


            return count;
        }

        /// <summary>
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public int InsertCardAlternateArt(int cardID, string alternateArtID)
        {
            int count = 0;
            string[] validAlternateArts = { "test Alternate Art 1", "test Alternate Art 2", "test Alternate Art 3" };

            if (!validAlternateArts.Contains(alternateArtID))
            {
                throw new Exception("Alternate Art not in the table.");
            }

            if (SelectCardByCardID(cardID) == null)
            {
                throw new Exception("CardID not valid.");
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
        /// Implements from <see cref="ICardAccessor"/> used for testing
        /// </summary>
        public int DeleteCardAlternateArt(int cardID, string alternateArtID)
        {
            int count = 0;
            CardAlternateArt deletedAltArt = null;

            foreach (CardAlternateArt cardAlternateArt in _cardAlternateArts)
            {
                if (cardAlternateArt.CardID == cardID &&
                    cardAlternateArt.AlternateArtID == alternateArtID)
                {
                    deletedAltArt = cardAlternateArt;
                    break;
                }
            }

            if (deletedAltArt != null)
            {
                _cardAlternateArts.Remove(deletedAltArt);
                count = 1;
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
