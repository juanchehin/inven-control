using LibUsbDotNet;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace CapaPresentacion.Configuraciones.balanzas
{
    public partial class formEnviarRecibir : Form
    {
        private delegate void DelegadoAcceso(string accion);


        public formEnviarRecibir()
        {
            InitializeComponent();
            deshabilitar_botones();
            llenar_cb_parity();
            llenar_cb_stop_bit();
        }

        private void llenar_cb_parity()
        {
            cbParityBit.Items.Add("No");
            cbParityBit.Items.Add("Si");

            cbParityBit.SelectedIndex = 0;

        }

        private void llenar_cb_stop_bit()
        {
            cbStopBit.Items.Add("No");
            cbStopBit.Items.Add("Si");

            cbStopBit.SelectedIndex = 0;

        }

        private void btnBuscarPuertos_Click(object sender, EventArgs e)
        {
            alta_log("Clic boton BUSCAR PUERTOS");

            CargarPuertosDisponibles();
        }

        private void habilitar_botones()
        {
            btnEnviarDatos.Enabled = true;
        }

        private void deshabilitar_botones()
        {
            btnEnviarDatos.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            formInfoBaudRate frm = new formInfoBaudRate();
            frm.Show();
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
                habilitar_botones();
            }
            else
            {
                deshabilitar_botones();
                // Si no se encontraron puertos, muestra un mensaje indicando que no hay puertos disponibles
                MessageBox.Show("No se encontraron puertos COM disponibles.", "Puertos COM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                alta_log("No se encontraron puertos COM disponibles.");
            }
        }

        private void alta_log(string mensaje)
        {
            try
            {

                // Obtiene la ruta de acceso a la carpeta AppData del usuario actual
                string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                // Crea la ruta completa para tu archivo dentro de la carpeta AppData
                string filePath = Path.Combine(appDataFolder, "store-soft", "logs_balanza_enviar_recibir.txt");

                // Verifica si el directorio del archivo existe, si no, lo crea
                string directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }


                // Verifica si el archivo existe
                if (!File.Exists(filePath))
                {
                    // Si el archivo no existe, lo crea y escribe contenido
                    using (StreamWriter writer = File.CreateText(filePath))
                    {
                        string mensaje_creacion = "Este es un nuevo archivo creado.";

                        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Obtiene la fecha y hora actual
                        writer.WriteLine($"[{timestamp}] - {mensaje_creacion}");
                    }

                    Console.WriteLine("Archivo creado exitosamente.");
                }

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Obtiene la fecha y hora actual
                    writer.WriteLine($"[{timestamp}] - {mensaje}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error al crear el archivo logs");
            }

        }

        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            alta_log(" pasa sp_DataReceived");

            if (this.Enabled == true)
            {
                Thread.Sleep(500);
                string data = spPuertos.ReadExisting();
                this.BeginInvoke(new DelegadoAcceso(si_DataReceived), new object[] { data });
            }
        }

        private void btnEnviarDatos_Click(object sender, EventArgs e)
        {
            Parity parity_bit = Parity.None;
            StopBits stop_bit = StopBits.One;

            try
            {
                string puertoSeleccionado = cbPuertos.SelectedItem.ToString();

                if (cbParityBit.Text == "Si")
                {
                    parity_bit = Parity.Mark;
                }

                if (cbParityBit.Text == "2")
                {
                    stop_bit = StopBits.Two;
                }

                alta_log("**Pasa 1**");


                try
                {
                    alta_log("**Pasa 1.1**");
                    spPuertos = new SerialPort(puertoSeleccionado, Int32.Parse(dupBaudRate.Text), parity_bit, Int32.Parse(dupDataBits.Text), StopBits.One);   // ver manual de la balanza para estos parametros
                    spPuertos.Open();
                    spPuertos.DiscardInBuffer();
                    spPuertos.Handshake = Handshake.None;
                    alta_log("**Pasa 2**");
                    spPuertos.DataReceived += new SerialDataReceivedEventHandler(spPuertos_DataReceived);
                    alta_log("**Pasa 3**");
                    spPuertos.ReadTimeout = 500;
                    spPuertos.WriteTimeout = 500;

                    alta_log("Configuracion del puerto ");

                    // Muestra la configuración del puerto en la consola
                    alta_log("Configuración del puerto serie:");
                    alta_log($"Nombre del puerto: {spPuertos.PortName}");
                    alta_log($"Velocidad de transmisión: {spPuertos.BaudRate} baudios");
                    alta_log($"Bits de datos: {spPuertos.DataBits}");
                    alta_log($"Paridad: {spPuertos.Parity}");
                    alta_log($"Bits de parada: {spPuertos.StopBits}");


                    spPuertos.Write(txtEnviarDatos.Text);

                    alta_log("Dato enviado : " + txtEnviarDatos.Text);
                }
                catch (IOException error)
                {
                    alta_log("Problema interno btnEnviarDatos_Click " + error.Message);
                    MessageBox.Show(error.Message, "Problema interno btnEnviarDatos_Click");
                }

            }
            catch (Exception ex)
            {
                alta_log("Problema btnEnviarDatos_Click " + ex.Message);
                MessageBox.Show(ex.Message, "Problema btnEnviarDatos_Click");
            }
           
        }

        private void si_DataReceived(string accion)
        {
            lblDatosRecibidos.Text = accion;
        }

        private void btn_recibir_Click(object sender, EventArgs e)
        {

        }

        private void btn_ruta_logs_Click(object sender, EventArgs e)
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = Path.Combine(appDataFolder, "store-soft", "logs_balanza_enviar_recibir.txt");

            // Obtén el directorio donde se encuentra el archivo
            string directory = Path.GetDirectoryName(filePath);

            // Abre el explorador de archivos en la ubicación del directorio
            Process.Start("explorer.exe", directory);
        }

        private void spPuertos_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            alta_log(" pasa spPuertos_DataReceived");
            if (this.Enabled == true)
            {
                Thread.Sleep(500);
                string data = spPuertos.ReadExisting();
                this.BeginInvoke(new DelegadoAcceso(si_DataReceived), new object[] { data });
            }
        }


    }
}
