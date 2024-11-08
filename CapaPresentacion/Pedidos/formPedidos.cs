using CapaNegocio;
using System;
using System.Data;
using System.Windows.Forms;

namespace CapaPresentacion.Pedidos
{
    public partial class formPedidos : Form
    {
        CN_Pedidos objeto_pedidos = new CN_Pedidos();
        DataSet pedidos;

        int IdUsuario;
        string Usuario;
        int desde = 0;
        string total_ventas;
        int p_id_pedido;
        string p_estado_pedido = "P";

        public formPedidos()
        {
            InitializeComponent();
            llenar_cb_estado_pedido();
            listar_pedidos();
        }
        private void listar_pedidos()
        {
            var añoInicio = dtFechaInicio.Value.Year;
            var mesInicio = dtFechaInicio.Value.Month;
            var diaInicio = dtFechaInicio.Value.Day;
            var fechaInicio = añoInicio + "-" + mesInicio + "-" + diaInicio;

            var añoFin = dtFechaFin.Value.Year;
            var mesFin = dtFechaFin.Value.Month;
            var diaFin = dtFechaFin.Value.Day;
            var fechaFin = añoFin + "-" + mesFin + "-" + diaFin;

            pedidos = objeto_pedidos.listar_pedidos(this.desde, fechaInicio, fechaFin, this.p_estado_pedido);

            dataListadoPedidos.DataSource = pedidos.Tables[0];

            if (pedidos.Tables[2].Rows.Count > 0)
            {
                DataRow firstRow = pedidos.Tables[2].Rows[0];

                object value = firstRow[0];

                // this.total_ventas = value.ToString();
                lblNroPedidosPendientes.Text = value.ToString();
            }
            else
            {
                Console.WriteLine("La tabla está vacía");
            }

            //
            if (pedidos.Tables[3].Rows.Count > 0)
            {
                DataRow firstRow = pedidos.Tables[3].Rows[0];

                object value = firstRow[0];

                lblNroPedidosConfirmado.Text = value.ToString();

            }
            else
            {
                Console.WriteLine("La tabla está vacía");
            }


        }

        private void llenar_cb_estado_pedido()
        {
            cbEstadoPedido.Items.Add("Todos");
            cbEstadoPedido.Items.Add("Pendiente");
            cbEstadoPedido.Items.Add("Confirmado");

            cbEstadoPedido.SelectedItem = "Todos";

        }

        private void cbEstadoPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado
            string valorSeleccionado = cbEstadoPedido.SelectedItem.ToString();

            switch (valorSeleccionado)
            {
                case "Pendiente":
                    this.p_estado_pedido = "P";
                    break;
                case "Confirmado":
                    this.p_estado_pedido = "C";
                    break;
                default:
                    this.p_estado_pedido = "0";
                    break;
            }

            listar_pedidos();
        }

        private void btnNuevoPedido_Click(object sender, EventArgs e)
        {
            formNuevoPedido frm = new formNuevoPedido();
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            this.desde = 0;
            listar_pedidos();
        }

        private void dataListadoPedidos_SelectionChanged(object sender, EventArgs e)
        {
            if (dataListadoPedidos.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataListadoPedidos.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataListadoPedidos.Rows[selectedrowindex];
                this.p_id_pedido = Convert.ToInt32(selectedRow.Cells["IdTransaccion"].Value);
            }
        }

        private void btnConfirmarPedido_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente Desea Confrirmar el pedido", "InvenControl", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    CN_Pedidos.confirmar_pedido(this.p_id_pedido);
                    this.listar_pedidos();
                    this.MensajeOk("Confirmacion exitosa");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
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

        private void dtFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            listar_pedidos();
        }

        private void dtFechaFin_ValueChanged(object sender, EventArgs e)
        {
            listar_pedidos();
        }
    }
}
