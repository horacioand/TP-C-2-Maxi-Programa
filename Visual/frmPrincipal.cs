﻿using System;
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

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            cargarDatos();
            
        }
        private void cargarDatos()
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            articulos = articuloNegocio.listar();
            
            try
            {
                dgvArticulos.DataSource = articulos;
                ocultarColumna(6);
                ocultarColumna(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void ocultarColumna(int columna)
        {
            dgvArticulos.Columns[columna].Visible = false;
        }

        private void btnDetalles_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmDetalles formDetalles = new frmDetalles(seleccionado);
            formDetalles.ShowDialog();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregar frmAgregar = new frmAgregar();
            frmAgregar.ShowDialog();
            cargarDatos();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmAgregar frmModificar = new frmAgregar(seleccionado);
            frmModificar.ShowDialog();
            cargarDatos();
        }

        private void btnEliminarf_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            try
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                DialogResult respuesta = MessageBox.Show("Desea borrar el registro?", "Registro eliminado",MessageBoxButtons.YesNo);
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
    }
}
