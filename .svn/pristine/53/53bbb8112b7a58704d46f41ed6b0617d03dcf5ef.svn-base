using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data.Common;

namespace Pencas.Models
{
    public class DAOUsuario
    {
        private string connectionString = "Data Source=GABRIEL-PC;Initial Catalog=Pencas;Integrated Security=True; MultipleActiveResultSets=True;";

        public List<Usuario> GetUsuarios()
        {
            List<Usuario> retorno = new List<Usuario>();
            SQLServerConnection connection = SQLServerConnection.GetInstance(connectionString);

            DbDataReader reader = connection.Execute("SELECT * FROM Usuario");
            while (reader.Read())
            {
                Usuario u = new Usuario();
                u.Id = reader.GetInt32(0);
                u.Nombre = reader.GetString(1);
                retorno.Add(u);
            }
            reader.Close();
            return retorno;
        }

        public void AsociarMailToken(string email, string token)
        {
            SQLServerConnection connection = SQLServerConnection.GetInstance(connectionString);
            string consulta = String.Format("SELECT * FROM Token WHERE token = '{0}'", token);
            DbDataReader reader = connection.Execute(consulta);
            if (!reader.Read()) throw new NoExisteElTokenException();
            reader.Close();
            consulta = String.Format("SELECT * FROM Token WHERE token = '{0}' AND idUsuario = -1", token);
            reader = connection.Execute(consulta);
            if (!reader.Read()) throw new YaEstaAsociadoElTokenConOtroUsuarioException();
            reader.Close();
            AgregarUsuario(email);
            Usuario usuario = GetUsuarioPorEmail(email);
            connection.ExecuteNonQuery(String.Format("UPDATE Token SET idUsuario = {0} WHERE token = '{1}'", usuario.Id, token ));
        }

        public void AgregarUsuario(string email)
        {
            SQLServerConnection connection = SQLServerConnection.GetInstance(connectionString);
            try
            {
                Usuario u = GetUsuarioPorEmail(email);
            }catch(NoExisteUsuarioException){
                connection.ExecuteNonQuery(String.Format("INSERT INTO Usuario(email, EnvioPronostico) VALUES('{0}', 'false')", email));
                return;
            }
            throw new YaExisteUsuarioException();
        }

        public Usuario GetUsuarioPorEmail(string email)
        {
            SQLServerConnection connection = SQLServerConnection.GetInstance(connectionString);
            DbDataReader reader = connection.Execute(String.Format("SELECT * FROM Usuario WHERE email = '{0}'", email)/*"SELECT * FROM Usuario u, Token t WHERE email = '{0}' and u.Id = t.idUsuario", email)*/);
            if (reader.Read())
            {
                Usuario u = new Usuario();
                u.Id = reader.GetInt32(0);
                u.Nombre = reader.GetString(1);
                u.EnvioPronostico = reader.GetBoolean(2);
                return u;
            }
            else
            {
                throw new NoExisteUsuarioException();
            }
        }

        public string GetTokenPorEmail(string email)
        {
            SQLServerConnection connection = SQLServerConnection.GetInstance(connectionString);
            DbDataReader reader = connection.Execute(String.Format("SELECT * FROM Usuario u, Token t WHERE u.email = '{0}' AND u.Id = t.idUsuario", email)/*"SELECT * FROM Usuario u, Token t WHERE email = '{0}' and u.Id = t.idUsuario", email)*/);
            reader.Read();
            return reader.GetString(3);
        }
    }
}