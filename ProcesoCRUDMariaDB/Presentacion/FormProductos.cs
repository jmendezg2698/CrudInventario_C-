using ProcesoCRUDMariaDB.Datos;
using ProcesoCRUDMariaDB.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcesoCRUDMariaDB.Presentacion
{
    public partial class FormProductos : Form
    {
        public FormProductos()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        #region"VARIABLES"
        int nEstadoGuarda = 0;
        int nCodigoProd = 0;
        int nCodigoMedida = 0;
        int nCodigoCategoria = 0;
        #endregion
        #region "METODOS"

        private void Formato() {
            tblListado.Columns[0].Width = 100;
            tblListado.Columns[0].HeaderText = "Codigo Producto";
            tblListado.Columns[1].Width = 150;
            tblListado.Columns[1].HeaderText = "Producto";
            tblListado.Columns[2].Width = 110;
            tblListado.Columns[2].HeaderText = "Marca";
            tblListado.Columns[3].Width = 100;
            tblListado.Columns[3].HeaderText = "Medida";
            tblListado.Columns[4].Width = 110;
            tblListado.Columns[4].HeaderText = "Categoria";
            tblListado.Columns[5].Width = 110;
            tblListado.Columns[5].HeaderText = "Stock Actual";
            tblListado.Columns[6].Visible = false;
            tblListado.Columns[7].Visible= false;
        }

        private void SeleccionaItem()
        {
            if (string.IsNullOrEmpty(Convert.ToString(tblListado.CurrentRow.Cells["codProducto"].Value)))
            {
                MessageBox.Show("Seleccione un registro", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                nCodigoProd = Convert.ToInt32(tblListado.CurrentRow.Cells["codProducto"].Value);
                txtProducto.Text = Convert.ToString(tblListado.CurrentRow.Cells["descProducto"].Value);
                txtMarca.Text = Convert.ToString(tblListado.CurrentRow.Cells["marcaProducto"].Value);
                cmbMedida.Text = Convert.ToString(tblListado.CurrentRow.Cells["descMedidas"].Value);
                cmbCategoria.Text = Convert.ToString(tblListado.CurrentRow.Cells["descCategoria"].Value);
                txtStock.Text = Convert.ToString(tblListado.CurrentRow.Cells["stockActual"].Value);
                nCodigoMedida = Convert.ToInt32(tblListado.CurrentRow.Cells["codMedida"].Value);
                nCodigoCategoria = Convert.ToInt32(tblListado.CurrentRow.Cells["codCategoria"].Value);
            }

        }

        private void ListadoProducto(string texto)
        {
            DProductos dProductos = new DProductos();
            tblListado.DataSource = dProductos.ListadoProductos(texto);
            this.Formato();
        }

        private void ListadoMedida()
        {
            DProductos productos = new DProductos();
            cmbMedida.DataSource = productos.ListadoMedidas();
            cmbMedida.ValueMember = "codMedidas";
            cmbMedida.DisplayMember = "descMedidas";
        }

        private void ListadoCategoria()
        {
            DProductos productos = new DProductos();
            cmbCategoria.DataSource = productos.ListadoCategoria();
            cmbCategoria.ValueMember = "codCategoria";
            cmbCategoria.DisplayMember = "descCategoria";
        }

        private void LimpiaTexto()
        {
            txtProducto.Text = "";
            txtMarca.Text = "";
            cmbCategoria.Text = "";
            cmbMedida.Text = "";
            txtStock.Text = "0.00";
        }


        private void EstadoTexto(bool estado)
        {
            txtProducto.Enabled= estado;
            txtMarca.Enabled= estado;
            txtStock.Enabled= estado;
            cmbCategoria.Enabled= estado;
            cmbMedida.Enabled = estado;
        }

        private void EstadoBotonesProceso(bool estado)
        {
            btnCancelar.Visible = estado;
            btnGuardar.Visible = estado;
        }

        private void EstadoBotonesPrincipales(bool estado)
        {
            btnNuevo.Enabled = estado;  
            btnActualiza.Enabled= estado;
            btnElimina.Enabled = estado;
            btnSalir.Enabled = estado;
            btnReporte.Enabled= estado;
        }
        #endregion

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            nEstadoGuarda = 1; //Guarda nuevo registro
            this.LimpiaTexto();
            this.EstadoTexto(true);
            this.EstadoBotonesProceso(true);
            this.EstadoBotonesPrincipales(false);
            txtProducto.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            nEstadoGuarda=0;
            this.LimpiaTexto();
            this.EstadoTexto(false);
            this.EstadoBotonesProceso(false);
            this.EstadoBotonesPrincipales(true);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormProductos_Load(object sender, EventArgs e)
        {
            this.ListadoMedida();   
            this.ListadoCategoria();
            this.ListadoProducto("%");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtProducto.Text == string.Empty || txtMarca.Text == string.Empty || cmbMedida.Text == string.Empty || cmbCategoria.Text ==  string.Empty || txtStock.Text == string.Empty)
            {
                MessageBox.Show("Ingrese los datos requeridos(*)", "Aviso del sistema",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string res = "";
                EProducto producto = new EProducto();
                nCodigoMedida = Convert.ToInt32(cmbMedida.SelectedValue);
                nCodigoCategoria = Convert.ToInt32(cmbCategoria.SelectedValue);

                producto.codigoProduto = nCodigoProd;
                producto.producto = txtProducto.Text;
                producto.marca = txtMarca.Text;
                producto.codigoMedida = nCodigoMedida;
                producto.codigoCatalogo = nCodigoCategoria;
                producto.stockActual = Convert.ToDecimal(txtStock.Text);

                DProductos dProductos = new DProductos();
                res = dProductos.GuardarProducto(nEstadoGuarda, producto);
                if (res.Equals("OK"))
                {
                    nEstadoGuarda = 0;
                    nCodigoCategoria = 0;
                    nCodigoMedida = 0;
                    nCodigoProd = 0;
                    this.LimpiaTexto();
                    this.EstadoTexto(false);
                    this.EstadoBotonesPrincipales(true);
                    this.EstadoBotonesProceso(false);
                    this.ListadoProducto("%");
                    MessageBox.Show("Los datos se guardaron correctamente.", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(res, "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.ListadoProducto(txtBuscar.Text);
        }

        private void tblListado_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.SeleccionaItem();
        }

        private void btnActualiza_Click(object sender, EventArgs e)
        {
            nEstadoGuarda = 2; //Actualiza  registro
            this.EstadoTexto(true);
            this.EstadoBotonesProceso(true);
            this.EstadoBotonesPrincipales(false);
            txtProducto.Focus();
        }

        private void btnElimina_Click(object sender, EventArgs e)
        {
            if (tblListado.Rows.Count > 0)
            {
                string res = "";
                DProductos productos = new DProductos();
                res = productos.EliminarProducto(Convert.ToInt32(tblListado.CurrentRow.Cells["codProducto"].Value));
                if (res.Equals("OK"))
                {
                    this.ListadoProducto("%");
                    MessageBox.Show("Producto eliminado correctamente","Alerta de sistema",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(res,"Aviso de sistema",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
            }
            else
            {

            }
        }
    }
}
