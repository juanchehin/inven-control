using CapaNegocio;
using System;
using System.Windows.Forms;

namespace CapaPresentacion.Gastos
{
    public partial class formNuevoGasto : Form
    {
        CN_Gastos objetoCN = new CN_Gastos();

        int p_id_tipo_pago_seleccionado = 0;
        public formNuevoGasto()
        {
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
            // Obtener el valor seleccionado
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
            var añoFin = dtpFechaGasto.Value.Year;
            var mesFin = dtpFechaGasto.Value.Month;
            var diaFin = dtpFechaGasto.Value.Day;
            var fechaGasto = añoFin + "-" + mesFin + "-" + diaFin;

            try
            {
                string rpta = "";
                if (this.txtMontoGasto.Text == string.Empty)
                {
                    MensajeError("Falta ingresar el monto");
                }
                else
                {
                    rpta = CN_Gastos.alta_gasto(this.txtMontoGasto.Text.Trim(), fechaGasto, this.p_id_tipo_pago_seleccionado, this.txtDescripcion.Text.Trim());

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
