using System.ComponentModel.Design;
using DataAccess;
using DataAccessFakes;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace LogicLayerTest;

[TestClass]
public class CardManagerTest
{
    ICardManager _cardManager;

    [TestInitialize]
    public void TestSetup()
    {
        _cardManager = new CardManager(new CardAccessorFakes(), new CardComponentAccessorFakes());
    }

    [TestMethod]
    public void TestGetAllCards()
    {
        // arrange
        const int expectedCount = 5;
        List<Card> actual;

        // act
        actual = _cardManager.GetAllCards();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestGetCardVMWithValidCardID()
    {
        // arrange
        const int cardID = 1;
        const string expectedName = "test 1";
        const int artistCount = 2;
        const int moveCount = 2;
        CardVM actual;

        // act
        actual = _cardManager.GetCardVM(cardID);

        // assert
        Assert.AreEqual(expectedName, actual.Name);
        Assert.AreEqual(artistCount, actual.AlternateArts.Count);
        Assert.AreEqual(moveCount, actual.Moves.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestGetCardVMThrowsApplicationExceptionWihInvalidID()
    {
        // arrange
        const int cardID = 999;
        CardVM actual;

        // act
        actual = _cardManager.GetCardVM(cardID);

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestAddCardWithReturnsNewCardIDValidInput()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 5,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        const int expected = 5;
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionWithInvalidArtistID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 999,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionWithInvalidAbilityID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "failed",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionWithInvalidBoosterID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "failed",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionWithInvalidRuleID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "failed",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionWithInvalidElementID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "failed",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionWithUniqueKey()
    {
        // BoosterId, BoosterNumber and Rarity are already used.

        // arrange
        Card card = new Card()
        {
            CardID = 4,
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
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullAbilityID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = null,
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullBoosterID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = null,
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullRuleID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = null,
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullElementID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = null,
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullName()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = null,
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullCardType()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = null,
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullRarity()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = null,
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullWeaknessType()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = null,
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullResistanceType()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = null,
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionNullStage()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 4,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = null
        };
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAddCardThrowsArgumentNullExceptionNullCard()
    {
        // arrange
        Card card = null;
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestAddCardThrowsApplicationExceptionBlankCard()
    {
        // arrange
        Card card = new Card();
        int actual = 1;

        // act
        actual = _cardManager.AddCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    public void TestEditCardWithReturnsTrueValidInput()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        const bool expected = true;
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionWithInvalidArtistID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 999,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionWithInvalidAbilityID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "failed",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothing
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionWithInvalidBoosterID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "failed",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionWithInvalidRuleID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "failed",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionWithInvalidElementID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "failed",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionWithUniqueKey()
    {
        // BoosterId, BoosterNumber and Rarity are already used.

        // arrange
        Card card = new Card()
        {
            CardID = 2,
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
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }


    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullAbilityID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = null,
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullBoosterID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = null,
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullRuleID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = null,
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullElementID()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = null,
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullName()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = null,
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullCardType()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = null,
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullRarity()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = null,
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullWeaknessType()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = null,
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullResistanceType()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = null,
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = "test stage"
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullStage()
    {
        // arrange
        Card card = new Card()
        {
            CardID = 2,
            ArtistID = 2,
            AbilityID = "test ability 1",
            BoosterID = "test booster 3",
            PokemonRuleID = "test pokemon rule 3",
            ElementTypeID = "test element 2",
            Name = "test 1",
            BoosterNumber = 3,
            CardType = "test type 3",
            Rarity = "test rarity 3",
            WeaknessType = "weakness 1",
            ResistanceType = "resistance 1",
            WeaknessValue = 1,
            ResistanceValue = 1,
            RetreatCost = 1,
            Health = 100,
            Stage = null
        };
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionNullCard()
    {
        // arrange
        Card card = null;
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    public void TestEditCardThrowsApplicationExceptionBlankCard()
    {
        // arrange
        Card card = new Card();
        bool actual = false;

        // act
        actual = _cardManager.EditCard(card);

        // asseret
        // do nothig
    }

    [TestMethod]
    public void TestDeleteCardReturnsTrueWithValidID()
    {
        // arrange
        const int cardID = 1;
        const bool expectedResult = true;
        bool actualResult = false;

        // act
        actualResult = _cardManager.DeleteCard(cardID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void TestDeleteCardReturnsFalseWithInvalidID()
    {
        // arrange
        const int cardID = 999;
        const bool expectedResult = false;
        bool actualResult = true;

        // act
        actualResult = _cardManager.DeleteCard(cardID);

        // assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestApplyFiltersThrowsArgumentNullExceptionWithNullCardList()
    {
        // arrange
        List<Card> cards = null;
        FilterOption filterOption = new FilterOption() { CardName = "1" };
        List<Card> actual;

        // act
        actual = _cardManager.ApplyFilters(cards, filterOption).ToList();

        // assert
        // do nothing
    }

    [TestMethod]
    public void TestApplyFiltersWithNullFilterOptionReturnsAllCards()
    {
        // arrange
        const int expectedCount = 5;
        List<Card> cards = _cardManager.GetAllCards();
        FilterOption filterOption = null;
        List<Card> actual;

        // act
        actual = _cardManager.ApplyFilters(cards, filterOption).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestApplyFiltersByCardNameWithValidInput()
    {
        // arrange
        const string name = "test 1";
        const int expectedCount = 2;
        List<Card> cards = _cardManager.GetAllCards();
        FilterOption filterOption = new FilterOption() { CardName = name };
        List<Card> actual;

        // act
        actual = _cardManager.ApplyFilters(cards, filterOption).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestApplyFiltersByCardNameReturnsBlankListWithInvalidName()
    {
        // arrange
        const string name = "fails";
        const int expectedCount = 0;
        List<Card> cards = _cardManager.GetAllCards();
        FilterOption filterOption = new FilterOption() { CardName = name };
        List<Card> actual;

        // act
        actual = _cardManager.ApplyFilters(cards, filterOption).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestApplyFiltersByRarityWithValidInput()
    {
        // arrange
        const string rarity = "test rarity 1";
        const int expectedCount = 3;
        List<Card> cards = _cardManager.GetAllCards();
        FilterOption filterOption = new FilterOption() { Rarity = rarity };
        List<Card> actual;

        // act
        actual = _cardManager.ApplyFilters(cards, filterOption).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestApplyFiltersByRarityReturnsBlankListWithInvalidRarity()
    {
        // arrange
        const string rarity = "fails";
        const int expectedCount = 0;
        List<Card> cards = _cardManager.GetAllCards();
        FilterOption filterOption = new FilterOption() { Rarity = rarity };
        List<Card> actual;

        // act
        actual = _cardManager.ApplyFilters(cards, filterOption).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestApplyFiltersByBoosterIDWithValidInput()
    {
        // arrange
        const string boosterID = "test booster 1";
        const int expectedCount = 3;
        List<Card> cards = _cardManager.GetAllCards();
        FilterOption filterOption = new FilterOption() { BoosterID = boosterID };
        List<Card> actual;

        // act
        actual = _cardManager.ApplyFilters(cards, filterOption).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestApplyFiltersByBoosterIDReturnsBlankListWithInvalidBoosterID()
    {
        // arrange
        const string boosterID = "fails";
        const int expectedCount = 0;
        List<Card> cards = _cardManager.GetAllCards();
        FilterOption filterOption = new FilterOption() { BoosterID = boosterID };
        List<Card> actual;

        // act
        actual = _cardManager.ApplyFilters(cards, filterOption).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestApplyFiltersByCardTypeWithValidInput()
    {
        // arrange
        const string cardType = "test type 1";
        const int expectedCount = 3;
        List<Card> cards = _cardManager.GetAllCards();
        FilterOption filterOption = new FilterOption() { CardType = cardType };
        List<Card> actual;

        // act
        actual = _cardManager.ApplyFilters(cards, filterOption).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestApplyFiltersByCardTypeReturnsBlankListWithInvalidCardType()
    {
        // arrange
        const string cardType = "fails";
        const int expectedCount = 0;
        List<Card> cards = _cardManager.GetAllCards();
        FilterOption filterOption = new FilterOption() { CardType = cardType };
        List<Card> actual;

        // act
        actual = _cardManager.ApplyFilters(cards, filterOption).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestApplyFiltersByElementTypeIDWithValidInput()
    {
        // arrange
        const string elementTypeID = "test element 1";
        const int expectedCount = 2;
        List<Card> cards = _cardManager.GetAllCards();
        FilterOption filterOption = new FilterOption() { ElementTypeID = elementTypeID };
        List<Card> actual;

        // act
        actual = _cardManager.ApplyFilters(cards, filterOption).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestApplyFiltersByElementTypeIDReturnsBlankListWithInvalidElement()
    {
        // arrange
        const string elementTypeID = "fails";
        const int expectedCount = 0;
        List<Card> cards = _cardManager.GetAllCards();
        FilterOption filterOption = new FilterOption() { ElementTypeID = elementTypeID };
        List<Card> actual;

        // act
        actual = _cardManager.ApplyFilters(cards, filterOption).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestApplyFiltersByArtistIDWithValidInput()
    {
        // arrange
        const int artistID = 1;
        const int expectedCount = 3;
        List<Card> cards = _cardManager.GetAllCards();
        FilterOption filterOption = new FilterOption() { ArtistID = artistID };
        List<Card> actual;

        // act
        actual = _cardManager.ApplyFilters(cards, filterOption).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestApplyFiltersByArtistIDReturnsBlankListWithInvalidArtistID()
    {
        // arrange
        const int artistID = 0;
        const int expectedCount = 5;
        List<Card> cards = _cardManager.GetAllCards();
        FilterOption filterOption = new FilterOption() { ArtistID = artistID };
        List<Card> actual;

        // act
        actual = _cardManager.ApplyFilters(cards, filterOption).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
    }

    [TestMethod]
    public void TestApplyFiltersMultipleOptionsReturnsExpectedCards()
    {
        // arrange
        const int expectedCount = 1;
        List<Card> cards = _cardManager.GetAllCards();

        // This filter combines BoosterID = "test booster 1" and ElementTypeID = "test element 2".
        // In our mock data (CardAccessorFakes), Card 5 has these properties.
        FilterOption filterOption = new FilterOption()
        {
            BoosterID = "test booster 1",
            ElementTypeID = "test element 2"
        };
        List<Card> actual;

        // act
        actual = _cardManager.ApplyFilters(cards, filterOption).ToList();

        // assert
        Assert.AreEqual(expectedCount, actual.Count);
        Assert.AreEqual("test 4", actual[0].Name);
    }

    [TestMethod]
    public void TestGetCardsPaginatedReturnsListWithValidParameters()
    {
        // arrange
        const int expectedCount = 5;
        const int pageSize = 5;
        const int pageNumber = 1;
        FilterOption filterOption = new FilterOption();
        PaginatedResult<Card> actual;

        // act
        actual = _cardManager.GetCardsPaginated(filterOption, pageNumber, pageSize);

        // assert
        Assert.AreEqual(expectedCount, actual.Items.Count);
    }
    
    [TestMethod]
    public void TestDeactivateCardWithValidIDReturnsTrue()
    {
        // arrange
        const int cardID = 1;
        const bool active = false;
        bool expected = true;
        bool actual = false;

        // act
        actual = _cardManager.ActivateCard(cardID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }
    
    [TestMethod]
    public void TestDeactivateCardWithInvalidIDReturnsFalse()
    {
        // arrange
        const int cardID = 999;
        const bool active = false;
        bool expected = false;
        bool actual = true;

        // act
        actual = _cardManager.ActivateCard(cardID, active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestReactivateCardWithValidIDReturnsTrue()
    {
        // arrange
        const int cardID = 2;
        const bool active = true;
        bool expected = true;
        bool actual = false;

        // act
        actual = _cardManager.ActivateCard(cardID,active);

        // assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void TestReactivateCardWithInvalidIDReturnsFalse()
    {
        // arrange
        const int cardID = 999;
        const bool active = true;
        bool expected = false;
        bool actual = true;

        // act
        actual = _cardManager.ActivateCard(cardID,active);

        // assert
        Assert.AreEqual(expected, actual);
    }
}
