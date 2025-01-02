using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Negocio
{
    public class AccesoDatos
    {
        SqlConnection conexion;
        SqlCommand comando;
        SqlDataReader reader;
        public SqlDataReader Reader { get { return reader; } }  
        public AccesoDatos()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS ; database=CATALOGO_DB ; integrated security=true");
            comando = new SqlCommand(); 
        }
        //
        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }
        //
        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open(); 
                reader = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //
        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }
        //
        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //
        public void cerrarConexion()
        {
            if (reader != null)
            {
                reader.Close();
            }
            conexion.Close();
        }
    }
}
