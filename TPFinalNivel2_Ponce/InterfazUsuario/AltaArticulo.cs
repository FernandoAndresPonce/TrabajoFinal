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
using System.IO;
using System.Configuration;

namespace InterfazUsuario
{
    public partial class frmAltaArticulo : Form
    {
        private Articulo auxArticulo = null;
        private OpenFileDialog buscarImagen = null;
        public frmAltaArticulo()
        {
            InitializeComponent();
        }

        public frmAltaArticulo(Articulo seleccionArticulo)
        {
            InitializeComponent();
            this.auxArticulo = seleccionArticulo;
            Text = "Modificar Articulo";
        }
        private void AltaArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            cbMarca.DataSource = marcaNegocio.MostrarLista();
            cbMarca.ValueMember = "Id";
            cbMarca.DisplayMember = "Descripcion";
            cbMarca.SelectedIndex = -1;

            cbCategoria.DataSource = categoriaNegocio.MostrarLista();
            cbCategoria.ValueMember = "Id";
            cbCategoria.DisplayMember = "Descripcion";
            cbCategoria.SelectedIndex = -1;

            

            if (auxArticulo != null)
            {
                txtCodigo.Text = auxArticulo.Codigo;
                txtNombre.Text = auxArticulo.Nombre;
                txtDescripcion.Text = auxArticulo.Descripcion;
                cbCategoria.SelectedValue = auxArticulo.Categoria.Id;
                cbMarca.SelectedValue = auxArticulo.Marca.Id;
                
                cargarImagen(auxArticulo.ImagenUrl);
                txtImagenUrl.Text = auxArticulo.ImagenUrl;
                txtPrecio.Text = auxArticulo.Precio.ToString("0.00");

                label1.Text = "MODIFICAR ARTICULO";
                this.label1.Location = new Point (360, 45);
                btnAceptar.Text = "Modificar";
                txtCodigo.ForeColor = Color.Black;
            }
        }
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {           

            if ((e.KeyChar < 48 || e.KeyChar > 59) && e.KeyChar != 8)
                e.Handled = true;
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbAltaImagen.Load(imagen);
            }
            catch (Exception ex)
            {
                pbAltaImagen.Load("https://live.staticflickr.com/6031/6367268285_6801f2fcbc_z.jpg");

            }
        }
        private void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            cargarImagen(txtImagenUrl.Text);
        }
        private bool validarCajasVacias()
        {

            if (txtCodigo.Text == string.Empty || txtCodigo.Text == "UNA LETRA, DOS NUMEROS = X00")
            {
                MessageBox.Show("Ingrese un Codigo, Por favor.", "Atencion...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("Ingrese un Nombre, Por favor.", "Atencion...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else if (cbCategoria.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione una Categoria, Por favor.", "Atencion...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else if (cbMarca.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione una Marca, Por favor.", "Atencion...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else if (txtPrecio.Text == string.Empty)
            {
                MessageBox.Show("Ingrese un Precio, Por favor.", "Atencion...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }


            return false;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocioArticulo = new ArticuloNegocio();
            
            if (validarCajasVacias())
                return;

            try
            {
                if (auxArticulo == null)
                    auxArticulo = new Articulo();
                
                auxArticulo.Codigo = txtCodigo.Text;
                auxArticulo.Nombre = txtNombre.Text;
                auxArticulo.Descripcion = txtDescripcion.Text;
                auxArticulo.Marca = (Marca)cbMarca.SelectedItem;
                auxArticulo.Categoria = (Categoria)cbCategoria.SelectedItem;
                auxArticulo.ImagenUrl = txtImagenUrl.Text;
                try
                {
                    if (buscarImagen != null && !(txtImagenUrl.Text.ToUpper().Contains("HTTP")))
                    {
                        File.Copy(buscarImagen.FileName, ConfigurationManager.AppSettings["Articulos"] + buscarImagen.SafeFileName);
                    }
                }
                catch (ArgumentException)
                {

                    MessageBox.Show("Se Sobreescribio la Imagen, repetida.");
                }
                catch (IOException)
                {

                    MessageBox.Show("Se Sobreescribio la Imagen, que ya fue utilizada, y estaba guardada.");
                }

                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
            
                if (txtPrecio.Text != string.Empty)
                {
                    auxArticulo.Precio = decimal.Parse(txtPrecio.Text);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                
                try
                {
                    DialogResult respuesta = MessageBox.Show("El PRECIO ingresado $ " + txtPrecio.Text + " es CORRECTO?", "Atencion...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Stop);

                    if (respuesta == DialogResult.Yes)
                    {
                        if (auxArticulo.Id != 0)
                        {
                            negocioArticulo.Modificar(auxArticulo);
                            MessageBox.Show("Se ha Modificado Exitosamente", "Modificar Articulo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Dispose();
                            
                        }
                        else
                        {
                            negocioArticulo.Agregar(auxArticulo);
                            MessageBox.Show("Se ha Agregado Exitosamente", "Agregar Articulo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Dispose();
                        }
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }



            }

        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void btnBuscarImagen_Click(object sender, EventArgs e)
        {
            buscarImagen = new OpenFileDialog();

            buscarImagen.Filter = "jpg|*.jpg;| png|*.png";
            
            if (buscarImagen.ShowDialog() == DialogResult.OK)
            {
                txtImagenUrl.Text = buscarImagen.FileName;
                cargarImagen(buscarImagen.FileName);
            }
        }
        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            if(txtCodigo.Text == string.Empty)
            {
                txtCodigo.Text = "UNA LETRA, DOS NUMEROS = X00";
                txtCodigo.ForeColor = Color.Silver;
            }
        }
        private void txtCodigo_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "UNA LETRA, DOS NUMEROS = X00")
            {
                txtCodigo.Text = string.Empty;
                txtCodigo.ForeColor = Color.Black;
            }

        }
    }
}
