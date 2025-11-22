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
    public class MoveAccessor : IMoveAccessor
    {

        /// <summary>
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_select_move_by_moveid
        /// </summary>
        public Move SelectMoveByMoveID(string moveID)
        {
            Move resultMoves = null;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_move_by_moveid";
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@MoveID", System.Data.SqlDbType.NVarChar, 30);
            cmd.Parameters["@MoveID"].Value = moveID;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    resultMoves = new Move()
                    {
                        MoveID = reader.GetString(0),
                        Damage = reader.GetInt32(1),
                        Description = reader.GetString(2)
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

            return resultMoves;
        }

        /// <summary>
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_select_move_cost_by_moveid
        /// </summary>
        public List<MoveCost> SelectMoveCostsByMoveID(string moveID)
        {
            List<MoveCost> resultMoveCosts = new List<MoveCost>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_move_cost_by_moveid";
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@MoveID", System.Data.SqlDbType.NVarChar, 30);
            cmd.Parameters["@MoveID"].Value = moveID;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        resultMoveCosts.Add(new MoveCost()
                        {
                            MoveID = reader.GetString(0),
                            ElementType = reader.GetString(1),
                            Quantity = reader.GetInt32(2)
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

            return resultMoveCosts;
        }

        /// <summary>
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_select_moves_with_move_cost
        /// </summary>
        public List<MoveVM> SelectMoveVMsWithMoveCost()
        {
            Dictionary<string, MoveVM> results = new Dictionary<string, MoveVM>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_moves_with_move_cost";
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
                        string moveID = reader.GetString(0);

                        // If this is the first time seeing this MoveID, add it
                        if (!results.ContainsKey(moveID))
                        {
                            results.Add(moveID,new MoveVM()
                            {
                                MoveID = moveID,
                                Damage = reader.GetInt32(1),
                                Description = reader.GetString(2),
                                Costs = new List<MoveCost>()
                            });
                        }

                        // Add MoveCost (each row has a cost)
                        results[moveID].Costs.Add(new MoveCost
                        {
                            MoveID = moveID,
                            ElementType = reader.GetString(3),
                            Quantity = reader.GetInt32(4)
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
            return results.Values.ToList();
        }

        /// <summary>
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_select_moves_without_move_cost
        /// </summary>
        public List<Move> SelectMovesWithoutMoveCost()
        {
            List<Move> results = new List<Move>();

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_moves_without_move_cost";
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
                        results.Add(new Move()
                        {
                            MoveID = reader.GetString(0),
                            Damage = reader.GetInt32(1),
                            Description = reader.GetString(2)
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
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_insert_move
        /// </summary>
        public int InsertMove(Move move)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_insert_move";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@MoveID", System.Data.SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@Damage", System.Data.SqlDbType.Int);
            cmd.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar, 200);

            cmd.Parameters["@MoveID"].Value = move.MoveID;
            cmd.Parameters["@Damage"].Value = move.Damage;
            cmd.Parameters["@Description"].Value = move.Description;

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
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_insert_move_cost
        /// </summary>
        public int InsertMoveCost(MoveCost cost)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_insert_move_cost";
            SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@MoveID", System.Data.SqlDbType.NVarChar, 30);
            cmd.Parameters.Add("@ElementTypeID", System.Data.SqlDbType.NVarChar, 15);
            cmd.Parameters.Add("@Quantity", System.Data.SqlDbType.Int);

            cmd.Parameters["@MoveID"].Value = cost.MoveID;
            cmd.Parameters["@ElementTypeID"].Value = cost.ElementType;
            cmd.Parameters["@Quantity"].Value = cost.Quantity;

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
        /// Implements from <see cref="IMoveAccessor"/>. Access the database
        /// using sp_delete_move
        /// </summary>
        public int DeleteMove(string moveID)
        {
            int count = 0;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_delete_move";
            SqlCommand cmd = new SqlCommand(cmdText,conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@MoveID", System.Data.SqlDbType.NVarChar, 30);
            cmd.Parameters["@MoveID"].Value = moveID;

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
