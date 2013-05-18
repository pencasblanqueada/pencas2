using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pencas.Models
{
    public class DAOToken
    {
        private string connectionString = "Data Source=GABRIEL-PC;Initial Catalog=Pencas;Integrated Security=True; MultipleActiveResultSets=True;";

        public void GenerarTokens(int cantidad)
        {
            SQLServerConnection connection = SQLServerConnection.GetInstance(connectionString);
            string codigo = "";
            for (int i = 0; i < cantidad; i++)
            {
                Guid g = Guid.NewGuid();
                codigo = g.ToString().Substring(0,5);
                connection.ExecuteNonQuery(string.Format("INSERT INTO Token(token) VALUES('{0}')", codigo));
            }
        }
    }
}