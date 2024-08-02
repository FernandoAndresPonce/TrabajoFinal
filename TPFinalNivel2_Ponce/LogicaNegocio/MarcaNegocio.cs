using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDatos;

namespace LogicaNegocio
{
    public class MarcaNegocio
    {
        private Acceso datos;
        private List<Marca> listaMarca;

        public MarcaNegocio()
        {
            this.datos = new Acceso();
            this.listaMarca = new List<Marca>();
        }

        public List<Marca> MostrarLista()
        {
            try
            {
                datos.setearConsulta("select Id, Descripcion from MARCAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca auxMarca = new Marca();
                    auxMarca.Id = (int)datos.Lector["Id"];
                    auxMarca.Descripcion = (string)datos.Lector["Descripcion"];

                    listaMarca.Add(auxMarca);
                }

                return listaMarca;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
