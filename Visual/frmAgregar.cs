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
        public frmAgregar()
        {
            InitializeComponent();
        }

        private void frmAgregar_Load(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            try
            {
                cboCategoria.DataSource = categoriaNegocio.listar();
                cboMarca.DataSource = marcaNegocio.listar();
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
            Articulo nuevoArticulo = new Articulo();
            nuevoArticulo.CodigoArticulo = txtCodigo.Text;
            nuevoArticulo.Nombre = txtNombre.Text;
            nuevoArticulo.Descripcion = txtDescripcion.Text;
            if (esDecimal(txtPrecio.Text))
            {
                nuevoArticulo.Precio = decimal.Parse(txtPrecio.Text);
            }
            else
            {
                nuevoArticulo.Precio = 0;
            }
            
        }
        
        private bool esDecimal(string texto)
        {
            decimal resultado;
            return decimal.TryParse(texto, out resultado);
        }
    }
}
