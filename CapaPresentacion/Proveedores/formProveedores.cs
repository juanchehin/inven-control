using CapaNegocio;
using System;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class formProveedores : Form
    {
        CN_Proveedores objetoCN = new CN_Proveedores();

        private int IdProveedor;
        public formProveedores()
        {
            InitializeComponent();
            MostrarProveedores();
        }


        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar_Click(this, new EventArgs());
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarProveedor();
        }

        private void BuscarProveedor()
        {
            this.dataListadoProveedores.DataSource = objetoCN.BuscarProveedor(this.txtBuscar.Text);
            // this.OcultarColumnas();
            lblTotalProveedores.Text = "Total de Registros: " + Convert.ToString(dataListadoProveedores.Rows.Count);
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            this.MostrarProveedores();
        }

        public void MostrarProveedores()
        {
            // Console.WriteLine("Ahora va el mostrar productos");
            dataListadoProveedores.DataSource = objetoCN.MostrarProveedores();
            dataListadoProveedores.Columns[0].Visible = false;
            lblTotalProveedores.Text = "Total de Registros: " + Convert.ToString(dataListadoProveedores.Rows.Count);
            // this.banderaFormularioHijo = false;
        }

        private void botonEditarListado_Click(object sender, EventArgs e)
        {
            formNuevoEditarProveedor frm = new formNuevoEditarProveedor(this.IdProveedor, false);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void dataListadoProveedores_SelectionChanged(object sender, EventArgs e)
        {
            if (dataListadoProveedores.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataListadoProveedores.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataListadoProveedores.Rows[selectedrowindex];
                this.IdProveedor = Convert.ToInt32(selectedRow.Cells["IdProveedor"].Value);
            }
        }

        private void btnNuevoProveedor_Click(object sender, EventArgs e)
        {
            formNuevoEditarProveedor frm = new formNuevoEditarProveedor(this.IdProveedor, true);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente Desea Eliminar el proveedor", "InvenControl", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    CN_Proveedores.Eliminar(this.IdProveedor);
                    this.MostrarProveedores();
                    this.MensajeOk("Se elimino de forma correcta el proveedor");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            // this.Close();
        }

        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        //Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnRefrescar_Click_1(object sender, EventArgs e)
        {
            this.BuscarProveedor();
        }
    }
}
