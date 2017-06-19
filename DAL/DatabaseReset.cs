using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DAL
{
    public static class DatabaseReset
    {
        public static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["testDB_MovieTheaterShowings_connection"].ConnectionString;
        public static void resetDatabase() 
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("truncate_all", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery(); //Run the query.
                    }
                }
            }
            catch(SqlException e){}
        }
    }
}
