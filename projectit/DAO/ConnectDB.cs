using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace projectit.DAO   
{
    public class ConnectDB
    {
        public static SqlConnection GetConnection() {
            string strCon = "Data Source=LOCALHOST; DataBase=projectIt; integrated security=true";
            SqlConnection conexao = new SqlConnection(strCon);
            conexao.Open();

            return conexao;
        }

    }
}
