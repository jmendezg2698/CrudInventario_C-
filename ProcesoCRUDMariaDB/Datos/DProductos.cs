using MySql.Data.MySqlClient;
using ProcesoCRUDMariaDB.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesoCRUDMariaDB.Datos
{
    public class DProductos
    {
        public DataTable ListadoProductos(string texto)
        {
            MySqlDataReader reader;
            DataTable dataTable = new DataTable();
            MySqlConnection con = new MySqlConnection();
            try
            {
                con = Conexion.getInstancia().OpenConexion();
                MySqlCommand cmd = new MySqlCommand("uspListadoProducto", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("texto", MySqlDbType.VarChar).Value = texto;
                con.Open();
                reader = cmd.ExecuteReader();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        public DataTable ListadoMedidas()
        {
            MySqlDataReader reader;
            DataTable dataTable = new DataTable();
            MySqlConnection con = new MySqlConnection();
            try
            {
                con = Conexion.getInstancia().OpenConexion();
                MySqlCommand cmd = new MySqlCommand("uspListadoMedida", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                reader = cmd.ExecuteReader();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        public DataTable ListadoCategoria()
        {
            MySqlDataReader reader;
            DataTable dataTable = new DataTable();
            MySqlConnection con = new MySqlConnection();
            try
            {
                con = Conexion.getInstancia().OpenConexion();
                MySqlCommand cmd = new MySqlCommand("uspListadoCategoria", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                reader = cmd.ExecuteReader();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        public string GuardarProducto(int nOpcion, EProducto producto)
        {
            string res = "";
            MySqlConnection mySql = new MySqlConnection();
            try
            {
                mySql = Conexion.getInstancia().OpenConexion();
                MySqlCommand cmd = new MySqlCommand("uspGuardarProducto", mySql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("nOpcion", MySqlDbType.Int32).Value = nOpcion;
                cmd.Parameters.Add("nCodigoProducto", MySqlDbType.Int32).Value = producto.codigoProduto;
                cmd.Parameters.Add("cDescripcionProducto", MySqlDbType.VarChar).Value = producto.producto;
                cmd.Parameters.Add("cMarcaProducto", MySqlDbType.VarChar).Value = producto.marca;
                cmd.Parameters.Add("nCodigoMedida", MySqlDbType.Int32).Value = producto.codigoMedida;
                cmd.Parameters.Add("nCodigoCategoria", MySqlDbType.Int32).Value = producto.codigoCatalogo;
                cmd.Parameters.Add("nStockActual", MySqlDbType.Decimal).Value = producto.stockActual;
                mySql.Open();
                res = cmd.ExecuteNonQuery() >= 1 ? "OK" : "No se pudo registrar los datos";
            }
            catch(Exception e) {
                res = e.Message;
            }
            finally
            {
                if(mySql.State == ConnectionState.Open) mySql.Close();
            }
            return res;
        }

        public string EliminarProducto(int codProducto)
        {
            string res = "";
            MySqlConnection mySql = new MySqlConnection();
            try
            {
                mySql = Conexion.getInstancia().OpenConexion();
                MySqlCommand cmd = new MySqlCommand("uspEliminarProducto", mySql);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("nCodigoProducto", MySqlDbType.Int32).Value = codProducto;
                mySql.Open();
                res = cmd.ExecuteNonQuery() >= 1 ? "OK" : "No se pudo eliminar el registro";
            }
            catch (Exception e)
            {
                res = e.Message;
            }
            finally
            {
                if (mySql.State == ConnectionState.Open) mySql.Close();
            }
            return res;
        }

    }
}
