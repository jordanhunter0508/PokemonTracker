using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class CollectionAccessor : ICollectionAccessor
    {

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/>. Access the database
        /// using sp_select_collection_cards_by_collection_id
        /// </summary>
        public List<CollectionCard> SelectCollectionCardsByCollectionID(int collectionID)
        {
            List<CollectionCard> results = new List<CollectionCard>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_collection_cards_by_collection_id";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@CollectionID", System.Data.SqlDbType.Int);
            cmd.Parameters["@CollectionID"].Value = collectionID;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        results.Add(new CollectionCard()
                        {
                            Card = new Card()
                            {
                                CardID = reader.GetInt32(0),
                                ArtistID = reader.GetInt32(1),
                                AbilityID = reader.GetString(2),
                                BoosterID = reader.GetString(3),
                                PokemonRuleID = reader.GetString(4),
                                ElementTypeID = reader.GetString(5),
                                Name = reader.GetString(6),
                                BoosterNumber = reader.GetInt32(7),
                                CardType = reader.GetString(8),
                                Rarity = reader.GetString(9),
                                WeaknessType = reader.GetString(10),
                                ResistanceType = reader.GetString(11),
                                WeaknessValue = reader.GetInt32(12),
                                ResistanceValue = reader.GetInt32(13),
                                RetreatCost = reader.GetInt32(14),
                                Health = reader.GetInt32(15),
                                Stage = reader.GetString(16)
                            },
                            CollectionCardID = reader.GetInt32(17),
                            CollectionID = reader.GetInt32(18),
                            Quantity = reader.GetInt32(19),
                            Owned = reader.GetBoolean(20),
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
        /// using sp_select_collection_elements_by_collection_id
        /// </summary>
        public List<string> SelectCollectionElementsByCollectionID(int collectionID)
        {
            List<string> results = new List<string>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_collection_elements_by_collection_id";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@CollectionID", System.Data.SqlDbType.Int);
            cmd.Parameters["@CollectionID"].Value = collectionID;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        results.Add(reader.GetString(0));
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
        /// using sp_select_max_size_by_collection_type_id
        /// </summary>
        public int SelectCollectionTypeMaxSize(string collectionTypeID)
        {
            int count = -1;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_max_size_by_collection_type_id";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@CollectionTypeID", System.Data.SqlDbType.NVarChar, 25);

            cmd.Parameters["@CollectionTypeID"].Value = collectionTypeID;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    count = reader.GetInt32(0);
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

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/>. Access the database
        /// using sp_select_collection_by_collection_id
        /// </summary>
        public Collection SelectCollectionByCollectionID(int collectionID)
        {
            Collection result = null;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_collection_by_collection_id";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@CollectionID", System.Data.SqlDbType.Int);

            cmd.Parameters["@CollectionID"].Value = collectionID;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    result = new Collection()
                    {
                        CollectionID = reader.GetInt32(0),
                        UserID = reader.GetInt32(1),
                        CollectionTypeID = reader.GetString(2),
                        Name = reader.GetString(3),
                        Description = reader.GetString(4)
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
            return result;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/>. Access the database
        /// using sp_delete_collection
        /// </summary>
        public int DeleteCollection(int collectionID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_delete_collection";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@CollectionID", System.Data.SqlDbType.Int);

            cmd.Parameters["@CollectionID"].Value = collectionID;

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
        /// using sp_delete_collection_card
        /// </summary>
        public int DeleteCollectionCard(int collectionCardID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_delete_collection_card";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@CollectionCardID", System.Data.SqlDbType.Int);

            cmd.Parameters["@CollectionCardID"].Value = collectionCardID;

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
