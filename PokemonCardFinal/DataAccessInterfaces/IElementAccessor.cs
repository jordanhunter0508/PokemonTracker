using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace DataAccessInterfaces
{
    public interface IElementAccessor
    {
        /// <summary>
        /// Requests all fields from the Element Type table to create an ElementType.
        /// </summary>
        /// <param name="elementTypeID">Used to search the database for an element</param>
        /// <returns>Returns an ElementType of the specified elementTypeID</returns>
        public ElementType SelectElementTypeByElementTypeID(string elementTypeID);

        /// <summary>
        /// Requests all data from the Element Type table to
        /// create an ElementType List.
        /// </summary>
        /// <returns>Returns a List of all ElementTypes in the database</returns>
        public List<ElementType> SelectElementTypes();

        /// <summary>
        /// Inserts the parameters into the stored procedure to try
        /// and create a new record for an ElementType
        /// </summary>
        /// <param name="elementTypeID">Element Type ID of the element wanting to create</param>
        /// <param name="description">Description of the element wanting to create</param>
        /// <returns>Returns 1 if the record was created</returns>
        public int InsertElementType(string elementTypeID, string description);

        /// <summary>
        /// Updates the description of a specified element type at elementTypeID. <br/>
        /// Changes description in the table to the description parameter.
        /// </summary>
        /// <param name="elementTypeID">Used to search the table Element Type for a match</param>
        /// <param name="description">Used to change the description of the element at elementTypeID</param>
        /// <returns>Returns 1 if the record at elementTypeId updated the description successfully</returns>
        public int UpdateElementType(string elementTypeID,string description);

        /// <summary>
        /// Deletes the record at elementTypeID
        /// </summary>
        /// <param name="elementTypeID">Used to search the table Element Type for a match</param>
        /// <returns>Returns 1 if the record at elementTypeID was deleted successfully</returns>
        public int DeleteElementType(string elementTypeID);
 
    }
}
