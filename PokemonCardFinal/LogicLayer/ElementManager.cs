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
    public class ElementManager : IElementManager
    {
        IElementAccessor _elementAccessor;

        /// <summary>
        /// General ElementManager created for the presentaion layer
        /// </summary>
        public ElementManager()
        {
            _elementAccessor = new ElementAccessor();
        }

        /// <summary>
        /// Used for testing to pass in fake data
        /// </summary>
        /// <param name="elementAccessor">Set the IElementAccessor in the ElementManager</param>
        public ElementManager(IElementAccessor elementAccessor)
        {
            _elementAccessor = elementAccessor;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/>
        /// </summary>
        public ElementType GetElementTypeByElementTypeID(string elementTypeID)
        {
            ElementType resultElement = null;

            try
            {
                resultElement = _elementAccessor.SelectElementTypeByElementTypeID(elementTypeID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve element.");
            }

            return resultElement;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/>
        /// </summary>
        public List<ElementType> GetElementTypes()
        {
            List<ElementType> results = null;

            try
            {
                results = _elementAccessor.SelectElementTypes();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve a list of element types.");
            }
            return results;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/>
        /// </summary>
        public bool CreateElementType(string elementTypeID, string description)
        {
            bool result = false;

            try
            {
                result = (1 == _elementAccessor.InsertElementType(elementTypeID, description));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create new record of element type.\nCheck if element name is already used.");
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/>
        /// </summary>
        public bool UpdateElementDescritpionByElementTypeID(string elementTypeID, string description)
        {
            bool result = false;

            try
            {
                result = (1 == _elementAccessor.UpdateElementTypeByElementTypeID(elementTypeID, description));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update element.");
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/>
        /// </summary>
        public bool DeleteElementTypeByElementTypeID(string elementTypeID)
        {
            bool result;
            try
            {
                result = (1 == _elementAccessor.DeleteElementTypeByElementTypeID(elementTypeID));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update element. May be connected to a move.");
            }
            return result;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/>
        /// </summary>
        public IEnumerable<ElementType> FormatElemetTypes(IEnumerable<ElementType> elementTypes) 
        {
            foreach (ElementType element in elementTypes)
            {
                element.ElementTypeID = char.ToUpper(element.ElementTypeID[0]) + element.ElementTypeID.Substring(1);
            }
            elementTypes = elementTypes.OrderBy(element => element.ElementTypeID);
            return elementTypes;
        }
    }
}
