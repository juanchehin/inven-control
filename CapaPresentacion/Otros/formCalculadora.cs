using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CapaPresentacion.Otros
{
    public partial class formCalculadora : Form
    {
        bool bandera = false;
        public formCalculadora()
        {
            InitializeComponent();
            rellenarCbCuotas();
            this.panelCalculo.Visible = false;
            this.txtTasa.Text = "0";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            this.calcular();
        }

        private void calcular()
        {
            this.bandera = true;
            try
            {
                if (Convert.ToDecimal(this.txtTasa.Text) > 100)
                {
                    MensajeError("La tasa debe ser menor a 100");
                    return;
                }
                if (Convert.ToDecimal(this.txtPrecioCompra.Text) <= 0 || String.IsNullOrEmpty(this.txtPrecioCompra.ToString()))
                {
                    MensajeError("Precio incorrecto");
                    return;
                }
                if (Convert.ToDecimal(this.cbCuotas.Text) <= 0)
                {
                    MensajeError("Cuotas incorrectas");
                    return;
                }

                this.panelCalculo.Visible = true;
                decimal resultado, cuotas;
                this.lblValorCompra.Text = this.txtPrecioCompra.Text;

                resultado = Convert.ToDecimal(this.lblValorCompra.Text) + ((Convert.ToDecimal(this.lblValorCompra.Text) * Convert.ToDecimal(this.txtTasa.Text)) / 100);

                this.lblValorCompra.Text = "$ " + this.lblValorCompra.Text;
                this.lblPrecioConInteres.Text = "$ " + Convert.ToString(resultado);
                this.lblCuotasDe.Text = this.cbCuotas.Text + " cuotas de : ";
                this.lblTasa.Text = this.txtTasa.Text + " %";

                cuotas = Convert.ToDecimal(resultado) / Convert.ToDecimal(this.cbCuotas.Text);
                this.lblCuotas.Text = cuotas.ToString();
            }
            catch (Exception e)
            {
                MensajeError(e.Message);
                MensajeError("Compruebe los campos del formulario");
            }

        }

        private void rellenarCbCuotas()
        {
            var cantCuotas = new List<Valor>();

            cantCuotas.Add(new Valor() { Index = "1", Value = "1" });
            cantCuotas.Add(new Valor() { Index = "2", Value = "2" });
            cantCuotas.Add(new Valor() { Index = "3", Value = "3" });
            cantCuotas.Add(new Valor() { Index = "4", Value = "4" });
            cantCuotas.Add(new Valor() { Index = "5", Value = "5" });
            cantCuotas.Add(new Valor() { Index = "6", Value = "6" });
            cantCuotas.Add(new Valor() { Index = "7", Value = "7" });
            cantCuotas.Add(new Valor() { Index = "8", Value = "8" });
            cantCuotas.Add(new Valor() { Index = "9", Value = "9" });
            cantCuotas.Add(new Valor() { Index = "10", Value = "10" });

            cbCuotas.DataSource = cantCuotas;
            cbCuotas.DisplayMember = "Value";
            cbCuotas.ValueMember = "Index";
        }

        public class Valor
        {
            public string Value { get; set; }
            public string Index { get; set; }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.panelCalculo.Visible = false;
        }


        private void formCalculadora_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.calcular();
            }
            if ((e.KeyCode == Keys.Escape) && (panelCalculo.Visible = false))
            {
                this.Close();
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

        private void txtTasa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.calcular();
            }
            if ((e.KeyCode == Keys.Escape) && (panelCalculo.Visible = true))
            {
                this.panelCalculo.Visible = false;
            }

        }

        private void cbCuotas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.calcular();
            }
        }

        private void txtPrecioCompra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.calcular();
            }
        }

        private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if (!Char.IsDigit(chr) && chr != 8 && this.bandera == false)
            {
                e.Handled = true;
                MessageBox.Show("Debe ingresar valores numericos ");
            }
        }

        private void cbCuotas_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if (!Char.IsDigit(chr) && chr != 8 && this.bandera == false)
            {
                e.Handled = true;
                MessageBox.Show("Debe ingresar valores numericos ");
            }
        }

        private void txtTasa_KeyPress(object sender, KeyPressEventArgs e)
        {
            Char chr = e.KeyChar;

            if (!Char.IsDigit(chr) && chr != 8 && this.bandera == false)
            {
                e.Handled = true;
                MessageBox.Show("Debe ingresar valores numericos ");
            }
        }
    }
}
