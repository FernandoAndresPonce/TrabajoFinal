using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using LogicaNegocio;

namespace InterfazUsuario
{
    public partial class frmCaracteristica : Form
    {
        private Articulo auxArticulo = null;
        public frmCaracteristica()
        {
            InitializeComponent();
        }
        public frmCaracteristica(Articulo seleccionArticulo)
        {
            InitializeComponent();
            this.auxArticulo = seleccionArticulo;
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbImagenD.Load(imagen);
            }
            catch (Exception ex)
            {
                pbImagenD.Load("https://live.staticflickr.com/6031/6367268285_6801f2fcbc_z.jpg");
            }
        }
        private void frmCaracteristica_Load(object sender, EventArgs e)
        {
            try
            {
                if (auxArticulo != null)
                {
                    lblCategoriaD.Text = auxArticulo.Categoria.Descripcion;
                    lblCodigoD.Text = auxArticulo.Codigo;
                    lblNombreD.Text = auxArticulo.Nombre;
                    lblMarcaD.Text = auxArticulo.Marca.Descripcion;
                    lblPrecioD.Text = "$ " + auxArticulo.Precio.ToString("0.00");
                    cargarImagen(auxArticulo.ImagenUrl);
                    txtDescripcionD.Text = auxArticulo.Descripcion;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }     
    }
}
