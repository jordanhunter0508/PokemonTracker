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
    public class CardComponentManager : ICardComponentManager
    {
        private ICardComponentAccessor _componentAccessor;

        /// <summary>
        /// General CardComponentManager created for the presentaion layer
        /// </summary>
        public CardComponentManager()
        {
            _componentAccessor = new CardComponentAccessor();
        }

        /// <summary>
        /// Used for testing to pass in fake data
        /// </summary>
        /// <param name="componentAccessor">Set the ICardComponentManager in the CardComponentManager</param>
        public CardComponentManager(ICardComponentAccessor componentAccessor)
        {
            _componentAccessor = componentAccessor;
        }

        /// <summary>
        /// Implements from <see cref="ICardComponentManager"/>
        /// </summary>
        public List<MoveVM> GetMovesByCardID(int cardID)
        {
            List<MoveVM> results = new List<MoveVM>();

            try
            {
                results = _componentAccessor.SelectMovesByCardID(cardID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get moves from a cardID.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardComponentManager"/>
        /// </summary>
        public List<string> GetAlternateArtsByCardID(int cardID)
        {
            List<string> results = new List<string>();

            try
            {
                results = _componentAccessor.SelectAlternateArtsByCardID(cardID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to get alternate arts from a cardID.", ex);
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="ICardComponentManager"/>
        /// </summary>
        public bool AddCardMove(int cardID, int moveID)
        {
            bool isAdded = false;

            try
            {
                isAdded = (1 == _componentAccessor.InsertCardMove(cardID, moveID));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to add a card's move.", ex);
            }

            return isAdded;
        }

        /// <summary>
        /// Implements from <see cref="ICardComponentManager"/>
        /// </summary>
        public bool DeleteCardMoves(int cardID)
        {
            bool isDeleted = false;

            try
            {
                isDeleted = (1 <= _componentAccessor.DeleteCardMoves(cardID));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to delete a card's moves", ex);
            }

            return isDeleted;
        }

        /// <summary>
        /// Implements from <see cref="ICardComponentManager"/>
        /// </summary>
        public bool AddCardAlternateArt(int cardID, string alternateArtID)
        {
            bool isAdded = false;

            if (String.IsNullOrWhiteSpace(alternateArtID))
            {
                throw new ArgumentNullException("Failed to add a card's alternate art. AlternateArtID was null.");
            }

            try
            {
                isAdded = (1 == _componentAccessor.InsertCardAlternateArt(cardID, alternateArtID));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to add a card's alternate art.", ex);
            }

            return isAdded;
        }

        /// <summary>
        /// Implements from <see cref="ICardComponentManager"/>
        /// </summary>
        public bool DeleteCardAlternateArts(int cardID)
        {
            bool isDeleted = false;

            try
            {
                isDeleted = (1 <= _componentAccessor.DeleteCardAlternateArts(cardID));
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to delete a card's alternate arts.", ex);
            }

            return isDeleted;
        }

    }
}
