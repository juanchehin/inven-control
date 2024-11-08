using CapaNegocio;
using System;
using System.Windows.Forms;

namespace CapaPresentacion.Clientes
{
    public partial class formNuevoDeposito : Form
    {
        CN_Clientes objetoCN = new CN_Clientes();
        int p_id_cliente;

        int p_id_tipo_pago_seleccionado = 0;
        public formNuevoDeposito(int p_id_cliente)
        {
            this.p_id_cliente = p_id_cliente;
            InitializeComponent();
            llenar_cb_tipos_pago();
        }

        private void llenar_cb_tipos_pago()
        {
            cbTiposPago.Items.Add("Efectivo");
            cbTiposPago.Items.Add("Tarjeta");
            cbTiposPago.Items.Add("Mercado pago");
            cbTiposPago.Items.Add("Transferencia");

            cbTiposPago.SelectedItem = "Efectivo";

        }

        private void cbTiposPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorSeleccionado = cbTiposPago.SelectedItem.ToString();

            switch (valorSeleccionado)
            {
                case "Efectivo":
                    this.p_id_tipo_pago_seleccionado = 1;
                    break;
                case "Tarjeta":
                    this.p_id_tipo_pago_seleccionado = 2;
                    break;
                case "Mercado pago":
                    this.p_id_tipo_pago_seleccionado = 3;
                    break;
                case "Transferencia":
                    this.p_id_tipo_pago_seleccionado = 5;
                    break;
                default:
                    this.p_id_tipo_pago_seleccionado = 0;
                    break;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var añoFin = dtpFechaDeposito.Value.Year;
            var mesFin = dtpFechaDeposito.Value.Month;
            var diaFin = dtpFechaDeposito.Value.Day;
            var fechaDeposito = añoFin + "-" + mesFin + "-" + diaFin;

            try
            {
                string rpta = "";
                if (this.txtMontoGasto.Text == string.Empty)
                {
                    MensajeError("Falta ingresar el monto");
                }
                else
                {
                    rpta = CN_Clientes.alta_deposito(this.p_id_cliente, this.txtMontoGasto.Text.Trim(), fechaDeposito, this.p_id_tipo_pago_seleccionado, this.txtDescripcion.Text.Trim());

                    if (rpta.Equals("Ok"))
                    {
                        this.MensajeOk("Se Insertó de forma correcta el registro");
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }
                    this.Close();
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

        private void txtMontoGasto_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if (e.KeyChar == '.')
            {
                // Reemplazar el punto '.' por una coma ','
                e.KeyChar = ',';
            }

            if (!Char.IsDigit(chr) && chr != 8 && chr != 8 && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Debe ingresar valores numericos ");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
