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
    public class MoveAccessor : IMoveAccessor
    {
        public Move SelectMoveByMoveID(string moveID)
        {
            Move resultMove = null;

            SqlConnection conn = DBConnection.GetConnection();
            string cmdText = "sp_select_move_by_moveid";
            SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@MoveID",System.Data.SqlDbType.NVarChar,30);
            cmd.Parameters["@MoveID"].Value = moveID;

            try
            {
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    resultMove = new Move()
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

            return resultMove;
        }
    }
}
