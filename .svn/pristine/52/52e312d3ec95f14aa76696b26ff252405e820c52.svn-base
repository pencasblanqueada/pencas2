using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;

namespace Pencas.Models
{
    public class SQLServerConnection
    {
        private SqlConnection sqlConn;
        private SqlCommand sqlComm;
        private static SQLServerConnection instance = null;

        public static SQLServerConnection GetInstance(string connectionString)
        {
            if (instance == null) instance = new SQLServerConnection(connectionString);
            return instance;
            
        }

        private SQLServerConnection(string connectionString)
        {
            sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
        }

        public int ExecuteNonQuery(string sqlQuery)
        {
            sqlComm = new SqlCommand(sqlQuery, this.sqlConn);
            return sqlComm.ExecuteNonQuery();
        }

        public DbDataReader Execute(string sqlQuery)
        {
            sqlComm = new SqlCommand(sqlQuery, this.sqlConn);
            return sqlComm.ExecuteReader();
        }

        ~SQLServerConnection()
        {
            //Revisar
            try
            {
                sqlConn.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}
