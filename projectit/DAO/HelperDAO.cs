using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace projectit.DAO
{
    public class HelperDAO
    {
        public static void RunSQL(string sql, SqlParameter[] param)
        {

            using (SqlConnection connection = ConnectDB.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (param != null)
                        command.Parameters.AddRange(param);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public static DataTable RunSelect(string sql, SqlParameter[] param)
        {
            using (SqlConnection connection = ConnectDB.GetConnection())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    if (param != null)
                        adapter.SelectCommand.Parameters.AddRange(param);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    connection.Close();
                    return table;
                }
            }
        }

        public static DataTable RunProcSelect(string nomeProc, SqlParameter[] param)
        {
            using (SqlConnection connection = ConnectDB.GetConnection())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(nomeProc, connection))
                {
                    if (param != null)
                        adapter.SelectCommand.Parameters.AddRange(param);

                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    connection.Close();
                    return table;
                }
            }
        }

        public static void RunProc(string nomeProc, SqlParameter[] param)
        {
            using (SqlConnection connection = ConnectDB.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(nomeProc, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (param != null)
                        command.Parameters.AddRange(param);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
