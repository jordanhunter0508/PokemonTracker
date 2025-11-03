using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class ElementAccessor : IElementAccessor
    {

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/>. Access the database
        /// using sp_select_element_by_elementtypeid
        /// </summary>
        public ElementType SelectElementTypeByElementTypeID(string elementTypeID)
        {
            ElementType resultElement = null;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_element_by_elementtypeid";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@ElementTypeID", System.Data.SqlDbType.NVarChar, 15);
            cmd.Parameters["@ElementTypeID"].Value = elementTypeID;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    resultElement = new ElementType()
                    {
                        ElementTypeID = reader.GetString(0),
                        Description = reader.GetString(1)
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return resultElement;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/>. Access the database
        /// using sp_select_elements
        /// </summary>
        public List<ElementType> SelectElementTypes()
        {
            List<ElementType> results = new List<ElementType>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_elements";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        results.Add(new ElementType()
                        {
                            ElementTypeID = reader.GetString(0),
                            Description = reader.GetString(1)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return results;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/>. Access the database
        /// using sp_insert_element_into_element_type
        /// </summary>
        public int InsertElementType(string elementTypeID, string description)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_insert_element_into_element_type";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@ElementTypeID", System.Data.SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 100);
            cmd.Parameters["@ElementTypeID"].Value = elementTypeID;
            cmd.Parameters["@Description"].Value = description;

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/>. Access the database
        /// using sp_update_element_description_by_elementtypeid
        /// </summary>
        public int UpdateElementTypeByElementTypeID(string elementTypeID, string description)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_update_element_description_by_elementtypeid";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@ElementTypeID", System.Data.SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 100);
            cmd.Parameters["@ElementTypeID"].Value = elementTypeID;
            cmd.Parameters["@Description"].Value = description;

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/>. Access the database
        /// using sp_delete_element_by_elementtypeid
        /// </summary>
        public int DeleteElementType(string elementTypeID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_delete_element_by_elementtypeid";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@ElementTypeID", System.Data.SqlDbType.NVarChar, 15);
            cmd.Parameters["@ElementTypeID"].Value = elementTypeID;

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();

            }

            return count;
        }

    }
}
