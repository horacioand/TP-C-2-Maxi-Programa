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
using Negocio;
namespace Visual
{
    public partial class frmAgregar : Form
    {
        private Articulo articulo = null;
        public frmAgregar()
        {
            InitializeComponent();
        }
        public frmAgregar(Articulo articulo)
        {   
            InitializeComponent();
            Text = "Modificar Articulo";
            this.articulo = articulo;
        }

        private void frmAgregar_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            try
            {
                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";
                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                if (articulo != null) 
                {
                    txtCodigo.Text = articulo.CodigoArticulo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtImagen.Text = articulo.Imagen;
                    cargarImagen(articulo.Imagen);
                    txtPrecio.Text = articulo.Precio.ToString();
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void txtImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagen.Text);
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception)
            {
                pbxArticulo.Load("https://doc24.com.ar/wp-content/uploads/2023/10/placeholder-2-1.png");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();

            try
            {
                if (articulo == null)
                    articulo = new Articulo();  
                
                articulo.CodigoArticulo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                if (esDecimal(txtPrecio.Text))
                    articulo.Precio = decimal.Parse(txtPrecio.Text);
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
                articulo.Imagen = txtImagen.Text;

                if(articulo.Id != 0)
                {
                    articuloNegocio.modificarArticulo(articulo);
                    MessageBox.Show("Modificado Exitosamente");
                }
                else
                {
                    articuloNegocio.agregarArticulo(articulo);
                    MessageBox.Show("Agregado Exitosamente");
                }

                Close();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        
        private bool esDecimal(string texto)
        {
            decimal resultado;
            return decimal.TryParse(texto, out resultado);
        }
    }
}
