using CapaNegocio;
using System;
using System.Windows.Forms;

namespace CapaPresentacion.Pedidos
{
    public partial class formNuevoPedido : Form
    {
        int IdUsuario;
        string Usuario;
        int desde = 0;
        string total_ventas;
        int p_id_pedido;
        string p_estado_pedido = "P";
        int p_tipo_pago = 1;

        public formNuevoPedido()
        {
            InitializeComponent();
            llenar_cb_estado_pedido();
            llenar_cb_tipos_pago();
        }

        private void llenar_cb_estado_pedido()
        {
            cbEstadoPedido.Items.Add("Pendiente");
            cbEstadoPedido.Items.Add("Confirmado");

            cbEstadoPedido.SelectedItem = "Pendiente";
        }

        private void llenar_cb_tipos_pago()
        {
            cbTiposPago.Items.Add("Efectivo");
            cbTiposPago.Items.Add("Tarjeta");
            cbTiposPago.Items.Add("Mercado pago");
            cbTiposPago.Items.Add("Transferencia");

            cbTiposPago.SelectedItem = "Efectivo";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var añoFin = dtpFechaPedido.Value.Year;
            var mesFin = dtpFechaPedido.Value.Month;
            var diaFin = dtpFechaPedido.Value.Day;
            var fechaPedido = añoFin + "-" + mesFin + "-" + diaFin;

            try
            {
                string rpta = "";

                rpta = CN_Pedidos.alta_pedido(fechaPedido, this.txtDireccionEnvio.Text.Trim(), this.txtCliente.Text.Trim(),
                    this.p_estado_pedido, this.p_tipo_pago, this.txtDescripcion.Text.Trim());

                if (rpta.Equals("ok"))
                {
                    this.MensajeOk("Se Insertó de forma correcta el registro");
                }
                else
                {
                    this.MensajeError(rpta);
                }
                this.Close();

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    this.p_estado_pedido = "P";
                    break;
            }
        }

        private void cbTiposPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener el valor seleccionado
            string valorSeleccionado = cbTiposPago.SelectedItem.ToString();

            switch (valorSeleccionado)
            {
                case "Efectivo":
                    this.p_tipo_pago = 1;
                    break;
                case "Tarjeta":
                    this.p_tipo_pago = 2;
                    break;
                case "Mercado pago":
                    this.p_tipo_pago = 3;
                    break;
                case "Transferencia":
                    this.p_tipo_pago = 5;
                    break;
                default:
                    this.p_tipo_pago = 1;
                    break;
            }
        }
    }
}
