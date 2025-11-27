using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface IElementManager
    {
        /// <summary>
        /// Passes parameters to <see href="SelectElementTypeByElementTypeID(string)"/><br/>
        /// then returns the results.
        /// </summary>
        /// <param name="elementTypeID">Used to search the database for an element</param>
        /// <returns>Returns an ElementType of the specified elementTypeID</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public ElementType GetElementTypeByElementTypeID(string elementTypeID);

        /// <summary>
        /// Calls the <see href="SelectElementTypes()"/> method to get<br/>
        /// a list of all ElementTypes from the database.
        /// </summary>
        /// <returns>Returns a List of all ElementTypes in the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<ElementType> GetElementTypes();

        /// <summary>
        /// Calls the <see href="GetElementTypes()"/> method to get<br/>
        /// a list of all ElementTypes from the database. Then returns a list of the IDs.
        /// </summary>
        /// <returns>Returns a List of all ElementTypeIDs in the database</returns>
        /// <exception cref="ApplicationException">Throws if there is an error retrieving data</exception>
        public List<string> GetElementTypeIDs();

        /// <summary>
        /// Passes parameters to <see href="InsertElementType(string,string)"/><br/>
        /// Then returns true if the record was created successfully
        /// </summary>
        /// <param name="elementTypeID">Element Type ID of the element wanting to create</param>
        /// <param name="description">Description of the element wanting to create</param>
        /// <returns>Returns true of the ElementType was created, false if not</returns>
        /// <exception cref="ApplicationException">Throws if the elemntTypeID is alredy used.</exception>
        public bool AddElementType(string elementTypeID, string description);

        /// <summary>
        /// Passes parameters to <see href="UpdateElementType(string,string)"/><br/>
        /// Then returns true if the record was updated successfully
        /// </summary>
        /// <param name="elementTypeID">Used to find the ElementType</param>
        /// <param name="description">Used to update the Description field</param>
        /// <returns>Returns true if the ElementType was updated successfully</returns>
        /// <exception cref="ApplicationException">Throws if there is an error connecting to the server</exception>
        public bool EditElementDescritpionByElementTypeID(string elementTypeID, string description);

        /// <summary>
        /// Passes parameters to <see href="DeleteElementType(string)"/><br/>
        /// Then returns true if the record was deleted successfully
        /// </summary>
        /// <param name="elementTypeID">Used to find the ElementType</param>
        /// <returns>Returns true if the ElementType was deleted successfully</returns>
        /// <exception cref="ApplicationException">Throws if the element is attached to a move</exception>
        public bool DeleteElementTypeByElementTypeID(string elementTypeID);

        /// <summary>
        /// Makes sure the first leter of a element type is capital
        /// then puts them in alphabetical order.
        /// </summary>
        /// <param name="elementTypes">The IEnumerable that is being sorted</param>
        /// <returns>Returns an IEnumberable of type ElementType that is formated for dispaly.</returns>
        public IEnumerable<ElementType> FormatElemetTypes(IEnumerable<ElementType> elementTypes);
    }
}
