using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;

namespace Pencas.Models
{
    public class DAOPronostico
    {
        private string connectionString = "Data Source=GABRIEL-PC;Initial Catalog=Pencas;Integrated Security=True; MultipleActiveResultSets=True;";

        /// <summary>
        /// Guarda los resultados en la base de datos
        /// </summary>
        /// <param name="resultados">Resultados en el formato partido1;golesA;golesB;partido2;golesA;golesB;...partidoN;golesA;golesB</param>
        public void GuardarPronostico(string resultados, Usuario u)
        {
            SQLServerConnection connection = SQLServerConnection.GetInstance(connectionString);

            string[] vecResultados = resultados.Split(';');
            for (int i = 0; i < vecResultados.Length; i++)
            {
                if (i % 3 == 0)
                {
                    connection.ExecuteNonQuery(String.Format("INSERT INTO PronosticoPartido(idPartido,idUsuario,golesA,golesB) VALUES({0},{1},{2},{3})", vecResultados[i], u.Id, vecResultados[i + 1], vecResultados[i + 2]));
                }
            }
        }

        public int ResultadoPronostico(Usuario u)
        {
            SQLServerConnection connection = SQLServerConnection.GetInstance(connectionString);
            string consulta = String.Format("SELECT * FROM PronosticoPartido WHERE idUsuario = {0}", u.Id);
            DbDataReader reader = connection.Execute(consulta);
            List<int> pronosticados = new List<int>();
            List<int> reales = new List<int>();
            int puntaje = 0;
            while (reader.Read())
            {
                pronosticados.Add(reader.GetInt32(2));
                pronosticados.Add(reader.GetInt32(3));
            }
            consulta = String.Format("SELECT * FROM Partido");
            reader = connection.Execute(consulta);
            while (reader.Read())
            {
                reales.Add(reader.GetInt32(2));
                reales.Add(reader.GetInt32(3));
            }
            for (int i = 0; i < 32; i = i + 2)
            {
                puntaje += PuntosPartido(pronosticados[i], pronosticados[i+1], reales[i], reales[i+1]);
            }
            return puntaje;
        }

        public int PuntosPartido(int pronosticoA, int pronosticoB, int realA, int realB)
        {
            //Si todavía no se jugó el partido, no se suman puntos
            if (realA != -1)
            {
                return 0;
            }
            return 0;
        }
    }
}