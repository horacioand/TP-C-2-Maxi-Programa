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
    public partial class frmPrincipal : Form
    {
        private List<Articulo> articulos;
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)  //Carga de formulario
        {
            cargarDatos();
            cboCampo.Items.Add("Código");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Descripcion");
            cboCampo.Items.Add("Precio");
        }
        
        private void cargarDatos()  //Funcion carga de datos en dataGridView
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            articulos = articuloNegocio.listar();
            
            try
            {
                dgvArticulos.DataSource = articulos;
                ocultarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ocultarColumnas()  //Funcion ocultar columna en dgv
        {
            dgvArticulos.Columns[0].Visible = false;
            dgvArticulos.Columns[6].Visible = false;
        }
       
        private void btnDetalles_Click(object sender, EventArgs e)  //Nueva ventana form detalles
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                frmDetalles formDetalles = new frmDetalles(seleccionado);
                formDetalles.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor seleccione un artículo");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)   //Nueva ventana form agregar articulo
        {
            frmAgregar frmAgregar = new frmAgregar();
            frmAgregar.ShowDialog();
            cargarDatos();
        }

        private void btnModificar_Click(object sender, EventArgs e) //Nueva ventana modificar articulo
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                frmAgregar frmModificar = new frmAgregar(seleccionado);
                frmModificar.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor seleccione un artículo");
            }
            cargarDatos();
        }

        private void btnEliminarf_Click(object sender, EventArgs e) //Boton eliminar artículo (físico)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            try
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                DialogResult respuesta = MessageBox.Show("Desea borrar el registro?", "Registro eliminado", MessageBoxButtons.YesNo);
                if (respuesta == DialogResult.Yes)
                {
                    articuloNegocio.eliminarFisico(seleccionado.Id);
                    MessageBox.Show("Eliminado correctamente");
                    cargarDatos();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            try
            {
                string campo = cboCampo.SelectedItem.ToString();
                string criterio = cboCriterio.SelectedItem.ToString();
                string filtro = txtFiltroAv.Text;
                dgvArticulos.DataSource = articuloNegocio.filtrarArticulo(campo, criterio, filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            

        }

        private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            List<Articulo> listaFiltrada;
            string filtro = txtFiltro.Text;
            if (filtro.Length >= 2)
            {
                listaFiltrada = articulos.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Descripcion.ToUpper().Contains(filtro.ToUpper()));

            }
            else
            {
                listaFiltrada = articulos;
            }
            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            ocultarColumnas();
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            if (opcion == "Precio")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con..");
                cboCriterio.Items.Add("Termina con..");
                cboCriterio.Items.Add("Contiene..");
            }
        }
    }
}
