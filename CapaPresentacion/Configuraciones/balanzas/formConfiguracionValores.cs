using CapaNegocio;
using System;
using System.Data;
using System.IO.Ports;
using System.Windows.Forms;

namespace CapaPresentacion.Configuraciones.balanzas
{
    public partial class formConfiguracionValores : Form
    {
        CN_Configuraciones objetoCN_configuraciones = new CN_Configuraciones();
        DataTable resp;

        public formConfiguracionValores()
        {
            InitializeComponent();
        }
        private void formConfiguracionValores_Load(object sender, EventArgs e)
        {
            llenar_cb_parity();
            llenar_cb_stop_bits();
            cargarDatosConfiguracionBalanza();
        }
        private void cargarDatosConfiguracionBalanza()
        {
            resp = objetoCN_configuraciones.cargarDatosConfiguracionBalanza();

            foreach (DataRow row in resp.Rows)
            {
                txtBalanza.Text = Convert.ToString(row["balanza"]);
                cbPuertos.Text = Convert.ToString(row["port_name"]);
                txtBaudRate.Text = Convert.ToString(row["baud_rate"]);
                txtDataBits.Text = Convert.ToString(row["data_bits"]);
                cbStopBits.Text = Convert.ToString(row["stop_bits"]);
                cbParityBits.Text = Convert.ToString(row["parity_bits"]);

            }
        }

        private void llenar_cb_parity()
        {
            cbParityBits.Items.Add("No");
            cbParityBits.Items.Add("Si");

            cbParityBits.SelectedIndex = 0;

        }

        private void llenar_cb_stop_bits()
        {
            cbStopBits.Items.Add("None");
            cbStopBits.Items.Add("One");
            cbStopBits.Items.Add("Two");
            cbStopBits.Items.Add("OnePointFive");

            cbStopBits.SelectedIndex = 1;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscarPuertos_Click(object sender, EventArgs e)
        {
            CargarPuertosDisponibles();
        }
        private void CargarPuertosDisponibles()
        {
            // Obtiene una lista de los nombres de los puertos COM disponibles
            string[] puertosDisponibles = SerialPort.GetPortNames();

            // Limpia los elementos existentes en el ComboBox
            cbPuertos.Items.Clear();

            // Agrega los nombres de los puertos disponibles al ComboBox
            foreach (string puerto in puertosDisponibles)
            {
                cbPuertos.Items.Add(puerto);
            }

            // Si se encontraron puertos disponibles, selecciona el primero en la lista
            if (cbPuertos.Items.Count > 0)
            {
                cbPuertos.SelectedIndex = 0;
                //habilitar_botones();
            }
            else
            {
                //deshabilitar_botones();
                // Si no se encontraron puertos, muestra un mensaje indicando que no hay puertos disponibles
                MessageBox.Show("No se encontraron puertos COM disponibles.", "Puertos COM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Si se encontraron puertos disponibles, selecciona el primero en la lista
                if (cbPuertos.Items.Count > 0)
                {
                    cbPuertos.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("No se encontraron puertos COM disponibles.Debe ingresar un puerto para continuar", "Problema - Puertos COM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string rpta = "";

                rpta = CN_Configuraciones.update_config_balanza(this.txtBalanza.Text.Trim(), cbPuertos.SelectedItem.ToString(), this.txtBaudRate.Text.Trim(),
                    this.txtDataBits.Text.Trim(), cbStopBits.SelectedItem.ToString(), cbParityBits.SelectedItem.ToString());


                if (rpta.Equals("Ok"))
                {
                    MessageBox.Show("Se Insertó de forma correcta el registro");
                }
                else
                {
                    MessageBox.Show(rpta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
            this.Close();
        }
    }
}
