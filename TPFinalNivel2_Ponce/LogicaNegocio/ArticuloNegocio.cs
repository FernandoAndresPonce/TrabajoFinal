using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDatos;

namespace LogicaNegocio
{
    public class ArticuloNegocio
    {
        private Acceso datos;
        private List<Articulo> listaAritculos;

        public ArticuloNegocio()
        {
            this.datos = new Acceso();
            this.listaAritculos = new List<Articulo>();
        }

        public List<Articulo> MostrarLista()
        {
            try
            {
                datos.setearConsulta("select C.Descripcion Categoria, M.Descripcion Marca, A.Codigo, A.Nombre, A.Descripcion, A.Precio, A.ImagenUrl, A.Id, A.IdMarca, a.IdCategoria from ARTICULOS A, MARCAS M, CATEGORIAS C where M.Id = A.IdMarca And C.Id = A.IdCategoria");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo auxArticulo = new Articulo();
                    
                    auxArticulo.Id = (int)datos.Lector["Id"];
                    auxArticulo.Codigo = (string)datos.Lector["Codigo"];
                    auxArticulo.Nombre = (string)datos.Lector["Nombre"];
                    auxArticulo.Descripcion = (string)datos.Lector["Descripcion"];
                    auxArticulo.Precio = (decimal)datos.Lector["Precio"];  
                    
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        auxArticulo.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    auxArticulo.Categoria = new Categoria();
                    auxArticulo.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    auxArticulo.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    
                    auxArticulo.Marca = new Marca();
                    auxArticulo.Marca.Descripcion = (string)datos.Lector["Marca"];
                    auxArticulo.Marca.Id = (int)datos.Lector["IdMarca"];

                    listaAritculos.Add(auxArticulo);
                }

                return listaAritculos;
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

        public void Agregar(Articulo auxArticulo)
        {
            try
            {
                datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @Precio)");
                datos.ejecutarParametro("@Codigo", auxArticulo.Codigo);
                datos.ejecutarParametro("@Nombre", auxArticulo.Nombre);
                datos.ejecutarParametro("@Descripcion", auxArticulo.Descripcion);
                datos.ejecutarParametro("@IdMarca", auxArticulo.Marca.Id);
                datos.ejecutarParametro("@IdCategoria", auxArticulo.Categoria.Id);
                datos.ejecutarParametro("@ImagenUrl", auxArticulo.ImagenUrl);
                datos.ejecutarParametro("@Precio", auxArticulo.Precio);
                datos.ejectuarAccion();
            }
            catch (Exception ex)
            {

                throw ex ;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void EliminacionFisica(Articulo seleccionarArticulo)
        {
            try
            {
                datos.setearConsulta("delete from ARTICULOS where id = @Id");
                datos.ejecutarParametro("@Id", seleccionarArticulo.Id);
                datos.ejectuarAccion();
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

        public void Modificar(Articulo auxArticulo)
        {
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo =@Codigo, Nombre =@Nombre, Descripcion =@Descripcion, IdMarca =@IdMarca , IdCategoria =@IdCategoria, ImagenUrl =@ImageUrl, Precio =@Precio where Id =@Id");
                datos.ejecutarParametro("@Codigo", auxArticulo.Codigo);
                datos.ejecutarParametro("@Nombre", auxArticulo.Nombre);
                datos.ejecutarParametro("@Descripcion", auxArticulo.Descripcion);
                datos.ejecutarParametro("@IdMarca", auxArticulo.Marca.Id);
                datos.ejecutarParametro("@IdCategoria", auxArticulo.Categoria.Id);
                datos.ejecutarParametro("@ImageUrl", auxArticulo.ImagenUrl);
                datos.ejecutarParametro("@Precio", auxArticulo.Precio);
                datos.ejecutarParametro("@Id", auxArticulo.Id);
                datos.ejectuarAccion();
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
