using System;
using System.Data;
using System.Windows.Forms;

using CapaNegocio;
using CapaPresentacion.Clientes;

namespace CapaPresentacion
{
    public partial class formClientes : Form
    {
        CN_Clientes objetoCN = new CN_Clientes();

        private int IdCliente;
        int desde = 0;
        int totalClientes = 0;
        DataSet dsClientes = new DataSet();


        public formClientes()
        {
            InitializeComponent();
            buscar_clientes_paginado();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buscar_clientes_paginado();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente Desea Eliminar el cliente", "InvenControl", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    CN_Clientes.Eliminar(this.IdCliente);
                    this.buscar_clientes_paginado();
                }
                this.MensajeOk("Se elimino de forma correcta el registro");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            this.Close();
        }

        private void buscar_clientes_paginado()
        {
            // this.dataListadoClientes.DataSource = objetoCN.buscar_clientes_paginado(this.txtBuscar.Text,this.desde);

            dsClientes = objetoCN.buscar_clientes_paginado(this.txtBuscar.Text, this.desde);
            dataListadoClientes.DataSource = dsClientes.Tables[0];
            totalClientes = Convert.ToInt32(dsClientes.Tables[1].Rows[0][0]);

            dataListadoClientes.Columns["IdPersona"].Visible = false;

            lblTotalClientes.Text = "Total de Registros : " + totalClientes.ToString();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.buscar_clientes_paginado();
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            formNuevoEditarClientes frm = new formNuevoEditarClientes(this.IdCliente, true);
            frm.MdiParent = this.MdiParent;
            frm.Show();
            this.Close();
        }

        private void dataListadoClientes_SelectionChanged(object sender, EventArgs e)
        {
            if (dataListadoClientes.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataListadoClientes.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataListadoClientes.Rows[selectedrowindex];
                this.IdCliente = Convert.ToInt32(selectedRow.Cells["IdPersona"].Value);
            }
        }

        private void botonEditarListado_Click_1(object sender, EventArgs e)
        {
            formNuevoEditarClientes frm = new formNuevoEditarClientes(this.IdCliente, false);
            frm.MdiParent = this.MdiParent;
            frm.Show();
            this.Close();
        }

        private void formClientes_Load(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente Desea Eliminar el cliente", "InvenControl", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    CN_Clientes.Eliminar(this.IdCliente);
                    this.buscar_clientes_paginado();
                    this.MensajeOk("Se elimino de forma correcta el registro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            this.Close();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar_Click(this, new EventArgs());
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            this.buscar_clientes_paginado();
        }

        private void btnRefrescar_Click_1(object sender, EventArgs e)
        {
            this.desde = 0;
            this.txtBuscar.Text = "";
            this.buscar_clientes_paginado();
        }


        //Mostrar Mensaje de Confirmación
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        //Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnVerCuenta_Click(object sender, EventArgs e)
        {
            formCuentasCorrientes frm = new formCuentasCorrientes(this.IdCliente);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if ((desde + 20) >= Convert.ToInt32(lblTotalClientes))
            {
                return;
            }

            if (desde < 0)
            {
                return;
            }

            this.desde += 20;
            this.buscar_clientes_paginado();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (desde <= 0)
            {
                return;
            }

            this.desde -= 20;
            this.buscar_clientes_paginado();
        }
    }

}
