using CapaNegocio;
using System;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class formActualizarPrecios : Form
    {
        public formActualizarPrecios()
        {
            InitializeComponent();
            
        }
        private void formActualizarPrecios_Load(object sender, EventArgs e)
        {
            cbActualizarTodos.Checked = true;
            tbDesdeArticulo.ReadOnly = true;
            tbHastaArticulo.ReadOnly = true;
            tbLineal.ReadOnly = true;

            rbLineal.Checked = false;
            rbPorcentual.Checked = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();                        
        }


        private void cbActualizarTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbActualizarTodos.Checked)
            {
                tbDesdeArticulo.ReadOnly = false;
                tbHastaArticulo.ReadOnly = false;
            }
            else
            {
                tbDesdeArticulo.ReadOnly = true;
                tbHastaArticulo.ReadOnly = true;
            }
            
        }

        private void lblPorcentual_Click(object sender, EventArgs e)
        {
            tbLineal.ReadOnly = true;
            tbPorcentual.ReadOnly = false;
            tbPorcentual.Focus();
            rbPorcentual.Checked = true;

        }

        private void lblLineal_Click(object sender, EventArgs e)
        {
            tbPorcentual.ReadOnly = true;
            tbLineal.ReadOnly = false;
            tbLineal.Focus();
            rbLineal.Checked = true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string message = "Esta a punto de actualizar los precios.¿Desea confirmar la operacion?";
            string caption = "Atencion";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No)
            {
                this.Close();
                return;
            }
            string rpta = "";
            // Validaciones
            try
            {
                // ======= Caso actualizar todos ============
                if (cbActualizarTodos.Checked)
                {
                    // ======= Caso actualizar todos y porcentual ============
                    if (rbPorcentual.Checked)
                    {
                        if (this.tbPorcentual.Text == string.Empty)
                        {
                            MensajeError("Falta ingresar algunos datos");
                        }
                        else
                        {
                            // Solo numerico
                            uint value;
                            if (!uint.TryParse(tbPorcentual.Text, out value))
                            {
                                MensajeError("Solo debe ingresar datos numericos");
                                return;
                            }
                            // Solo menor o igual a 100
                            if (Convert.ToInt32(tbPorcentual.Text) > 100)
                            {
                                MensajeError("Debe ingresar un valor menor o igual a 100%");
                                return;
                            }
                            Decimal dValue = -1;
                            decimal.TryParse(this.tbPorcentual.Text, out dValue);
                            rpta = CN_Productos.ActualizacionPorcentual(dValue, 0,0);
                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se Actualizaron todos los precios");    
                            }
                            else
                            {
                                this.MensajeError(rpta);
                            }
                            this.Close();
                        }
                    }
                    // ======= Caso actualizar todos y lineal ============
                    if (rbLineal.Checked)
                    {
                        // Llamar capa negocio correspondiente
                        if (rbLineal.Enabled)
                        {
                            if (this.tbLineal.Text == string.Empty)
                            {
                                MensajeError("Falta ingresar algunos datos");
                            }
                            else
                            {
                                // Solo numerico
                                uint value;
                                if (!uint.TryParse(tbLineal.Text, out value))
                                {
                                    MensajeError("Solo debe ingresar datos numericos");
                                    return;
                                }
                                Decimal dValue = -1;
                                decimal.TryParse(this.tbLineal.Text, out dValue);
                                rpta = CN_Productos.ActualizacionLineal(dValue, 0, 0);
                                if (rpta.Equals("OK"))
                                {
                                    this.MensajeOk("Se Actualizaron todos los precios");
                                }
                                else
                                {
                                    this.MensajeError(rpta);
                                }
                                this.Close();
                            }
                        }
                    }
                }
                // ======= Caso actualizar por rango de productos ============
                else
                {
                    // Hasta es menor que desde
                    if (Convert.ToInt32(tbHastaArticulo.Text) <= Convert.ToInt32(tbDesdeArticulo.Text))
                    {
                        MensajeError("Rango incorrecto");
                        return;
                    }
                    // ======= Caso actualizar por rango de productos y porcentual ============
                    if (rbPorcentual.Checked)
                    {
                        if (this.tbPorcentual.Text == string.Empty || this.tbDesdeArticulo.Text == string.Empty || this.tbHastaArticulo.Text == string.Empty)
                        {
                            MensajeError("Falta ingresar algunos datos");
                        }
                        else
                        {
                            // Solo numerico
                            uint iValue;
                            if (!uint.TryParse(tbPorcentual.Text, out iValue) || !uint.TryParse(tbDesdeArticulo.Text, out iValue) || !uint.TryParse(tbHastaArticulo.Text, out iValue))
                            {
                                MensajeError("Solo debe ingresar datos numericos");
                                return;
                            }
                            // Solo menor o igual a 100
                            if (Convert.ToInt32(tbPorcentual.Text) > 100)
                            {
                                MensajeError("Debe ingresar un valor menor o igual a 100%");
                                return;
                            }
                            Decimal dValue = -1;
                            decimal.TryParse(this.tbPorcentual.Text, out dValue);
                            rpta = CN_Productos.ActualizacionPorcentual(dValue, Convert.ToInt32(tbDesdeArticulo.Text) , Convert.ToInt32(this.tbHastaArticulo.Text) );
                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se Actualizaron todos los precios");
                            }
                            else
                            {
                                this.MensajeError(rpta);
                            }
                            this.Close();
                        }
                    }
                    // ======= Caso actualizar por rango de productos y lineal ============
                    if (rbLineal.Checked)
                    {
                        // Llamar capa negocio correspondiente
                        if (rbLineal.Enabled)
                        {
                            if (this.tbLineal.Text == string.Empty || this.tbDesdeArticulo.Text == string.Empty || this.tbHastaArticulo.Text == string.Empty)
                            {
                                MensajeError("Falta ingresar algunos datos");
                            }
                            else
                            {
                                // Solo numerico
                                uint value;
                                if (!uint.TryParse(tbLineal.Text, out value) || !uint.TryParse(tbDesdeArticulo.Text, out value) || !uint.TryParse(tbHastaArticulo.Text, out value))
                                {
                                    MensajeError("Solo debe ingresar datos numericos");
                                    return;
                                }
                                Decimal dValue = -1;
                                decimal.TryParse(this.tbLineal.Text, out dValue);
                                rpta = CN_Productos.ActualizacionLineal(dValue, Convert.ToInt32(tbDesdeArticulo.Text) , Convert.ToInt32(tbHastaArticulo.Text) );
                                if (rpta.Equals("OK"))
                                {
                                    this.MensajeOk("Se Actualizaron todos los precios");
                                }
                                else
                                {
                                    this.MensajeError(rpta);
                                }
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "InvenControl", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void lblPorcentual_DoubleClick(object sender, EventArgs e)
        {
            tbLineal.ReadOnly = true;
            tbPorcentual.ReadOnly = false;
            tbPorcentual.Focus();
            rbPorcentual.Checked = true;
        }

        private void lblLineal_DoubleClick(object sender, EventArgs e)
        {
            tbPorcentual.ReadOnly = true;
            tbLineal.ReadOnly = false;
            tbLineal.Focus();
            rbLineal.Checked = true;
        }

        private void rbPorcentual_Click(object sender, EventArgs e)
        {
            tbLineal.ReadOnly = true;
            tbPorcentual.ReadOnly = false;
            tbPorcentual.Focus();
            rbPorcentual.Checked = true;
        }

        private void rbLineal_Click(object sender, EventArgs e)
        {
            tbPorcentual.ReadOnly = true;
            tbLineal.ReadOnly = false;
            tbLineal.Focus();
            rbLineal.Checked = true;
        }
    }
}
