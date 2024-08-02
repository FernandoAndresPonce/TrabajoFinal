using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; //

namespace Dominio
{
    public class Articulo
    {
        /*
         Código de artículo.
        Nombre.
        Descripción.
        Marca (seleccionable de una lista desplegable).
        Categoría (seleccionable de una lista desplegable.
        Imagen.
        Precio.
         */

        public int Id { get; set; }
        [DisplayName("Código")]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public string ImagenUrl { get; set; }
        public decimal Precio { get; set; }
        public Marca Marca { get; set; }
        [DisplayName("Categoría")]
        public Categoria Categoria { get; set; }

    }
}
