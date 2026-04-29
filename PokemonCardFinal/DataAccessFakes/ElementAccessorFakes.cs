using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class ElementAccessorFakes : IElementAccessor
    {
        List<ElementType> _elements;

        /// <summary>
        /// Fills the _elements list with fake data
        /// </summary>
        public ElementAccessorFakes()
        {
            _elements = new List<ElementType>();
            _elements.Add(new ElementType()
            {
                ElementTypeID = "testElement1",
                Description = "Description test 1.",
                Active = true,
            });
            _elements.Add(new ElementType()
            {
                ElementTypeID = "testElement2",
                Description = "Description test 2.",
                Active = true,
            });
            _elements.Add(new ElementType()
            {
                ElementTypeID = "testElement3",
                Description = "Description test 3.",
                Active = false,
            });
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public ElementType SelectElementTypeByElementTypeID(string elementTypeID)
        {
            ElementType resultElement = null;

            foreach (ElementType element in _elements)
            {
                if (element.ElementTypeID == elementTypeID)
                {
                    resultElement = element;
                    break;
                }
            }
            if (resultElement == null)
            {
                throw new ArgumentException("Element Type ID could not be found.");
            }
            return resultElement;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public List<ElementType> SelectElementTypes()
        {
            List<ElementType> results = null;
            results = _elements;
            return results;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public int InsertElementType(string elementTypeID, string description)
        {
            int count = 0;

            ElementType newElement = new ElementType()
            {
                ElementTypeID = elementTypeID,
                Description = description,
            };
            _elements.Add(newElement);

            foreach (ElementType element in _elements)
            {
                if (newElement.ElementTypeID == element.ElementTypeID)
                {
                    count++;
                }
            }

            if (count > 1)
            {
                throw new Exception();
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public int UpdateElementType(string elementTypeID, string description)
        {
            int count = 0;
            ElementType updatedElement = null;

            // Uses a foreach rather than calling the SelectElementTypeByElementTypeID method
            // because it will throw an error if the id is not found
            // the stored procedure just returns a 0. Allowing the logic layer method
            // to be false
            foreach (ElementType element in _elements)
            {
                if (element.ElementTypeID == elementTypeID)
                {
                    updatedElement = element;
                    break;
                }
            }
            if (updatedElement != null)
            {
                updatedElement.Description = description;

                foreach (ElementType element in _elements)
                {
                    if (updatedElement.Description == element.Description)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public int DeleteElementType(string elementTypeID)
        {
            int count = 0;

            ElementType deletedElement = null;

            // Uses a foreach rather than calling the SelectElementTypeByElementTypeID method
            // because it will throw an error if the id is not found
            // the stored procedure just returns a 0. Allowing the logic layer method
            // to be false
            foreach (ElementType element in _elements)
            {
                if (element.ElementTypeID == elementTypeID)
                {
                    deletedElement = element;
                    break;
                }
            }
            if (deletedElement != null)
            {
                _elements.Remove(deletedElement);
                count++;
            }
            return count;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public int ActivateElementType(string elementTypeID, bool active)
        {
            int count = 0;
            ElementType element = _elements.Find(e => string.Equals(e.ElementTypeID, elementTypeID, StringComparison.OrdinalIgnoreCase));

            if (element != null)
            {
                element.Active = active;
                count++;
            }

            return count;
        }
    }
}
