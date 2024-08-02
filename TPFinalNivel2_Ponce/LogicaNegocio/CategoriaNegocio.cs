using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDatos;

namespace LogicaNegocio
{
    public class CategoriaNegocio
    {
        private Acceso datos;
        private List<Categoria> listaCategoria;

        public CategoriaNegocio()
        {
            this.datos = new Acceso();
            this.listaCategoria = new List<Categoria>();
        }

        public List<Categoria> MostrarLista()
        {
            try
            {
                datos.setearConsulta("select id, Descripcion from CATEGORIAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria auxCategoria = new Categoria();
                    auxCategoria.Id = (int)datos.Lector["Id"];
                    auxCategoria.Descripcion = (string)datos.Lector["Descripcion"];

                    listaCategoria.Add(auxCategoria);
                }

                return listaCategoria;
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
