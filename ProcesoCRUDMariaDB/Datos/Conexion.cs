using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProcesoCRUDMariaDB.Datos
{
    public class Conexion
    {
        private string db;
        private string server;
        private string port;
        private string user;
        private string password;
        private static Conexion con = null;

        private Conexion()
        {
            this.db = "dbcrud";
            this.server = "127.0.0.1";
            this.port = "3307";
            this.user = "userCrud";
            this.password = "123456";
        }

        public MySqlConnection OpenConexion()
        {
            MySqlConnection connection = new MySqlConnection();
            try
            {
                connection.ConnectionString = "datasource=" + 
                    this.server + ";port=" + 
                    this.port + ";username=" + 
                    this.user + ";password=" + 
                    this.password + ";Database=" + 
                    this.db;
            }catch (Exception e){
                connection = null;
                throw e;
            }
            return connection;
        }

        public static Conexion getInstancia()
        {
            if(con == null)
            {
                con = new Conexion();
            }
            return con;
        }
    }


}
