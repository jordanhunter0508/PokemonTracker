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
                                Stage = reader.GetString(16),
                                ImagePath = reader.IsDBNull(17) ? null : reader.GetString(17)
                            },
                            CollectionCardID = reader.GetInt32(18),
                            CollectionID = reader.GetInt32(19),
                            Quantity = reader.GetInt32(20),
                            Owned = reader.GetBoolean(21),
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

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/>. Access the database
        /// using sp_insert_collection_card
        /// </summary>
        public int InsertCollectionCard(CollectionCard collectionCard)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_insert_collection_card";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@PokemonCardID", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@CollectionID", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@Quantity", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@Owned", System.Data.SqlDbType.Bit);

            cmd.Parameters["@PokemonCardID"].Value = collectionCard.Card.CardID;
            cmd.Parameters["@CollectionID"].Value = collectionCard.CollectionID;
            cmd.Parameters["@Quantity"].Value = collectionCard.Quantity;
            cmd.Parameters["@Owned"].Value = collectionCard.Owned;

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
        /// using sp_insert_collection
        /// </summary>
        public int InsertCollection(Collection collection)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_insert_collection";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@CollectionTypeID", System.Data.SqlDbType.NVarChar, 25);
            cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar,150);

            cmd.Parameters["@UserID"].Value = collection.UserID;
            cmd.Parameters["@CollectionTypeID"].Value = collection.CollectionTypeID;
            cmd.Parameters["@Name"].Value = collection.Name;
            cmd.Parameters["@Description"].Value = collection.Description;

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
