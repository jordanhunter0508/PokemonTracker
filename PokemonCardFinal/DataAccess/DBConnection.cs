using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    internal static class DBConnection
    {
        /// <summary>
        /// Establishs a conncetion to the tcg_db database
        /// </summary>
        /// <returns>Returns a SqlConnection to the tcg_db database</returns>
        public static SqlConnection GetConnection()
        { 
            SqlConnection conn = null;

            string connectionString = "Data Source=localhost;Initial Catalog=tcg_db;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
