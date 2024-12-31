using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visual
{
    public partial class frmDetalles : Form
    {
        private Articulo articulo = null;
        public frmDetalles(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        //Carga de datos del articulo seleccionado en cajas de texto (detalles)
        private void frmDetalles_Load(object sender, EventArgs e)
        {
            try
            {
                txtCodigo.Text = articulo.CodigoArticulo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                txtMarca.Text = articulo.Marca.Descripcion;
                txtCategoria.Text = articulo.Categoria.Descripcion;
                txtPrecio.Text = articulo.Precio.ToString();
                cargarImagen(articulo.Imagen);
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
        
        //Cierre de formulario
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
