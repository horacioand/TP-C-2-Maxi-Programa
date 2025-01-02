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
using System.IO;
using System.Configuration;
namespace Visual
{
    public partial class frmAgregar : Form
    {
        //objeto articulo null, asigno si traigo un obj enlazado desde principal si estoy modificando o creo uno nuevo si estoy agregando
        private Articulo articulo = null;
        private OpenFileDialog archivo = null;
        public frmAgregar()
        {
            InitializeComponent();
        }
        public frmAgregar(Articulo articulo)    //Sobrecarga para traer un objeto desde formulario principal
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
        private void txtImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagen.Text);
        }//Evento leave para cargar imagen desde url
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception)
            {
                pbxArticulo.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR8bikI-KUuM1IWosgqDRS5jyv2U_PPYlG6Tg&s");
            }
        }//Pendiente centralizar en clase helper
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
                //if (archivo != null && !(txtImagen.Text.ToUpper().Contains("HTTP")))   //si se carga una imagen de forma local se guarda
                //{
                //    File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName);
                //}
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        } 
        private bool esDecimal(string texto)    //Validar precio
        {
            decimal resultado;
            return decimal.TryParse(texto, out resultado);
        }
        private void btnCerrar_Click_1(object sender, EventArgs e)  //Cerrar Formulario
        {
            Close();
        }

        //private void btnAgregarImagen_Click(object sender, EventArgs e) //Añadir imagen local
        //{
        //    archivo = new OpenFileDialog();
        //    archivo.Filter = "jpg|*.jpg;|png|*.png";
            
        //    if(archivo.ShowDialog() == DialogResult.OK)
        //    {
        //        txtImagen.Text = archivo.FileName;
        //        cargarImagen(archivo.FileName);

                //Para guardar la img necesito usar system.io, la configuracion en app.config y la referencia a system.configuration
                //File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName);
        //    }
        //}
    }
}
