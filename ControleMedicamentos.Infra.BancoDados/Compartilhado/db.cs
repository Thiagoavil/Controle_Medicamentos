using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Compartilhado
{
    public class db
    {
        private const string enderecoBanco =
             @"Data Source=(LocalDB)\MSSqlLocalDB;
                 Initial Catalog=ControleMedicamentos.Projeto.SqlServer;
                 Integrated Security=True;
                 Connect Timeout=30;
                 Encrypt=False;
                 TrustServerCertificate=False;   
                 ApplicationIntent=ReadWrite;
                 MultiSubnetFailover=False;
                 Pooling=False";

        public static void ComandoSql(string sql)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comando = new SqlCommand(sql, conexaoComBanco);

            conexaoComBanco.Open();
            comando.ExecuteNonQuery();
            conexaoComBanco.Close();
        }


    }
}
