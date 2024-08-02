using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class Acceso
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public Acceso()
        {
            this.conexion = new SqlConnection("server= .\\SQLEXPRESS; database= CATALOGO_DB; integrated security= true");
            this.comando = new SqlCommand();
        }
        public SqlDataReader Lector { get {return lector; } }

        public void setearConsulta(string consulta)
        {
            try
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader(); //Ojo.
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ejectuarAccion()
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
        public void cerrarConexion()
        {
            try
            {
                if (Lector != null)
                    Lector.Close();//Ojo

                conexion.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void ejecutarParametro(string dato, object articulo)
        {
            try
            {
                comando.Parameters.AddWithValue(dato, articulo);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

    }
}
